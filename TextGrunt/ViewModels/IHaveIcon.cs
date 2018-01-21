using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGrunt.ViewModels
{
    public interface IHaveIcon
    {
        IconType IconType { get; }
    }

    public enum IconType
    {
        None,
        ClipBoard
    }
}
