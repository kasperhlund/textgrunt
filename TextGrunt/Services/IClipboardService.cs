using Caliburn.Micro;
using System;
using System.Windows;
using System.Windows.Input;

namespace TextGrunt.Services
{
    public interface IClipboardService
    {
        BindableCollection<string> Clips { get; set; }
        void Initiate(Window window);
        bool IsRecording { get; set; }
        void ToClipBoard(string text);
    }
}