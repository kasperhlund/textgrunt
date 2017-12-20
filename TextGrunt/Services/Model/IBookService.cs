using TextGrunt.Models;

namespace TextGrunt.Services
{
    public interface IBookService
    {
        Book Book { get; }

        Sheet BuildNewSheet();
    }
}