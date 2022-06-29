using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using _0_Framework;
using _0_Framework.Application;
using Domain.ProductCategoryAgg;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategoryModel command)
        {
            var operationresult = new OperationResult();


            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
            {
                return operationresult.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفا مجدد تلاش فرمایید");
            }

            var slug = GenerateSlug.Slugify(command.Slug);


            _productCategoryRepository.Create(new ProductCategory(
                command.Name,
                command.Description,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.Keywords,
                slug,
                command.MetaDescription
            ));
            _productCategoryRepository.SaveChanges();

            return operationresult.Succdded();
        }

        public OperationResult Edit(EditProductCategoryModel command)
        {
            var operationresult = new OperationResult();

            var productCategory = _productCategoryRepository.GetBy(command.Id);

            if (productCategory == null)
            {
                return operationresult.Failed("رکورد مد نظر یافت نشد");
            }

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operationresult.Failed("امکان ثبت رکورد با نام تکراری وجود ندارد. لطفا مجدد تلاش فرمایید");
            }

            productCategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt,
                command.PictureTitle, command.Keywords, command.Slug, command.MetaDescription);

            _productCategoryRepository.SaveChanges();
            return operationresult.Succdded();
        }

        public List<ProductCategoryViewModel> GetAll()
        {
            return _productCategoryRepository.GetAll().Select(x => new ProductCategoryViewModel()
            {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToShortDateString()
            }).ToList();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }


        public EditProductCategoryModel GetDetail(long id)
        {
            return _productCategoryRepository.GetDetailBy(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
           return _productCategoryRepository.GetProductCategories();
        }
    }
}