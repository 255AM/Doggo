using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class DogsController : Controller
    {
        // GET: DogController
        //*********************GET ALL**************************//
        [Authorize]
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);

            return View(dogs);
        }
        
        
        // GET: WalkersController/Create
        //*********************CREATE******************8888888888888//
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            
            {
                _dogRepo.AddDog(dog);
                return RedirectToAction(nameof(Index));
            }
            
        }

        //*********************GET DETSIS*************************//
        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }
        //****************************************EDIT*********************//
        // GET: Owners/Edit/5
        public ActionResult Edit(int id)
        {

            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            else if(dog.OwnerId == GetCurrentUserId())
            {
                return View(dog);
            }
            else
            {
                return NotFound();
            }

            
        }

        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
           
            {
                _dogRepo.UpdateDog(dog);

                return RedirectToAction("Index");
            }
           
        }
        //*****************************************DELETE****************//
        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            if (dog.OwnerId == GetCurrentUserId())
            {
                return View(dog);
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST: Owners/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepo.DeleteDog(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public DogsController(IDogRepository DogRepository)
        {
            _dogRepo = DogRepository;
        }
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
