using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _ProductRepository;
        private readonly ResponeActionResult _responeActionResult;
        public int TotalRecord = 0;

        public ProductService(ResponeActionResult responeActionResult,IProductRepository productRepository) 
        {
            _ProductRepository = productRepository;
            _responeActionResult = responeActionResult;
        }

        public Product GetById(int id)
        {
            return _ProductRepository.GetById(id);
        }
        public ResponeActionResult ApprovedItem(int id)
        {
            _ProductRepository.ApprovedItem(id);
            _responeActionResult.ex_message = "Duyệt thành công";
            return _responeActionResult;
        }
        public ResponeActionResult DisapprovedItem(int id)
        {
            _ProductRepository.DisapprovedItem(id);
            _responeActionResult.ex_message = "Hủy duyệt thành công";
            return _responeActionResult;
        }


        public ResponeActionResult GetPaged(ProductQuery searchQuery)
        {
           
            List<Product_Entity> listTaiLieuThucTap = _ProductRepository.GetPaged(searchQuery);
            if (listTaiLieuThucTap == null)
            {
                _responeActionResult.Message($"Không tìm thấy bản ghi");
                return _responeActionResult;
            }
            _responeActionResult.data = listTaiLieuThucTap;
            _responeActionResult.draw = searchQuery.draw;
            _responeActionResult.recordsTotal = _ProductRepository.TotalRecord;
            _responeActionResult.recordsFiltered = _ProductRepository.TotalRecord;
            return _responeActionResult;
        }

        public ResponeActionResult Insert(Product item)
        {
            item.FillDataForInsert(1);
            _ProductRepository.Insert(item);
            _responeActionResult.ex_message = "Thêm thành công";
            _responeActionResult.data = new Product() { Id = item.Id };
            return _responeActionResult;
        }


        public ResponeActionResult Delete(int id)
        {
            _ProductRepository.DeleteById(id);
            _responeActionResult.ex_message = "Xóa thành công";
            return _responeActionResult;
        }

        public ResponeActionResult Update(Product item)
        {
            item.FillDataForUpdate(1);
            _ProductRepository.Update(item);
            _responeActionResult.ex_message = "Sửa thành công";
            _responeActionResult.data = new Product() { Id = item.Id };
            return _responeActionResult;
        }
        public Product_Entity GetEntity(int Id)
        {
            return _ProductRepository.GetEntity(Id);
        }



    }
}
