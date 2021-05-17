using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {
        // GET: Owner
        // GET: Walkers
        //*********************GET All*************************//
        public ActionResult Index()
        {
            List<Owner> owners = _ownerRepo.GetAllOwners();

            return View(owners);

        }


        //*********************create*************************//
        //GET: Owner/Create
        // GET: Owners/Create
        // GET: Owners/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OwnerFormViewModel ownerformviewmodel)
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();
            Neighborhoods = neighborhoods;
            OwnerFormViewModel vm = new OwnerFormViewModel();
            {
                Neighborhoods = neighborhoods;
            }
            try
            {
                _ownerRepo.AddOwner(ownerformviewmodel.Owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View(vm);
            }
        }
        //*********************GET DETS88*************************//
        // GET: WalkersController/Details/5
        // GET: Owners/Details/5
        public ActionResult Details(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(owner.Id);
            List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);

            ProfileViewModel vm = new ProfileViewModel()
            {
                Owner = owner,
                Dogs = dogs,
                Walkers = walkers
            };

            return View(vm);
        }
        //*********************edit*************************//
        public ActionResult Edit(int id)
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();
            Owner owner = _ownerRepo.GetOwnerById(id);

            OwnerFormViewModel tr = new OwnerFormViewModel()
            {
                Neighborhoods = neighborhoods,
                Owner = owner
            };

            return View(tr);
        }

        // POST: Owners/Edeit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OwnerFormViewModel ownerformviewmodel)
        {
            
            
            try
            {
                _ownerRepo.UpdateOwner(ownerformviewmodel.Owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();
                OwnerFormViewModel tr = new OwnerFormViewModel()
                {
                    Neighborhoods = neighborhoods,
                    
                };
                return View(tr);
            }
        }

        //*********************delete*************************//
        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner owner)
        {
            try
            {
                _ownerRepo.DeleteOwner(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(owner);
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            Owner owner = _ownerRepo.GetOwnerByEmail(viewModel.Email);

            if (owner == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, owner.Id.ToString()),
        new Claim(ClaimTypes.Email, owner.Email),
        new Claim(ClaimTypes.Role, "DogOwner"),
    };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Dogs");
        }

        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        public Owner Owner { get; private set; }
        public List<Neighborhood> Neighborhoods { get; private set; }



        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public OwnersController(IOwnerRepository ownerRepository,
                                IDogRepository dogRepository,
                                IWalkerRepository walkerRepository,
                                INeighborhoodRepository neighborhoodRepository)
                                
        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }



    }


}
