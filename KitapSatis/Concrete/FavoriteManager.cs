using KitapSatis.Abstract;
using KitapSatis.Models;
using KitapSatis.Repository;

namespace KitapSatis.Concrete
{
    public class FavoriteManager:IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteManager(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }
        public void InitializeFavorite(string userId)
        {
            _favoriteRepository.Create(new Favorite() { UserId = userId });
        }

        public void AddFavorite(string userId, int productId)
        {
            var favorite = GetFavoriteByUserId(userId);
            if (favorite != null)
            {
                var index = favorite.FavoriteItems.FindIndex(x => x.ProductId == productId);
                if (index < 0)
                {
                    favorite.FavoriteItems.Add(new FavoriteItem()//yeni ürün ekleme
                    {
                        ProductId = productId,
                        FavoriteId = favorite.Id
                    });
                }
                else
                {
                    //ürün zaten var o ürünün adetini arttırıyor
                }
                _favoriteRepository.Update(favorite);
            }
        }

        public void DeleteFromFavorite(string userId, int productId)
        {
            var favorite = GetFavoriteByUserId(userId);
            if (favorite != null)
            {
                _favoriteRepository.DeleteFromFavorite(favorite.Id, productId);
            }
        }

        public Favorite GetFavoriteByUserId(string userId)
        {
            return _favoriteRepository.GetByUserId(userId);
        }

        
    }
}
