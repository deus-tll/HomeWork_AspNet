﻿using InternetShop.Models.DataModels;
using InternetShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InternetShop.Models.HandlerModels
{
    public interface IUserHandler
    {
        Task<IdentityResult> SignUpUserAsync(string email, string password);
        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
        Task DeleteUserAsync(string? userName);
        Task<List<Message>> GetMessages();
        Task<string?> GetUserEmailByIdAsync(string userId);
        Task<string> SendMessageToManager(ClaimsPrincipal currentUser, string messageText);

    }

    public class UserHandler : IUserHandler
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationContext _context;


        public UserHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public async Task<List<Message>> GetMessages() => await _context.Messages.ToListAsync();


        public async Task<string> SendMessageToManager(ClaimsPrincipal currentUser, string messageText)
        {
            var user = await _userManager.GetUserAsync(currentUser);
            if (user == null) return "Error";

            var message = new Message
            {
                Text = messageText,
                CreatedAt = DateTime.Now,
                UserId = user.Id
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return "Index";
        }


        public async Task<IdentityResult> SignUpUserAsync(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }


        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
            return result;
        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task DeleteUserAsync(string? userName)
        {
            if (userName is null) return;

            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                await _userManager.DeleteAsync(user);
            }
        }


        public async Task<string?> GetUserEmailByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.Email;
        }
    }
}
