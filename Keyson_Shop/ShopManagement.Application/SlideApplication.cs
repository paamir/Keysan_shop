using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(SlideCreateModel command)
        {
            var operationResult = new OperationResult();

            _slideRepository.Create(new Slide(command.Picture, command.PictureAlt, command.PictureTitle,
                command.IsDeleted, command.Title, command.Header, command.Text, command.ButtonText, command.Link));
            _slideRepository.SaveChanges();

            return operationResult.Succdded();
        }

        public OperationResult Edit(SlideEditModel command)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.GetBy(command.Id);

            slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle, command.IsDeleted, command.Title,
                command.Header, command.Text, command.ButtonText, command.Link);
            _slideRepository.SaveChanges();

            return operationResult.Succdded();
        }

        public OperationResult Delete(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.GetBy(id);
            if (slide == null)
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }
            slide.Delete();
            _slideRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.GetBy(id);
            if (slide == null)
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }
            slide.Restore();
            _slideRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public List<SlideViewModel> GetAll()
        {
           return _slideRepository.GetAll();
        }

        public SlideEditModel GetDetail(long id)
        {
            return _slideRepository.GetDetail(id);
        }
    }
}