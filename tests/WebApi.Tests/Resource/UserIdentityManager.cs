using System;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Security.Cryptography;
using FleetManager.Domain.Security.Token;

namespace WebApi.Tests.Resource;

public class UserIdentityManager(User user, string password, string token)
{
    private readonly User _user = user;
    private readonly string _password = password;
    private readonly string _token = token;

    public string GetName() => _user.Name;
    public string GetEmail() => _user.Email;
    public string GetPassword() => _password;
    public string GetToken() => _token;

}
