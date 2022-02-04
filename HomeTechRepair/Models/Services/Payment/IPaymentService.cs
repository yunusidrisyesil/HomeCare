using HomeTechRepair.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Models.Services.Payment
{
    public interface IPaymentService
    {
        public InstallmentModel CheckInstalment(string binNumber, decimal Price);
        public PaymentResponseModel Pay(PaymentModel model);
    }
}
