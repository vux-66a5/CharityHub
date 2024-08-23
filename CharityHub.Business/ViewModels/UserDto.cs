using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityHub.Business.ViewModels
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string  Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
