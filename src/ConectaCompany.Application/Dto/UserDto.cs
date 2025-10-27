namespace ConectaCompany.Application.Dto;

public record UserDto(string FullName, DateOnly Birthday, long CompanyId, long ProfileId, string Email, string Password, string ConfirmPassword, string Role);