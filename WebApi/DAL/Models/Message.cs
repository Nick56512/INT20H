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
        public string SenderId { get; set; }
        public virtual User Sender { get; set; }
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
