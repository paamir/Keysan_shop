using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        ColleagueDiscountEditModel GetDetailBy(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel command);
        OperationResult Edit(ColleagueDiscountEditModel command);
        OperationResult Create(ColleagueDiscountCreateModel command);
        OperationResult Delete(long id);
        OperationResult Restore(long id);

    }
}
