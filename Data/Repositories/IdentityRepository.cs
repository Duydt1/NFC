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
        Task SeedDataAsync();
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

        public async Task SeedDataAsync()
        {
            // Insert roles if they do not exist
            var insertRolesQuery = @"
				IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name IN ('Admin', 'Create Data', 'View Data'))
				BEGIN
					INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
					VALUES 
					(NEWID(), 'Admin', 'ADMIN', NEWID()),
					(NEWID(), 'Create Data', 'CREATE DATA', NEWID()),
					(NEWID(), 'View Data', 'VIEW DATA', NEWID());
				END
			";

            await _context.Database.ExecuteSqlRawAsync(insertRolesQuery);

            // Insert a default production line if it does not exist
            var insertProductionLineQuery = @"
				IF NOT EXISTS (SELECT 1 FROM ProductionLines WHERE Name = 'Line 0')
				BEGIN
					INSERT INTO ProductionLines(Name)
					VALUES ('Line 0');
				END
			";

            await _context.Database.ExecuteSqlRawAsync(insertProductionLineQuery);

            // Check if the user already exists
            var insertUserQuery = @"
				IF NOT EXISTS (SELECT 1 FROM AspNetUsers WHERE UserName = 'nfc_admin@gmail.com')
				BEGIN
					-- Insert user
					INSERT INTO AspNetUsers 
					(Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, ProductionLineId, RoleId)
					VALUES 
					(NEWID(), 'nfc_admin@gmail.com', 'NFC_ADMIN@GMAIL.COM', 'nfc_admin@gmail.com', 'NFC_ADMIN@GMAIL.COM', 1, 
					'AQAAAAIAAYagAAAAEIuXt3ZfMSwN/nnfRUmu1XVaRo3HbGr9D7v7NI9BLUVNXvDShzFYaf3h0Mg9rptHUw==', NEWID(), NEWID(), 0, 0, 1, 0, 
					(SELECT TOP(1) Id FROM ProductionLines), (SELECT Id FROM AspNetRoles WHERE NormalizedName = 'ADMIN'));
				END
				ELSE
				BEGIN
					PRINT 'User  already exists. Skipping user insert.';
				END
			";

            await _context.Database.ExecuteSqlRawAsync(insertUserQuery);

            // Assign user to role if not already assigned
            var assignUserRoleQuery = @"
				DECLARE @UserId UNIQUEIDENTIFIER = (SELECT Id FROM AspNetUsers WHERE NormalizedUserName = 'NFC_ADMIN@GMAIL.COM');
                DECLARE @RoleId UNIQUEIDENTIFIER = (SELECT Id FROM AspNetRoles WHERE NormalizedName = 'ADMIN');

                IF @UserId IS NOT NULL AND @RoleId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId)
                BEGIN
				                INSERT INTO AspNetUserRoles (UserId, RoleId)
				                VALUES (@UserId, @RoleId);
                END
                ELSE
                BEGIN
				                PRINT 'User Id or RoleId is NULL or User already assigned to this role. Cannot insert into AspNetUser  Roles.';
                END
			";

            await _context.Database.ExecuteSqlRawAsync(assignUserRoleQuery);
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
