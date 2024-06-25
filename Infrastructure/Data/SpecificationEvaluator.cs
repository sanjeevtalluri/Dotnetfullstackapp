using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreData.Entities;
using CoreData.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, 
        ISpecification<TEntity> spec){
            var query = inputQuery;
            if(spec.Criteria != null){
                query = query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query,(curr,include)=>curr.Include(include));
            return query;
        }
        
    }
}