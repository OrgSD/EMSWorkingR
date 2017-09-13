using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMS_IMD.Areas.SA.Models.BEL;

namespace EMS_IMD.Models
{
    public class LoginRegistrationModel : UserInRoleBEL
    {

        public string UserID { get; set; }
        public string Password { get; set; }



    
    }
}