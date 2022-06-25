using KitapSatis.Models;

namespace KitapSatis.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);
        
        void AddCart(string userId,int productId,int quantitiy);
        void DeleteFromCart(string userId, int productId);
        void ClearCart(int cartId );
    }
}
