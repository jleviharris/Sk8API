using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SkateAPI.Data;
using SkateAPI.Models;
using SkateAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using SkateAPI.Enities;
namespace SkateAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(bool Success, string Message)> RegisterUserAsync(Register request)
        {
            // Check if the user already exists
            var existingUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return (false, "User already exists.");
            }

            // Generate a salt
            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Create a new user
            var user = new AppUser
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, salt),
                Salt = salt,
                Email = request.Email,
                UserType = "D",
                SecurityLevel = 1
            };

            // Add the new user to the context
            _context.AppUsers.Add(user);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return (true, "Registration successful.");
        }

        public async Task<(bool Success, string Token, string Message)> LoginUserAsync(LoginRequest request)
        {
            // Find the user by username
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return (false, null, "Invalid username or password.");
            }

            // Check if the password matches
            bool passwordMatches = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!passwordMatches)
            {
                return (false, null, "Invalid username or password.");
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.RowKey.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                    // Add more claims as needed
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return (true, tokenString, "Login successful.");
        }
    }
}

