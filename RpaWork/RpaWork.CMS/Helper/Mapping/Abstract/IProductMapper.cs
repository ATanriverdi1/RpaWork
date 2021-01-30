using RpaWork.CMS.Models.ProductDTOs;
using RpaWork.Entities;

namespace RpaWork.CMS.Helper.Mapping.Abstract
{
    public interface IProductMapper
    {
        Product MapFromProductVM(ProductViewModel product);
        ProductViewModel MapFromProduct(Product product);
    }
}
