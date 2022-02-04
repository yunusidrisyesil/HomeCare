using HomeTechRepair.Models.Payment;

namespace HomeTechRepair.Services.Payment
{
    public interface IPaymentService
    {
        public InstallmentModel CheckInstalment(string binNumber, decimal Price);
        public PaymentResponseModel Pay(PaymentModel model);
    }
}
