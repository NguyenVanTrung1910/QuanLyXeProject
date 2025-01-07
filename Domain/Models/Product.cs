using Domain.Querys.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class Product : CoreEntity
    {
        public string? Name { get; set; }
    }
}
