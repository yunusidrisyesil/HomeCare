using HomeTechRepair.Models.Entities;
using System.Collections.Generic;

namespace HomeTechRepair.Areas.Admin.ViewModels
{
    public class AppointmemtViewModel
    {
        //private readonly MyContext _dbContex;
        //public AppointmemtViewModel(MyContext dbContext)
        //{
        //    _dbContex = dbContext;
        //} 
        public static readonly IEnumerable<Appointment> Appointments = new[] {
            new Appointment { }
            //appointments from data will come a list
            };
    }

    } 
           