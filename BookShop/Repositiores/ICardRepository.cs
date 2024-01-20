using BookShop.Domain.Model;

namespace BookShop.Ui.Repositiores
{
    public interface ICardRepository
    {
        Task<int> AddCart(int bookId, int quantity);
        Task<int> RemoveCart(int bookId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);


    }
}
