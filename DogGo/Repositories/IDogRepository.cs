using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        Dog GetDogById(int id);
        void AddDog(Dog dog);
        void UpdateDog(Dog dog);
        void DeleteDog(int DogId);
        List<Dog> GetDogsByOwnerId(int id);
    }
}
