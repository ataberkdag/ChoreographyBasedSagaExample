namespace Shared.Base
{
    public interface IRepository<T> where T : EntityBase
    {
        void Add(T item);

        T GetById(long id);
    }
}
