using System.Collections.Generic;
using System.Threading.Tasks;
using ETZ.Lending.Domain.Abstractions.Models;

namespace ETZ.Lending.Domain.Abstractions.Services
{
    public interface IProductDomainService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> SearchAsync(string query, string sortBy = null, string sortDirection = null);
        Task<Product> CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}