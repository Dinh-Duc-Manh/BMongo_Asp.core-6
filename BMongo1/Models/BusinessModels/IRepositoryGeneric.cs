namespace BMongo1.Models.BusinessModels
{
    public interface IRepositoryGeneric<T,K,FK>
    {
        List<T> GetAll();
        T GetById(K key);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(K key);

        List<FK> GetForeignKey();
    }
}
