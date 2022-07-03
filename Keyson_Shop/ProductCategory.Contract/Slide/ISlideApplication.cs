using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Slide
{
    public interface ISlideApplication
    {
        OperationResult Create(SlideCreateModel command);
        OperationResult Edit(SlideEditModel command);
        OperationResult Delete(long id);
        OperationResult Restore(long id);
        List<SlideViewModel> GetAll();
        SlideEditModel GetDetail(long id);
    }
}
