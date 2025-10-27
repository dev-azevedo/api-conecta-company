namespace ConectaCompany.Shared.Constants;

public class Roles
{
    public const string Admin = "Admin";
    public const string Manager = "Manager";
    public const string Employee = "Employee";

    public static readonly string[] All = new[]
    {
        Admin,
        Manager,
        Employee
    };
}