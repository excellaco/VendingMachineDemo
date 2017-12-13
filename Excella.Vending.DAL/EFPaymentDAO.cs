using System.Data.Common;
using System.Linq;

namespace Excella.Vending.DAL
{
    public class EFPaymentDAO : IPaymentDAO
    {
        private readonly VendingMachineContext _context = new VendingMachineContext();

        /// <summary>
        /// The default constructor, which will use EF's default transaction handling
        /// </summary>
        public EFPaymentDAO() {}

        /// <summary>
        /// When passing in a transaction, this will force the EFPaymentDAO to use that Transaction.
        /// This forces the caller to handle all transaction operations such as commits or rollbacks.
        /// </summary>
        /// <remarks>
        /// This enables the EFPaymentDAO to be integration tested, 
        /// because we can properly set up and roll back the transaction. 
        /// </remarks>
        /// <param name="dbTransaction"></param>
        public EFPaymentDAO(DbTransaction dbTransaction)
        {
            _context.Database.UseTransaction(dbTransaction);
        }

        public int Retrieve()
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == 1);

            if (payment != null)
            {
                return payment.Value;
            }

            return 0;
        }

        public void SavePayment(int amount)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == 1);

            if (payment != null)
            {
                payment.Value += amount;
                _context.SaveChanges();
            }
        }

        public void SavePurchase()
        {
            const int PURCHASE_COST = 50;
            var payment = _context.Payments.FirstOrDefault(p => p.Id == 1);

            if (payment != null)
            {
                payment.Value -= PURCHASE_COST;
                _context.SaveChanges();
            }
        }

        public void ClearPayments()
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == 1);

            if (payment != null)
            {
                payment.Value = 0;
                _context.SaveChanges();
            }
        }
    }
}
