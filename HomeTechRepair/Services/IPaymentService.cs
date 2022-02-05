using HomeTechRepair.Models.Payment;

namespace HomeTechRepair.Services
{
    public interface IPaymentService
    {
        public InstallmentModel CheckInstalment(string binNumber, decimal Price);
        public PaymentResponseModel Pay(PaymentModel model);
    }
}
