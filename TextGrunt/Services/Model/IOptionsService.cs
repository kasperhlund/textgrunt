using TextGrunt.Models;

namespace TextGrunt.Services
{
    public interface IOptionsService
    {
        Options Current { get; }
        Options Default { get; }
    }
}