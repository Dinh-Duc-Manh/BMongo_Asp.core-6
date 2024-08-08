using BMongo1.Models.ModelsViews;

namespace BMongo1.Models.BusinessModels
{
    public interface IRepositoryOrderDetail
    {
        List<OrderDetail> GetAll();
        OrderDetail GetAccId(int id);
        OrderDetail GetById(int id);
        bool Insert(OrderDetail entity);
        bool Update(OrderDetail entity);
        bool Delete(int id);
        List<OrderDetailViewModel> GetForeignKey(int OdId);
    }
}
