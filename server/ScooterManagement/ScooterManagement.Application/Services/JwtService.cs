﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScooterManagement.Application.Exceptions;
using ScooterManagement.Domain.Enums;
using ScooterManagement.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ScooterManagement.Application.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public string CreateToken(int roleId, int userId)
    {
        Role userRole = (Role)roleId;

        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.Role, userRole.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        string secretKey = _configuration["JwtSettings:SecretKey"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: DateTime.UtcNow.AddDays(1),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int GetNameIdentifier()
    {
        var claims = GetClaimsFromJwt();

        int userId = 0;

        if (claims == null)
        {
            throw new AuthorizationException
            {
                ErrorMessage = "The token doesn't contain any claims.",
                StatusCode = HttpStatusCode.Forbidden
            };
        }

        foreach (var claim in claims)
        {
            if (claim.Type is ClaimTypes.NameIdentifier)
            {
                userId = int.Parse(claim.Value);
            }
        }

        if (userId == 0)
        {
            throw new AuthorizationException
            {
                ErrorMessage = "The token doesn't contain the subject claim.",
                StatusCode = HttpStatusCode.Forbidden
            };
        }

        return userId;
    }

    private IEnumerable<Claim>? GetClaimsFromJwt()
    {
        string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        string token;

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            token = authorizationHeader.Split(' ')[1].Trim();

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            return jwtSecurityToken.Claims;
        }

        return null;
    }
}
