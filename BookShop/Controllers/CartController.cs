using BookShop.Ui.Repositiores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Ui.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICardRepository _cartRep;
        public CartController(ICardRepository cartRep)
        {
            _cartRep= cartRep;
        }

       public async Task<IActionResult> AddItem(int bookId, int qty =1,int redirect=0)
        {
            var resultCount = await _cartRep.AddCart(bookId, qty);
            if (redirect == 0)
            {
                return Ok(resultCount);
            }
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var resultCount = await _cartRep.RemoveCart(bookId);
            return View();
        }

        public async Task<IActionResult> GetUserItem(int bookId)
        {
            var resultCount = await _cartRep.GetUserCart();
            return View();
        }

        public async Task<IActionResult> GetTotalItemInCart(int bookId)
        {
            var resultItem = await _cartRep.GetCartItemCount();
            return Ok(resultItem);
        }
    }
}
