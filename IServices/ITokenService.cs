namespace EmployeeManagementApplication.IServices
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }
}
