using Caliburn.Micro;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;

namespace TextGrunt.Services
{
    // low level code from: https://stackoverflow.com/questions/621577/clipboard-event-c-sharp
    // Need to Initiate() with a window
    public class ClipboardService : IClipboardService
    {
        private readonly int _maxSize;

        public ClipboardService()
        {
            IsRecording = false;
            Clips = new BindableCollection<string>();
            _maxSize = 100;
        }

        public void Initiate(Window windowSource)
        {
            HwndSource source = PresentationSource.FromVisual(windowSource) as HwndSource;
            if (source == null)
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

        private void OnClipboardChanged()
        {
            if (!IsRecording)
                return;

            if (!TryGetClipBoardText(10, 50, out string newText))
            {
                return;
            }

            // only unique strings wanted
            while (Clips.Contains(newText))
                Clips.Remove(newText);
            // put clip on top of list
            Clips.Insert(0, newText);
            while (Clips.Count > _maxSize)
                Clips.RemoveAt(Clips.Count - 1);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
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

        // Sometimes get COMException in Clipboard
        private bool TryGetClipBoardText(int retries, int delayMs, out string result)
        {
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    if (Clipboard.ContainsText())
                    {
                        result = Clipboard.GetText();
                        return true;
                    }
                    result = "";
                    return false;
                }
                catch (COMException) { }
                Thread.Sleep(delayMs);
            }
            result = null;
            return false;
        }
    }
}