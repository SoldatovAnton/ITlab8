namespace ElectricsOnlineWebApp.Models
{
    public class CartBasket
    {
        public CartBasket(int pid, string pName, decimal unitPrice, int quantity)
        {
            PID = pid;
            PName = pName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public int PID { get; set; }

        public string PName { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        public override string ToString()
        {
            return PName + " " + Quantity;
        }
    }
}