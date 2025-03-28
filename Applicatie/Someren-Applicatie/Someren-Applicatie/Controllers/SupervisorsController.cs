using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using Someren_Applicatie.Repositories;
using Someren_Applicatie.Repositories.Supervisors;
using Someren_Applicatie.Repositories.Lecturers;

namespace Someren_Applicatie.Controllers
{
    [Route("Supervisors")] //delete if no worky
    public class SupervisorsController : Controller
    {
        private readonly ISupervisorsRepository _supervisorsRepository;
        private readonly ILecturersRepository _lecturersRepository;

        public SupervisorsController(ISupervisorsRepository supervisorsRepository, ILecturersRepository lecturersRepository)
        {
            _supervisorsRepository = supervisorsRepository;
            _lecturersRepository = lecturersRepository;
        }
    }
}