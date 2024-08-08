using BMongo1.Models;
using BMongo1.Models.ModelsViews;

namespace BMongo1.Models.BusinessModels
{
    public interface IRepositoryProduct: IRepositoryGeneric<Product,string, ProductsViewModel>
    {
        List<Product> Paging(int page, int pageSize,out long totalrows);
        List<Product> SearchPaging(string name, int page, int pageSize,out long totalrows);
        Task<List<Product>> GetByCatId(int CatId);


    }
}
