namespace ConectaCompany.Application.Dto.Auth;

public record UserDto(string Fullname, string Email, string Password, string ConfirmPassword);