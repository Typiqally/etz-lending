using System;
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
        private static readonly string[] IncludeProperties = { nameof(LentProduct.Product), $"{nameof(LentProduct.Product)}.{nameof(Product.Image)}" };

        private readonly IMapper _mapper;
        private readonly ILentProductRepository _lentProductRepository;
        private readonly IProductRepository _productRepository;

        public LentProductDomainService(IMapper mapper, ILentProductRepository lentProductRepository, IProductRepository productRepository)
        {
            _mapper = mapper;
            _lentProductRepository = lentProductRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<LentProduct>> GetAllAsync()
        {
            var lentProduct = await _lentProductRepository.AllToListAsync(null, IncludeProperties);

            return _mapper.Map<IEnumerable<LentProduct>>(lentProduct);
        }

        public async Task<LentProduct> GetByIdAsync(int id)
        {
            var product = await _lentProductRepository.SingleOrDefaultAsync(p => p.Id == id, IncludeProperties);

            return _mapper.Map<LentProduct>(product);
        }

        public async Task<LentProduct> LendProductAsync(LentProduct lentProduct)
        {
            var product = await _productRepository.FindAsync(lentProduct.ProductId);
            if (product == null) return null;

            var entity = _mapper.Map<LentProductEntity>(lentProduct);

            var created = await _lentProductRepository.CreateAsync(entity);

            var affected = await _lentProductRepository.SaveChangesAsync();
            if (affected != 1) return null;

            product.IsLent = true;
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return _mapper.Map<LentProduct>(created);
        }

        public async Task<LentProduct> UpdateAsync(int id, DateTime expiredAt)
        {
            var entity = await _lentProductRepository.FindAsync(id);
            entity.ExpiredAt = expiredAt;

            _lentProductRepository.Update(entity);
            await _lentProductRepository.SaveChangesAsync();

            return _mapper.Map<LentProduct>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var lentProduct = await _lentProductRepository.SingleOrDefaultAsync(p => p.Id == id, IncludeProperties);
            if (lentProduct == null) return;

            await _lentProductRepository.DeleteAsync(lentProduct.Id);

            var affected = await _lentProductRepository.SaveChangesAsync();
            if (affected != 1) return;

            lentProduct.Product.IsLent = false;
            _productRepository.Update(lentProduct.Product);
            await _productRepository.SaveChangesAsync();
        }
    }
}