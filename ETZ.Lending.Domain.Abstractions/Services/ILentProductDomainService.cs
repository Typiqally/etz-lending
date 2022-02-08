﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ETZ.Lending.Domain.Abstractions.Models;

namespace ETZ.Lending.Domain.Abstractions.Services
{
    public interface ILentProductDomainService
    {
        Task<IEnumerable<LentProduct>> GetAllAsync();
        Task<LentProduct> GetByIdAsync(int id);
        Task<LentProduct> LendProductAsync(LentProduct lentProduct);
        Task DeleteAsync(int id);
    }
}