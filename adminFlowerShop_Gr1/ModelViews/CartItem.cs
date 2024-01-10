using adminFlowerShop_Gr1.Models;
namespace adminFlowerShop_Gr1.ModelViews
{
    public class CartItem
    {
        public TblProduct? product { get; set; }
        public int amount { get; set; }
        public double? TotalMoney => amount * product.Price.Value;
    }
}
