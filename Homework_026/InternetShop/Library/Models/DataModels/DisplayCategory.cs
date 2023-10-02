using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DataModels
{
    public class DisplayCategory
    {
        public required Category Category { get; set; }
        public int Count { get; set; }
    }
}
