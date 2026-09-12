using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fix.Shared.Domain.Models.Entities;


namespace Fix.Authentication.Application.Authentication.Commands.AddUser
{
   

       public class AddUserCommandResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        
        public UserRole UserRole { get; set; }
    }

     
}
