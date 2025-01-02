using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public List<Product> XemDanhSach()
        {
            return _productRepository.XemDanhSach();
        }
    }
}
