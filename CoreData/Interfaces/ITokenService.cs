using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreData.Entities.Identity;

namespace CoreData.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}