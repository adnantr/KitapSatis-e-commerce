using KitapSatis.Models;

namespace KitapSatis.Abstract
{
    public interface IFavoriteService
    {
        void InitializeFavorite(string userId);
        Favorite GetFavoriteByUserId(string userId);

        void AddFavorite(string userId, int productId);
        void DeleteFromFavorite(string userId, int productId);
    }
}
