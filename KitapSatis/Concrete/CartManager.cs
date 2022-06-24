using KitapSatis.Abstract;
using KitapSatis.Models;
using KitapSatis.Repository;

namespace KitapSatis.Concrete
{
    public class CartManager : ICartService
    {
        private ICartRepository _cartRepository;
        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            if(cart != null)
            {
                var index = cart.CartItems.FindIndex(x => x.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()//yeni ürün ekleme
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity+=quantity;//ürün zaten var o ürünün adetini arttırıyor
                }
                _cartRepository.Update(cart);
            }
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                _cartRepository.DeleteFromCart(cart.Id, productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartRepository.GetByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _cartRepository.Create(new Cart() { UserId = userId });
        }
    }
}
