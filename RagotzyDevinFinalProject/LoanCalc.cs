using System;

namespace RagotzyDevinFinalProject {
    class LoanCalculator {

        private object[,] _loanArray;
        private object[] _personArray;

        public object[,] LoanArray {
            get => _loanArray;
            set => _loanArray = value;
        }
        public object[] Person {
            get => _personArray;
            set => _personArray = value;
        }

        public LoanCalculator(object[,] loans) {
            this._loanArray = loans;
        }

        private object[] BSearch(int num, string want) {
            object[] person = new object[3];
            int left = 0;
            int right = num - 1;
            while (left <= right) {
                int mid = (left + right) / 2;
                if (this._loanArray[mid, 0].ToString().CompareTo(want) < 0) {
                    left = mid + 1;
                } else if (this._loanArray[mid, 0].ToString().CompareTo(want) > 0) {
                    right = mid - 1;
                } else {
                    for (int i = 0; i < 3; i++) {
                        person[i] = this._loanArray.GetValue(mid, i);
                    }
                    this._personArray = person;
                    return this._personArray;
                }
            }
            return null;
        }

        // returns a tuple
        // the 0 index is the calculated loan info the second is original customer info
        public Payment CalcPaymentInfo(string person, decimal rate) {
            object[] cust = this.BSearch(this._loanArray.GetLength(0), person);

            if (cust == null) return null;

            decimal numPays = (decimal)cust[2] * 12;
            decimal loanInterest = (decimal)cust[1] * rate * (decimal)cust[2]; 
            decimal totalAmount = (decimal)cust[1] + loanInterest;
            int left = Convert.ToInt32(totalAmount) % (Convert.ToInt32(cust[2]) * 12);
            // decimal monthlyPayment = totalAmount / ((decimal)cust[2] * 12);
            int monthlyPayment = Convert.ToInt32(totalAmount) / (Convert.ToInt32(cust[2]) * 12);

            object[] pays = new object[] {
                loanInterest,
                totalAmount,
                monthlyPayment,
                left,
                rate,
                numPays
            };
            return new Payment(pays, cust);
        }
    }
}
