namespace SoftUniJobPlatform.Services.Data
{
    public interface IStudentsService
    {
        bool IsExistUsername(string username);

        bool IsExistEmail(string email);
    }
}
