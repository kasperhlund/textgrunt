using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using TextGrunt.Models;

namespace TextGrunt.Services
{
    public class OptionsService : IOptionsService
    {
        private IStorageService _storageService;

        public OptionsService(IStorageService storageService)
        {
            _storageService = storageService;
            Current = _storageService.Read<Options>(DefaultSettingsLocation) ?? Default;
            Apply();
            Current.PropertyChanged += (sender, e) => ApplyAndSave();
        }

        public Options Current
        {
            get; private set;
        }

        public Options Default => new Options
        {
            HasAutoStart = true,
        };

        private string DefaultSettingsLocation
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TextGrunt", "settings.json");
        }

        private void ApplyAndSave()
        {
            Apply();
            _storageService.Write(Current, DefaultSettingsLocation);
        }

        private void Apply()
        {
            ApplyAutoStart();
        }

        private void ApplyAutoStart()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (Current.HasAutoStart)
                rk.SetValue("TextGrunt", $"{Assembly.GetExecutingAssembly().Location} {Bootstrapper.CommandlineOnlyTrayBar}");
            else
                rk.DeleteValue("TextGrunt", false);
        }
    }
}