using OwnersAndPets.App_Start;
using OwnersAndPets.Interface;
using OwnersAndPets.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OwnersAndPets.Controllers
{
    public class PetController : Controller
    {
        private readonly IOwnerAccessor _ownerAccessor;

        public PetController()
        {
            _ownerAccessor = ObjectLocator.GetInstance<IOwnerAccessor>(); 
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cats()
        {
            List<OwnerModel> owners = _ownerAccessor.GetCatOwners();

            return View(owners);
        }
    }
}