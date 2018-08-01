using TextGrunt.ViewModels;

namespace TextGrunt.Messages
{
    public class MoveTabMessage
    {
        public IShellTabItem Source { get; set; }
        public IShellTabItem Target { get; set; }
    }
}