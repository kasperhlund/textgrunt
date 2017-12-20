using Caliburn.Micro;
using System.Reflection;

namespace TextGrunt.ViewModels
{
    internal class AboutViewModel : Screen
    {
        public string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}