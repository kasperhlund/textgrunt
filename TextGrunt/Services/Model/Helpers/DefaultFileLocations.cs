using System;
using System.IO;

namespace TextGrunt.Services
{
    public static class FileLocations
    {
        public static string OptionsLocation
        {
            get => Path.Combine(FolderLocation, "settings.json");
        }

        public static string BookLocation
        {
            get => Path.Combine(FolderLocation, "workbook.json");
        }

        public static string FolderLocation
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TextGrunt");
        }
    }
}