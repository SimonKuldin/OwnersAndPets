using OwnersAndPets.Models;
using System.Collections.Generic;

namespace OwnersAndPets.Interface
{
    public interface IOwnerAccessor
    {
        List<OwnerModel> GetCatOwners();
    }
}
