using OwnersAndPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnersAndPets.Interface
{
    interface IOwnerAccessor
    {
        List<OwnerModel> GetCatOwners();
    }
}
