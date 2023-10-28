using ScooterManagement.Application.Exceptions;
using ScooterManagement.Domain.Dtos;
using ScooterManagement.Domain.Entites;
using ScooterManagement.Domain.Interfaces;
using ScooterManagement.Domain.Requests;

namespace ScooterManagement.Application.Services;

public class ScooterService : IScooterService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public ScooterService(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<List<ScooterDto>> GetScootersAsync()
    {
        List<Scooter> scooters = await _unitOfWork.ScooterRepository.GetScootersAsync();
        List<ScooterDto> scootersDto = scooters.Select(s => Mapper.ScooterEntityToScooterDto(s)).ToList();

        return scootersDto;
    }

    public async Task CreateScooterAsync(CreateScooterRequest request)
    {
        Scooter newScooter = Mapper.CreateScooterRequestToScooterEntity(request);

        await _unitOfWork.ScooterRepository.CreateScooterAsync(newScooter);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateScooterAsync(int scooterId)
    {
        Scooter? scooter = await _unitOfWork.ScooterRepository.GetScooterByIdAsync(scooterId);

        if (scooter == null)
        {
            throw new EntityNotFoundException
            {
                ErrorMessage = "The scooter you want to rent doesn't exist."
            };
        }

        if (scooter.UserId != null)
        {
            throw new ForbiddenException
            {
                ErrorMessage = "This scooter is already being rented by someone else."
            };
        }

        scooter.UserId = _jwtService.GetNameIdentifier();

        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteScooterAsync(int scooterId)
    {
        Scooter? scooter = await _unitOfWork.ScooterRepository.GetScooterByIdAsync(scooterId);

        if (scooter == null)
        {
            throw new EntityNotFoundException
            {
                ErrorMessage = "The scooter you want to rent doesn't exist."
            };
        }

        if (scooter.UserId != null)
        {
            throw new ForbiddenException
            {
                ErrorMessage = "This scooter cannot be deleted because it's being rented by someone."
            };
        }

        await _unitOfWork.ScooterRepository.DeleteScooterAsync(scooterId);
        await _unitOfWork.SaveAsync();
    }

    public async Task ReturnScooterAsync(int scooterId)
    {
        Scooter? scooter = await _unitOfWork.ScooterRepository.GetScooterByIdAsync(scooterId);

        if (scooter == null)
        {
            throw new EntityNotFoundException
            {
                ErrorMessage = "The scooter you want to return doesn't exist."
            };
        }

        int scooterOwnerId = _jwtService.GetNameIdentifier();

        if (scooter.UserId == null)
        {
            throw new ForbiddenException
            {
                ErrorMessage = "You cannot return this scooter because it doesn't belong to anyone."
            };
        }

        if (scooter.UserId != scooterOwnerId)
        {
            throw new ForbiddenException
            {
                ErrorMessage = "You cannot return this scooter because it doesn't belong to you."
            };
        }

        scooter.UserId = null;

        await _unitOfWork.SaveAsync();
    }
}
