using BookShop.Data;
using BookShop.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Ui.Repositiores
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> user;
        private readonly IHttpContextAccessor http;
        public CardRepository(ApplicationDbContext dbContext, UserManager<IdentityUser> user, IHttpContextAccessor http)
        {
            this.dbContext = dbContext;
            this.user = user;
            this.http = http;
        }
        public async Task<int> AddCart(int bookId, int quantity)
        {
            string userId = GetUserId();
            using var transation = dbContext.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId)) { throw new Exception("user is not logged-in"); }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    dbContext.ShoppingCarts.Add(cart);
                }
                dbContext.SaveChanges();
                // Cart
                var cartItem = dbContext.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if (cartItem is not null)
                { cartItem.Quantity += quantity; }
                else
                {
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCartId = cart.Id,
                        Quantity = quantity
                    };
                    dbContext.CartDetails.Add(cartItem);
                }

                dbContext.SaveChanges();
                transation.Commit();
            }
            catch (Exception ex) { }
            return await GetCartItemCount(userId);
        }
        public async Task<int> RemoveCart(int bookId)
        {
            string userId = GetUserId();
            try
            {
                
                if (string.IsNullOrEmpty(userId)) { throw new Exception("user is not logged-in"); }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    throw new Exception("Cart is empty");
                }
                // Cart
                var cartItem = dbContext.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if (cartItem == null) { throw new Exception("Not item in cart"); }
                else if (cartItem.Quantity == 1)
                {
                    dbContext.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                dbContext.SaveChanges();
            }
            catch (Exception ex) { }
            return await GetCartItemCount(userId);

        }
        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId is null) { throw new Exception("Invalid userId"); }
            var shoppingCart = await dbContext.ShoppingCarts
                .Include(x => x.Carts)
                .ThenInclude(x => x.Book).ThenInclude(x => x.Genre)
                .Where(x => x.UserId == userId).FirstOrDefaultAsync();
            return shoppingCart;
        }
        public async Task<int> GetCartItemCount(string userId = "")
        {
            if(!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            var data = await (from cart in dbContext.ShoppingCarts
                              join cartDetail in dbContext.CartDetails
                              on cart.Id equals cartDetail.ShoppingCartId
                              select new { cartDetail.Id }).ToListAsync();
            return data.Count;
        }
        public async Task<ShoppingCart> GetCart(string userId) => await dbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
        private string GetUserId()
        {
            var pcl = http.HttpContext.User;
            return user.GetUserId(pcl);
        }

    }
}
