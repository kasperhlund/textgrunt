namespace TextGrunt.Services
{
    public interface IStorageService
    {
        T Read<T>(string path);

        bool Write<T>(T o, string path);
    }
}