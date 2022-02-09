using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Domain.Abstractions.Services;
using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Persistence.Abstractions.Repositories;

namespace ETZ.Lending.Domain.Services
{
    public class ProductDomainService : IProductDomainService
    {
        private static readonly string[] IncludeProperties = { nameof(Product.Image) };

        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductDomainService(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _repository.ToListAsync(
                static product => !product.IsLent,
                null,
                IncludeProperties
            );

            return _mapper.Map<IEnumerable<Product>>(products);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _repository.SingleOrDefaultAsync(p => p.Id == id, IncludeProperties);

            return _mapper.Map<Product>(product);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string query, string sortBy = null, string sortDirection = null)
        {
            var products = await _repository.ToListAsync(
                product => product.Name.Contains(query),
                null,
                IncludeProperties
            );

            return _mapper.Map<IEnumerable<Product>>(products);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var entity = _mapper.Map<ProductEntity>(product);

            var created = await _repository.CreateAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<Product>(created);
        }

        public async Task UpdateAsync(Product product)
        {
            var entity = _mapper.Map<ProductEntity>(product);

            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}