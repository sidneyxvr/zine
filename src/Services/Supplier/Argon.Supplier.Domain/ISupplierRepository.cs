﻿using System.Threading.Tasks;

namespace Argon.Suppliers.Domain
{
    public interface ISupplierRepository
    {
        Task AddAsync(Supplier supplier);
    }
}
