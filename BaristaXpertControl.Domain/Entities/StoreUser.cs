using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaristaXpertControl.Domain.Entities
{
    public class StoreUser
    {
        public int Id { get; set; } 
        public string Username { get; set; } 
        public int StoreId { get; set; } 
        public Store Store { get; set; } 
    }
}
