﻿using System;
using System.Windows;
using System.Windows.Interop;
using Caliburn.Micro;

namespace TextGrunt.Services
{
    // low level code from: https://stackoverflow.com/questions/621577/clipboard-event-c-sharp
    // Need to Initiate() with a window 
    public class ClipboardService : IClipboardService
    {
        readonly int _maxSize;
        public ClipboardService()
        {
            IsRecording = false;
            Clips = new BindableCollection<string>();
            _maxSize = 100;
        }
        


        public void Initiate(Window windowSource)
        {
            HwndSource source = PresentationSource.FromVisual(windowSource) as HwndSource;
            if(source == null)
                throw new ArgumentException("Window source MUST be initialized first, such as in the Window's OnSourceInitialized handler.");

            source.AddHook(WndProc);
            // get window handle for interop
            IntPtr windowHandle = new WindowInteropHelper(windowSource).Handle;
            // register for clipboard events
            NativeMethods.AddClipboardFormatListener(windowHandle);

            IsRecording = true;
        }


        public BindableCollection<string> Clips { get; set; }

        public bool IsRecording { get; set; }

        void OnClipboardChanged()
        {
            if (!Clipboard.ContainsText() || !IsRecording)
                return;

            string newText = Clipboard.GetText();
            // only unique strings wanted
            while (Clips.Contains(newText))
                Clips.Remove(newText);
            // put clip on top of list
            Clips.Insert(0, Clipboard.GetText());
            while (Clips.Count > _maxSize)
                Clips.RemoveAt(Clips.Count - 1);
        }
        IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == NativeMethods.WM_CLIPBOARDUPDATE)
            {
                OnClipboardChanged();
                handled = true;
            }

            return IntPtr.Zero;
        }

        public void ToClipBoard(string text)
        {
            Clipboard.SetDataObject(text);
        }
    }
}