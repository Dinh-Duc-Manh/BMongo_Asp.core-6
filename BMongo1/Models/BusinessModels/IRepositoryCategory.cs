using BMongo1.Models;

namespace BMongo1.Models.BusinessModels
{
    public interface IRepositoryCategory: IRepositoryGeneric<Category,int,Category>
    {
        List<Category> Paging(int page, int pageSize, out long totalrows);
        List<Category> SearchPaging(string name, int page, int pageSize, out long totalrows);
    }
}
