using System.Threading;
using System.Threading.Tasks;

namespace ETZ.Lending.Persistence.Contexts
{
    public interface IContext
    {
        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}