using System.Threading.Tasks;

namespace Shared.Base
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        Task SaveChangesAsync();
    }
}
