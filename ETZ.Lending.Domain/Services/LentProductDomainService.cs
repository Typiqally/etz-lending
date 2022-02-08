using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ETZ.Lending.Domain.Abstractions.Models;
using ETZ.Lending.Domain.Abstractions.Services;
using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Persistence.Abstractions.Repositories;

namespace ETZ.Lending.Domain.Services
{
    public class LentProductDomainService : ILentProductDomainService
    {
        private static readonly string[] IncludeProperties = { nameof(LentProduct.Product) };

        private readonly IMapper _mapper;
        private readonly ILentProductRepository _repository;

        public LentProductDomainService(IMapper mapper, ILentProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<LentProduct>> GetAllAsync()
        {
            var lentProduct = await _repository.AllToListAsync(null, IncludeProperties);

            return _mapper.Map<IEnumerable<LentProduct>>(lentProduct);
        }

        public async Task<LentProduct> GetByIdAsync(int id)
        {
            var product = await _repository.SingleOrDefaultAsync(p => p.Id == id, IncludeProperties);

            return _mapper.Map<LentProduct>(product);
        }

        public async Task<LentProduct> LendProductAsync(LentProduct lentProduct)
        {
            var entity = _mapper.Map<LentProductEntity>(lentProduct);

            var created = await _repository.CreateAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<LentProduct>(created);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}