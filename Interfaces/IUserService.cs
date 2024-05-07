using Microsoft.EntityFrameworkCore;
using SkateAPI.Enities;
using SkateAPI.Models;
using SkateAPI.Data;
using SkateAPI.Interfaces;



namespace SkateAPI.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, string Message)> RegisterUserAsync(Register request);

        Task<(bool Success, string Token, string Message)> LoginUserAsync(LoginRequest request);
    }
}
