using Newtonsoft.Json;
using System.IO;

namespace TextGrunt.Services
{
    public class JsonStorageService : IStorageService
    {
        public bool Write<T>(T obj, string path)
        {
            try
            {
                string s = JsonConvert.SerializeObject(obj, Formatting.Indented);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, s);
                return true;
            }
            catch { return false; }
        }

        public T Read<T>(string path)
        {
            if (!File.Exists(path))
            {
                return default(T);
            }
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}