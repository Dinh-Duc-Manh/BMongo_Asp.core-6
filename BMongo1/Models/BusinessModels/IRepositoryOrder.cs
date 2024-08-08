using BMongo1.Models.ModelsViews;

namespace BMongo1.Models.BusinessModels
{
    public interface IRepositoryOrder
    {
        List<Orders> GetAll();
        Orders GetAccId(int id);
        Orders GetById(int id);
        bool Insert(Orders entity);
        bool Update(Orders entity);
        bool Delete(int id);
        int GetId();
        List<OrdersViewModel> GetForeignKey(int AccId);
    }
}
