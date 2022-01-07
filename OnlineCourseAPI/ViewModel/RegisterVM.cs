using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.ViewModel
{
   
    public class RegisterVM
    {
       
        public string email { get; set; }
        public string  password { get; set; }


    }

}
