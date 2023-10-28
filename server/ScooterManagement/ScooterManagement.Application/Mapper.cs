using ScooterManagement.Domain.Dtos;
using ScooterManagement.Domain.Entites;
using ScooterManagement.Domain.Requests;

namespace ScooterManagement.Application;

internal static class Mapper
{
    public static User RegisterRequestToUserEntity(RegisterRequest request, string passwordHash)
    {
        return new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = passwordHash,
            RoleId = (int)request.RoleId,
        };
    }

    public static ScooterDto ScooterEntityToScooterDto(Scooter scooter)
    {
        return new ScooterDto
        {
            Name = scooter.Name,
            MaxSpeed = scooter.MaxSpeed,
            RentalPrice = scooter.RentalPrice,
            UserId = scooter.UserId
        };
    }

    public static Scooter CreateScooterRequestToScooterEntity(CreateScooterRequest request)
    {
        return new Scooter
        {
            Name = request.Name,
            MaxSpeed = request.MaxSpeed,
            RentalPrice = request.RentalPrice
        };
    }
}
