namespace AuthWeb.Services
{
    public interface IEncryptionService
    {
        string EncryptPassword(string password);
        string DecryptPassword(string password);
    }
}
