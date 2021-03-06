using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using _0_Framework;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

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
            var operationResult = new OperationResult();
            var productCategory = _productCategoryRepository.GetBy(command.Id);

            if (productCategory == null)
            {
                return operationResult.Failed("رکورد مد نظر یافت نشد");
            }

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operationResult.Failed("امکان ثبت رکورد با نام تکراری وجود ندارد. لطفا مجدد تلاش فرمایید");
            }

            productCategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt,
                command.PictureTitle, command.Keywords, command.Slug, command.MetaDescription);

            _productCategoryRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public OperationResult unVisible(long id)
        {
            var operationResult = new OperationResult();
            var productCategory = _productCategoryRepository.GetBy(id);
            if (productCategory == null)
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }

            productCategory.Delete();
            _productCategoryRepository.SaveChanges();

            return operationResult.Succdded();
        }

        public OperationResult Visible(long id)
        {
            var operationResult = new OperationResult();
            var productCategory = _productCategoryRepository.GetBy(id);
            if (productCategory == null)
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }

            productCategory.Restore();
            _productCategoryRepository.SaveChanges();

            return operationResult.Succdded();
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