using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;



namespace Domain.Models.User
{
	public class User : Entity
	{
		public string Document { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; } = true;
        public string PassWord { get; set; }
        public string login { get; set; }

        public List<UserRol> UserRol { get; set; }
    }
}