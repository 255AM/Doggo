
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Repositories;
using DogGo.Models;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IOwnerRepository _ownerRepo;
        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository,
                                 IOwnerRepository ownerRepository)
        {
            _walkerRepo = walkerRepository;
            _ownerRepo = ownerRepository;
        }

        // GET: WalkersController
        // GET: Walkers
        //*********************GET all*************************//
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();
            if (ownerId == 0)
            {
                List<Walker> allWalkers = _walkerRepo.GetAllWalkers();
                return View(allWalkers);
            }

            //pulls in the entire owner object(which has a neighborhoodId on it)
            Owner currentOwner = _ownerRepo.GetOwnerById(ownerId);

            //this code will get all the walkers in the Walker table
            //convert it to a list and pass it off to the view
            List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(currentOwner.NeighborhoodId);



            return View(walkers);


        }
        //*********************GET DETSIS*************************//
        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }
        //*********************GET create*************************//
        //GET: Owner/Create
        public ActionResult Create()
        {
            return View();

        }

        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walker walker) { 
            try
            {
                _walkerRepo.AddWalker(walker);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(walker);
            }
        }
        //****************************************EDIT*********************//
        // GET: Owners/Edit/5
        public ActionResult Edit(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }

        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Walker walker)
        {

            {
                _walkerRepo.UpdateWalker(walker);

                return RedirectToAction("Index");
            }

        }
        //*********************GET DElete*************************//
        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            return View(walker);
        }

        // POST: Owners/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Walker walker)
        {
            try
            {
                _walkerRepo.DeleteWalker(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(walker);
            }
        }



        private int GetCurrentUserId()
        {

            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                id = "0";
            }
            return int.Parse(id);
        }

    }
}
