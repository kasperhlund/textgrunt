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