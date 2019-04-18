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

        public void Search(string want) {
            object[] person = new object[3];
            bool found = false;
            for (int i = 0; i < this._loanArray.GetLength(0); i++) {
                string curr = (string)this._loanArray[i, 0];
                if (curr == want) {
                    found = true;
                    for (int j = 0; j < 3; j++) {
                        person[j] = this._loanArray.GetValue(i, j);
                    }
                }
            }
            this._personArray = found ? person : null;
        }
    }
}
