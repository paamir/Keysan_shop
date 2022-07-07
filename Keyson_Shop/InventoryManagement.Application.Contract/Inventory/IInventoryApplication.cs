using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(InventoryCreateModel command);
        OperationResult Edit(InventoryEditModel command);
        OperationResult Increase(InventoryIncreaseModel command);
        OperationResult Reduction(InventoryReductionModel command);
        List<InventoryViewModel> Search(InventorySearchModel command);
        InventoryEditModel GetDetail(long id);

    }
}
