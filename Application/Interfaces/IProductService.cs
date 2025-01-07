using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Product GetById(int id);
        ResponeActionResult Insert(Product item);
        ResponeActionResult Delete(int id);
        ResponeActionResult Update(Product item);
        ResponeActionResult GetPaged(ProductQuery query);
        ResponeActionResult DisapprovedItem(int ID);
        ResponeActionResult ApprovedItem(int ID);
        Product_Entity GetEntity(int Id);


    }
}
