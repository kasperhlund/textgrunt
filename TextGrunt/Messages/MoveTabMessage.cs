using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGrunt.ViewModels;

namespace TextGrunt.Messages
{
    public class MoveTabMessage
    {
        public IShellTabItem Source { get; set; }
        public IShellTabItem Target { get; set; }
    }
}
