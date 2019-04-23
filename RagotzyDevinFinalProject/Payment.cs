using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagotzyDevinFinalProject
{

    // a dto so i dont have to keep track of array or tuple names/indexs
    class Payment
    {
        private string _name = "";
        private decimal _rate;
        private decimal _loanInter;
        private decimal _total;
        private decimal _numPay;
        private int _monthPay;
        private int _left;

        public string Name { get => _name; }
        public decimal Rate { get => _rate;  }
        public decimal LoanInterest { get => _loanInter; }
        public decimal TotalAmount { get => _total; }
        public decimal NumPayments { get => _numPay; }
        public int MonthlyPayment { get => _monthPay; }
        public int AmountLeft { get => _left; }

        public Payment(object[] pay, object[] loan)
        {
            _name = Convert.ToString(loan[0]);
            _loanInter = Convert.ToDecimal(pay[0]);
            _total = Convert.ToDecimal(pay[1]);
            _monthPay = Convert.ToInt32(pay[2]);
            _left = Convert.ToInt32(pay[3]);
            _rate = Convert.ToDecimal(pay[4]);
            _numPay = Convert.ToDecimal(pay[5]);
        }
    }
}
