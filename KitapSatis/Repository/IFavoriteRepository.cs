using KitapSatis.Models;

namespace KitapSatis.Repository
{
    public interface IFavoriteRepository:IRepository<Favorite>
    {
        Favorite GetByUserId(string userId);
        void DeleteFromFavorite(int favoriteId, int productId);
    }
}
