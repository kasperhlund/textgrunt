using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows;

namespace Textgrunt.tests
{
    [TestClass]
    public class PopulateData
    {
        // Need to start Textgrunt manually before running...
        [Ignore]
        [TestMethod]
        public void LotsofTextToClipBoard()
        {
            for (int i = 0; i < 150; i++)
            {
                Thread t = new Thread(() => Clipboard.SetDataObject($"Clip {i}"));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                Thread.Sleep(100);
            }
        }
    }
}