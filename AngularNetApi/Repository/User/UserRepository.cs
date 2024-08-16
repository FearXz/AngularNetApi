﻿using AngularNetApi.Conext;
using AngularNetApi.Entities;
using AngularNetApi.Exceptions;

namespace AngularNetApi.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserProfile> GetByIdAsync(string userId)
        {
            try
            {
                var user = await _db.UserProfiles.FindAsync(userId);

                if (user == null)
                {
                    throw new NotFoundException($"User with ID {userId} not found.");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error in UserRepository GetByIdAsync", ex);
            }
        }

        public async Task<UserProfile> CreateAsync(UserProfile user)
        {
            try
            {
                var newUser = await _db.UserProfiles.AddAsync(user);
                await _db.SaveChangesAsync();

                return newUser.Entity;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error in UserRepository CreateAsync", ex);
            }
        }

        public async Task<UserProfile> UpdateAsync(UserProfile user)
        {
            try
            {
                var existingUser = await _db.Users.FindAsync(user.Id);
                if (existingUser == null)
                {
                    throw new NotFoundException($"User with ID {user.Id} not found.");
                }

                _db.UserProfiles.Update(user);
                await _db.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error in UserRepository UpdateAsync", ex);
            }
        }
    }
}
