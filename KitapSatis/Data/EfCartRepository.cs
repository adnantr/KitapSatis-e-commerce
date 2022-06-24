using KitapSatis.Models;
using KitapSatis.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KitapSatis.Data
{
    public class EfCartRepository:EfCoreGenericRepository<Cart>,ICartRepository
    {

        public EfCartRepository(ApplicationDbContext context) : base(context)
        {

        }
        private ApplicationDbContext ApplicationDbContext
        {
            get { return context as ApplicationDbContext; }  
        }


        public void ClearCart(int cartId)
        {
            var cmd = @"delete from CartItems where CartId=@p0";
            ApplicationDbContext.Database.ExecuteSqlRaw(cmd, cartId);
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            var cmd = @"delete from CartItems where CartId=@p0 and ProductId=@p1";
            ApplicationDbContext.Database.ExecuteSqlRaw(cmd, cartId, productId);
        }

        public Cart GetByUserId(string userId)
        {
            return ApplicationDbContext.Carts
                        .Include(i => i.CartItems)
                        .ThenInclude(i => i.Product)
                        .FirstOrDefault(i => i.UserId == userId);
        }

        public override void Update(Cart entity)
        {
            ApplicationDbContext.Carts.Update(entity);
            ApplicationDbContext.SaveChanges();
        }
    }
}
