using BookShop.Domain.Model;

namespace BookShop.Ui.Repositiores
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0);
        Task<IEnumerable<Genre>> GetGenre();

    }
}