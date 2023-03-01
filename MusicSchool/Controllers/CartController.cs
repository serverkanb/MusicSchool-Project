using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MusicSchool.Controllers
{
    public class CartController : Controller
    {
        private const string SESSIONKEY = "cart";

        private readonly IInstrumentService _instrumentService;

        public CartController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        public IActionResult GetCart()
        {
            List<CartItemModel> cart = GetCartFromSession();

            List<CartItemGroupByModel> cartGroupBy = (from c in cart
                                                      group c by new { c.InstrumentId, c.UserId, c.InstrumentName }
                                                    into cGroupBy
                                                      select new CartItemGroupByModel()
                                                      {
                                                          UserId = cGroupBy.Key.UserId,
                                                          InstrumentId= cGroupBy.Key.InstrumentId,
                                                          InstrumentName = cGroupBy.Key.InstrumentName,
                                                          TotalUnitPrice = cGroupBy.Sum(cgb => cgb.UnitPrice).ToString("C2"),
                                                          TotalCount = cGroupBy.Count() + " pieces",
                                                          TotalUnitPriceValue = cGroupBy.Sum(cgb => cgb.UnitPrice),
                                                          TotalCountValue = cGroupBy.Count()

                                                      }).ToList();
            return View("CartGroupBy", cartGroupBy);
        }

        public IActionResult AddToCart(int instrumentId)
        {
            List<CartItemModel> cart = GetCartFromSession();
            int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            InstrumentModel instrument = _instrumentService.Query().SingleOrDefault(i => i.Id == instrumentId);
            if (instrument.StockAmount == 0)
            {
                TempData["Message"] = "Instrument cannot be added to card because there is not product in stock!";
                return RedirectToAction("Index", "Instrument");
            }
            CartItemModel cartItem = new CartItemModel()
            {
                InstrumentId = instrumentId,
                InstrumentName = instrument.Name,
                UnitPrice = instrument.UnitPrice ?? 0,
                UserId = userId
            };
            cart.Add(cartItem);
            string cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(SESSIONKEY, cartJson);
            TempData["Message"] = $"{instrument.Name} added to cart";
            return RedirectToAction("Index", "Instruments");
        }

        private List<CartItemModel> GetCartFromSession()
        {
            List<CartItemModel> cart = new List<CartItemModel>();
            string cartJson = HttpContext.Session.GetString(SESSIONKEY);
            if (!string.IsNullOrWhiteSpace(cartJson))
            {
                cart = JsonConvert.DeserializeObject<List<CartItemModel>>(cartJson);
                int userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                cart = cart.Where(c => c.UserId == userId).ToList();
            }

            return cart;
        }

        public IActionResult ClearCart()
        {
            List<CartItemModel> cart = new List<CartItemModel>();
            string cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(SESSIONKEY, cartJson);
            TempData["Message"] = "Cart cleared.";
            return RedirectToAction(nameof(GetCart));
        }

        public IActionResult DeleteFromCart(int userId,int instrumentId)
        {
            List<CartItemModel> cart = GetCartFromSession();
            CartItemModel cartItem = cart.FirstOrDefault(c => c.UserId == userId && c.InstrumentId==instrumentId );
            if (cartItem is not null)
            {
                cart.Remove(cartItem);
            }
            string cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(SESSIONKEY, cartJson);
            TempData["Message"] = "Item removed from cart successfully.";
            return RedirectToAction(nameof(GetCart));

        }
    }
}
