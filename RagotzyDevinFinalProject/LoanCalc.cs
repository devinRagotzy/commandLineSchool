using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    return this._personArray;
                }
            }
            return null;
        }

        public decimal[] CalcPaymentInfo(string person) {
            object[] cust = this.BSearch(this._loanArray.GetLength(0) - 1, person);
            if (cust == null) return null;
            return new decimal[3] { 1.1m, 1.2m, 1.3m };

        }
    }
}
