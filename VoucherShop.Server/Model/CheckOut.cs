namespace VoucherShop.Server.Model
{
    public class CheckOut
    {

        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int CartID { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
