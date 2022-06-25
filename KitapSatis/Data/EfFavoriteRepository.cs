using KitapSatis.Models;
using KitapSatis.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KitapSatis.Data
{
    public class EfFavoriteRepository : EfCoreGenericRepository<Favorite>, IFavoriteRepository
    {
        public EfFavoriteRepository(ApplicationDbContext context) : base(context)
        {

        }
        private ApplicationDbContext ApplicationDbContext
        {
            get { return context as ApplicationDbContext; }
        }


        public void ClearFavorite(int favoriteId)
        {
            var cmd = @"delete from FavoriteItems where FavoriteId=@p0";
            ApplicationDbContext.Database.ExecuteSqlRaw(cmd, favoriteId);
        }

        public void DeleteFromFavorite(int favoriteId, int productId)
        {
            var cmd = @"delete from FavoriteItems where FavoriteId=@p0 and ProductId=@p1";
            ApplicationDbContext.Database.ExecuteSqlRaw(cmd, favoriteId, productId);
        }

        public Favorite GetByUserId(string userId)
        {
            return ApplicationDbContext.Favorites
                        .Include(i => i.FavoriteItems)
                        .ThenInclude(i => i.Product)
                        .FirstOrDefault(i => i.UserId == userId);
        }

        public override void Update(Favorite entity)
        {
            ApplicationDbContext.Favorites.Update(entity);
            ApplicationDbContext.SaveChanges();
        }
    }
}
