using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Message
    {
        public int Id { get; set; }
        public User Sender { get; set; }
        public Chat Chat { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
