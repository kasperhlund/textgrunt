using System;
using System.IO;

namespace TextGrunt.Services
{
    public static class Files
    {
        private const string bookSuffix = "tgtbook";
        private const string tabSuffix = "tgttab";

        public static string OptionsLocation
        {
            get => Path.Combine(FolderLocation, "settings.json");
        }

        public static string BookLocation
        {
            get => Path.Combine(FolderLocation, $"workbook.{bookSuffix}");
        }

        public static string FolderLocation
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TextGrunt");
        }

        public static string GruntTabFileFilter => $"TextGrunt tab files (*.{tabSuffix})|*.{tabSuffix}|All files (*.*)|*.*";
        public static string GruntBookFileFilter => $"TextGrunt workbook files (*.{bookSuffix})|*.{bookSuffix}|All files (*.*)|*.*";
    }
}