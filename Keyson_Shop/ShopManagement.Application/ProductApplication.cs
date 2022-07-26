using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(ProductCreateModel command)
        {
            var operationResult = new OperationResult();

            if (_productRepository.Exists(x => x.Name == command.Name))
            {
                return operationResult.Failed(OperationMessages.Duplicate);
            }

            var slug = GenerateSlug.Slugify(command.Slug);
            _productRepository.Create(new Product(command.Name, command.Code, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Description, command.ShortDescription,
                command.Keywords, command.MetaDescription, command.CategoryId, slug));
            _productRepository.SaveChanges();


            return operationResult.Succdded();
        }

        public OperationResult Edit(ProductEditModel command)
        {
            var operationResult = new OperationResult();

            var product = _productRepository.GetBy(command.Id);
            if (product == null)
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operationResult.Failed(OperationMessages.Duplicate);
            }

            var slug = GenerateSlug.Slugify(command.Slug);

            product.Edit(command.Name, command.Code, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Description, command.ShortDescription,
                command.Keywords, command.MetaDescription, command.CategoryId, slug);

            _productRepository.SaveChanges();


            return operationResult.Succdded();
        }

        public ProductEditModel GetDetailBy(long id)
        {
            return _productRepository.GetDetailBy(id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}