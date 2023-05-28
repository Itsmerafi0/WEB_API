using API.Contracts;
using API.Models;
using API.ViewModels.Educations;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels;
using API.ViewModels.Others;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : BaseController<University, UniversityVM>
    {
        private readonly IUniveristyRepository _universityRepository;
        private readonly IMapper<University, UniversityVM> _mapper;
        public UniversityController(IUniveristyRepository universityRepository, IMapper<University, UniversityVM> mapper) : base(universityRepository, mapper)
        {
            _universityRepository = universityRepository;

            _mapper = mapper;
        }

   }

}
