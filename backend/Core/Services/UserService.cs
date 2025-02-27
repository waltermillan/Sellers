using Core.Entities;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            throw new KeyNotFoundException("User not found");

        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAllAsync();
    }

    public void AddUser(User user)
    {
        _userRepository.AddAsync(user);
    }

    public void AddUsers(IEnumerable<User> users)
    {
        foreach (var user in users)
            _userRepository.AddAsync(user);
    }

    public void UpdateUser(User user)
    {
        var exists = _userRepository.GetByIdAsync(user.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("User to update not found");

        _userRepository.UpdateAsync(user);
    }

    public void DeleteUser(User user)
    {
        var exists = _userRepository.GetByIdAsync(user.Id).Result;

        if (exists is null)
            throw new KeyNotFoundException("User not found");

        _userRepository.DeleteAsync(user.Id);
    }
}
