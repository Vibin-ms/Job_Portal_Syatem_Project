using Domain.Data;
using Domain.Enum;
using Domain.Models;
using Domain.Services.Authentication.Interface;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Authentication.Repository
{
    public class AuthenticationRepository : IAuthrepository
    {
        private readonly AppDbContext _context;

        public AuthenticationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SystemUser?> RegisterAsync(SystemUser user)
        {
            var existingUser = await _context.SystemUsers
                .FirstOrDefaultAsync(x => x.Email == user.Email);

            if (existingUser != null)
            {
                return null;
            }

            var systemUser = new SystemUser
            {
                Id = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Roles = user.Roles,
                Status = Status.Pending,
                CreatedDate = DateTime.UtcNow
            };


            await _context.SystemUsers.AddAsync(systemUser);
            await _context.SaveChangesAsync();


            if (systemUser.Roles == Role.JobSeeker)
            {
                var jobSeeker = new JobSeeker
                {
                    JobSeekerId = Guid.NewGuid(),
                    SystemUserId = systemUser.Id
                };

                await _context.JobSeekers.AddAsync(jobSeeker);
            }
            else if (systemUser.Roles == Role.JobProvider)
            {
                var jobProvider = new JobProvider
                {
                    JobProviderId = Guid.NewGuid(),
                    SystemUserId = systemUser.Id
                };

                await _context.JobProviders.AddAsync(jobProvider);
            }


            await _context.SaveChangesAsync();

            return systemUser;
        }

        public async Task<bool> UpdateStatusAsync(
           Guid userId,
           Status status)
        {
            var user = await _context.SystemUsers
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            user.Status = status;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> CreatePasswordAsync(
           Guid userId,
           string password)
        {
            var systemUser = await _context.SystemUsers
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (systemUser == null)
            {
                return false;
            }

            if (systemUser.Status != Status.Verified)
            {
                return false;
            }

            var existingAuthUser = await _context.AuthUsers
                .FirstOrDefaultAsync(x => x.SystemUserId == userId);

            if (existingAuthUser != null)
            {
                return false;
            }
            var authUser = new AuthUser
            {
                AuthUserId = Guid.NewGuid(),

                SystemUserId = systemUser.Id,

                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            await _context.AuthUsers.AddAsync(authUser);

            systemUser.Status = Status.Completed;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AuthUser?> LoginAsync(string email, string password)
        {
            var authUser = await _context.AuthUsers
                .Include(x => x.SystemUser)
                .FirstOrDefaultAsync(
                    x => x.SystemUser.Email == email);

            if (authUser == null)
            {
                return null;
            }

            if (authUser.SystemUser.Status != Status.Completed)
            {
                return null;
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(password, authUser.PasswordHash);

            if (!passwordValid)
            {
                return null;
            }

            return authUser;
        }
        private static readonly HashSet<string> RevokedTokens = new();

        public Task RevokeTokenAsync(string token)
        {
            lock (RevokedTokens)
            {
                RevokedTokens.Add(token);
            }

            return Task.CompletedTask;
        }

        public Task<bool> IsTokenRevokedAsync(string token)
        {
            lock (RevokedTokens)
            {
                return Task.FromResult(RevokedTokens.Contains(token));
            }
        }
    }
}  
