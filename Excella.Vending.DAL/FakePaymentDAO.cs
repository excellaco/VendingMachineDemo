namespace Excella.Vending.DAL
{
    public class FakePaymentDAO : IPaymentDAO
    {
        private int balance;

        public void SavePayment(int amount)
        {
            balance += amount;
        }

        public int Retrieve()
        {
            return balance;
        }
    }
}
