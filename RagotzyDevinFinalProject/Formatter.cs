using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// colors strings with escape codes for terminal
using Crayon;


namespace RagotzyDevinFinalProject {
    public static class EOL {
        public static string _ = Environment.NewLine;
    }

    class Formatter {

        private string[] _optionsArray;
        private int _lastIndexInt;
        // keeps track of uncolored string that needs to be put back 
        private string _tempHolderStr = "";

        public Formatter() {
            this._optionsArray = new string[6] {
                "Would you like to see all loans",
                "Would you like to search loans by amount for the name of debter",
                "Would you like to sort loans by first name",
                "Would you like to sort loans by last name",
                "Would you like to a report of monthly payments",
                "You can also Quit",
            };
        }

        // moves the selected item dependent on up and down arrows
        public String ShowOptions(int index) {
            if (this._tempHolderStr != "") {
                // convets colored output back to normal
                this._optionsArray[_lastIndexInt] = this._tempHolderStr;
            }
            this._tempHolderStr = this._optionsArray[index];
            // converts selected item to color with star
            this._optionsArray[index] = Output.Magenta().Bold().Text($"* {Output.Cyan().Text(this._optionsArray[index])}");
            this._lastIndexInt = index;
            return Output.Bold().Green().Text("Use up or down arrows to select then hit enter")
                + EOL._ + string.Join(EOL._, this._optionsArray);
        }

        public String Display(object[,] arr) {
            String msg = Output.Blue().Bold().Text("Here is the list of all Loans") + EOL._
                + Output.Green().Text("Name" + "Loan amount" + "Term") + EOL._;
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < 3; j++) {
                    if (j == 0) {
                        msg += arr[i, j];
                    } else {
                        msg += arr[i, j];
                    }
                }
                msg += EOL._;
            }
            return msg;
        }

        public String Display(List<string> col) {
            String msg = Output.Blue().Bold().Text("Here is the list of all customers who have that size loan") + EOL._
                + Output.Green().Text("Name") + EOL._;
            foreach (string name in col) {
                msg += name + EOL._;
            }
            return msg;
        }

        public String Error(string msg) {
            return Output.Bold().Red().Text(msg) + EOL._ + "Hit Enter to continue";
        }
    }
}
