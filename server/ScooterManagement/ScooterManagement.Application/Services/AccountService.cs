using ScooterManagement.Application.Exceptions;
using ScooterManagement.Domain.Entites;
using ScooterManagement.Domain.Interfaces;
using ScooterManagement.Domain.Requests;
using System.Net;

namespace ScooterManagement.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public AccountService(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task RegisterUserAsync(RegisterRequest request)
    {
        User? user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);

        if (user != null)
        {
            throw new DuplicateException()
            {
                ErrorMessage = $"There's already an account associated with the email {request.Email}."
            };
        }

        string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);

        User newUser = Mapper.RegisterRequestToUserEntity(request, passwordHash);

        await _unitOfWork.UserRepository.CreateUserAsync(newUser);
        await _unitOfWork.SaveAsync();
    }

    public async Task<string> LoginUserAsync(LoginRequest request)
    {
        User? userFromDb = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);

        if (userFromDb == null)
        {
            throw new EntityNotFoundException()
            {
                ErrorMessage = $"There's no account registered with the email {request.Email}"
            };
        }

        bool isMatch = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, userFromDb.Password);

        if (!isMatch)
        {
            throw new LoginException
            {
                ErrorMessage = "The password is incorrect.",
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        string token = _jwtService.CreateToken(userFromDb.RoleId, userFromDb.Id);

        return token;
    }
}
