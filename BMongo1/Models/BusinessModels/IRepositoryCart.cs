using BMongo1.Models;
using BMongo1.Models.ModelsViews;

namespace BMongo1.Models.BusinessModels
{
    public interface IRepositoryCart
    {
        List<Cart> All();
        Cart GetAccId(int id);
        Cart GetById(int id);
        bool Insert(Cart entity);
        bool Update(Cart entity);
        bool Delete(int id);
        bool DeleteAll(int AccId);

        List<CartViewModel> GetForeignKey(int AccId);
    }
}
