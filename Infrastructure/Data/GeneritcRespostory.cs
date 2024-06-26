﻿using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GeneritcRespostory<T> : IGenericRepository<T> where T :BaseEntity
    {
        private readonly StoreContext _context;
        public GeneritcRespostory(StoreContext context)
        {
                _context=context;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsQueryable().CountAsync();   
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
            
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsQueryable().FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAync(ISpecification<T> spec)
        {  
            return await ApplySpecification(spec).AsQueryable().ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(),spec) ;    

        }    
    }
}
