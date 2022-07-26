using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(ProductPictureCreateModel command)
        {
            var operationResult = new OperationResult();

            _productPictureRepository.Create(new ProductPicture(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle));
            _productPictureRepository.SaveChanges();

            return operationResult.Succdded();
        }

        public OperationResult Edit(ProductPictureEditModel command)
        {
            var operationResult = new OperationResult();

            try
            {
                var ProductPicture = _productPictureRepository.GetBy(command.Id);
                ProductPicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
                _productPictureRepository.SaveChanges();
                return operationResult.Succdded();
            }
            catch
            {
                return operationResult.Failed("عملیات با موفقیت انجام نشد لطلفا برای برسی با مدیر سیستم صحبت کنید");
            }

        }

        public OperationResult Delete(long id)
        {
            var operationResult = new OperationResult();

            if (!_productPictureRepository.Exists(x => x.Id == id))
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }

            var ProductPicture = _productPictureRepository.GetBy(id);
            ProductPicture.Delete();
            _productPictureRepository.SaveChanges();
            return operationResult.Succdded();

        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();

            if (!_productPictureRepository.Exists(x => x.Id == id))
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }

            var ProductPicture = _productPictureRepository.GetBy(id);
            ProductPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel command)
        {
           return _productPictureRepository.Search(command);
        }

        public ProductPictureEditModel GetDetailBy(long id)
        {
            return _productPictureRepository.GetDetailBy(id);
        }
    }
}
