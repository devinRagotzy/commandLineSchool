using System;
using System.Collections.Generic;
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

        // Whole list of loans
        public String Display(object[,] arr) {
            String msg = Output.Blue().Bold().Text("Here is the list of all Loans") + EOL._
                + Output.White().Underline().Text("Name".PadRight(18) + "Loan amount".PadLeft(9) + "Term".PadLeft(9)) + EOL._;
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < 3; j++) {
                    // we need this ugly mess to space the colums correctly
                    if (j == 0) {
                        string temp = (string)arr[i, j];
                        (byte r, byte g, byte b) = this.GenerateRGBvalue(i);
                        msg += Output.FromRgb(r, g, b).Text(temp.PadRight(18));
                    } else if (j == 1){
                        decimal temp = (decimal)arr[i, j];
                        msg += temp.ToString("c").PadLeft(11);
                    } else {
                        decimal temp = (decimal)arr[i, j];
                        msg += temp.ToString().PadLeft(9);
                    }
                }
                msg += EOL._;
            }
            return msg + EOL._ + "Press Enter to continue" + EOL._;
        }

        // search display 
        public String Display(List<string> names) {
            String msg = Output.Blue().Bold().Text("Here is the list of all customers who have that size loan") + EOL._
                + Output.Green().Underline().Text("Name") + EOL._;
            foreach (string name in names) {
                msg += name + EOL._;
            }
            return msg + EOL._ + "Press Enter to continue" + EOL._;
        }

        // payment info display
        public String Display(Payment pay) {
            string msg =
                Output.Blue().Bold().Text($"Here is all the payment information about {Output.Yellow().Text(pay.Name)} we could find") + EOL._

                + Output.White().Underline().Text("Name".PadRight(15) + "Loan Interest".PadLeft(2)
                    + "Total Due".PadLeft(14) + "Annual Interest".PadLeft(18)) + EOL._

                + Output.Green().Text(pay.Name.PadRight(15) + pay.LoanInterest.ToString("c").PadLeft(13)
                    + pay.TotalAmount.ToString("c").PadLeft(14) + pay.Rate.ToString("##%").PadLeft(18)) + EOL._
                    + "Month Number" + "Payment".PadLeft(11) + "Amount Left".PadLeft(14) + EOL._;
            int amtLeft = (int)pay.TotalAmount;
            for (int i = 0; i < (int)pay.NumPayments; i++)
            {
                if (i+1 == pay.NumPayments)
                {
                    amtLeft -= (pay.MonthlyPayment + pay.AmountLeft);
                    msg += $"month {i + 1}:".PadRight(14) + $"{(pay.MonthlyPayment + pay.AmountLeft).ToString("c")}"
                        + $"{amtLeft.ToString("c")}   {pay.AmountLeft.ToString()}".PadLeft(14) + EOL._;
                }
                else
                {
                    amtLeft -= pay.MonthlyPayment;
                    msg += $"month {i + 1}:".PadRight(16) + $"{pay.MonthlyPayment.ToString("c")}"
                        + $"{amtLeft.ToString("c")}".PadLeft(14) + EOL._;
                }
            }
            return msg + EOL._ + "Press Enter to continue";
        }

        public String Error(string msg) {
            return Output.Bold().Red().Text(msg) + EOL._ + EOL._ + "Press Enter to continue";
        }

        // uses the index and a sine wave to create incremental rgb values (make a rainbow!!!)
        private (byte, byte, byte) GenerateRGBvalue(int idx) {
            double freq = 0.8;

            byte r = Convert.ToByte(Math.Round(Math.Sin(freq * idx) * 127 + 128));
            byte g = Convert.ToByte(Math.Round(Math.Sin(freq * idx + 2) * 127 + 128));
            byte b = Convert.ToByte(Math.Round(Math.Sin(freq * idx + 4) * 127 + 128));

            return (r, g, b);
        }
    }
}
