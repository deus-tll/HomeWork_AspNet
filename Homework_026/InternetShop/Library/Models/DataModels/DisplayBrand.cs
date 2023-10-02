using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DataModels
{
    public class DisplayBrand
    {
        public required Brand Brand { get; set; }
        public int Count { get; set; }
    }
}
