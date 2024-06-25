using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using CoreData.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product,ProductToReturnDto,string>
    {
        private readonly IConfiguration config;

        public ProductUrlResolver(IConfiguration config)
        {
            this.config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context){
            if(!string.IsNullOrEmpty(source.PictureUrl)){
                return config["ApiUrl"]+ source.PictureUrl;
            }
            return null;
        }
    }
}