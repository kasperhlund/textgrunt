using TextGrunt.Models;

namespace TextGrunt.Services
{
    public interface IBookService
    {
        Book Book { get; set; }

        Sheet BuildNewSheet();
    }
}