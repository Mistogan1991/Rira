namespace Rira.Application.Users.Queries.GetUser;

public record GetUserDto(int Id, string FirstName, string LastName, string NationalCode, DateTime BirthDate);
