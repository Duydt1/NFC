using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NFC.Data;
using NFC.Data.Entities;
using NFC.Data.Models;
using System.Security.Claims;

namespace Data.Repositories
{
    public interface IIdentityRepository
    {
        Task<List<NFCUser>> GetAllUserAsync();
        Task<NFCUser> GetUserAsync(string userId);
        Task<NFCUser> CreateUserAsync(NFCUser user, string password);
        Task<NFCUser> UpdateUserAsync(NFCUser user, string newPass, string curPass);
        Task DeleteUserAsync(NFCUser user);
        Task<bool> CheckExistedDataAsync();


		Task<List<IdentityRole>> GetAllRolesAsync();
        Task<IdentityRole> GetRoleAsync(string roleId);
        Task<bool> GetRoleExistsAsync(string roleName);
        Task<IdentityResult> CreateRoleAsync(IdentityRole role);
        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
    }

    public class IdentityRepository(UserManager<NFCUser> userManager, NFCDbContext context, RoleManager<IdentityRole> roleManager) : IIdentityRepository
    {
        private readonly UserManager<NFCUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly NFCDbContext _context = context;

        public async Task<List<NFCUser>> GetAllUserAsync()
        {
            return await _context.Users.Include(x => x.Role).ToListAsync();
        }
		public async Task<bool> CheckExistedDataAsync()
		{
			return await _context.Users.AnyAsync();
		}
		public async Task<NFCUser> GetUserAsync(string userId)
        {
            return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<NFCUser> CreateUserAsync(NFCUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
                await _userManager.AddToRoleAsync(user, role.Name);
            }
            return user;
        }

        public async Task<NFCUser> UpdateUserAsync(NFCUser user, string newPass, string curPass)
        {
            await _userManager.UpdateAsync(user);
            if (!string.IsNullOrEmpty(newPass) && !string.IsNullOrEmpty(curPass))
            {

                var result = await _userManager.ChangePasswordAsync(user, curPass, newPass);
            }
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
            await _userManager.AddToRoleAsync(user, role.Name);
            // Add the role claim to the user's identity
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role.Name));
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(NFCUser user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IdentityRole> GetRoleAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<IdentityRole> GetRoleByNameAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }
        public async Task<bool> GetRoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> UpdateRoleAsync(IdentityRole role)
        {
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(IdentityRole role)
        {
            return await _roleManager.DeleteAsync(role);
        }
    }
}
