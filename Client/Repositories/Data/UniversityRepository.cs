using API.Models;
using Client.Repositories.Interface;

namespace Client.Repositories.Data
{
    public class UniversityRepository : GeneralRepository<University, int>, IUniversityRepository
        {


            public UniversityRepository(string request = "University/") : base(request)
            {

            }



        }
    }
