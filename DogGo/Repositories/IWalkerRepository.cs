using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkerRepository
    {
        List<Walker> GetAllWalkers();
        Walker GetWalkerById(int id);
        void AddWalker(Walker walker);
        void UpdateWalker(Walker walker);
        void DeleteWalker(int WalkerId);
        List<Walker> GetWalkersInNeighborhood(int neighborhoodId);
    }
}