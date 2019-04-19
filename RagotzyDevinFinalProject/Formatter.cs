﻿using System;
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
                        msg += temp.ToString("c").PadLeft(9);
                    } else {
                        decimal temp = (decimal)arr[i, j];
                        msg += temp.ToString().PadLeft(10);
                    }
                }
                msg += EOL._ + "Press Enter to continue";
            }
            return msg;
        }

        // search display 
        public String Display(List<string> names) {
            String msg = Output.Blue().Bold().Text("Here is the list of all customers who have that size loan") + EOL._
                + Output.Green().Underline().Text("Name") + EOL._;
            foreach (string name in names) {
                msg += name + EOL._ + "Press Enter to continue";
            }
            return msg;
        }

        // payment info display
        public String Display(object[] payInfo, object[] loanInfo) {
            // initialize so we can toString currency the decimals and keep everthing short
            string name = (string)loanInfo[0];
            decimal lIntrest = Convert.ToDecimal(payInfo[0]);
            decimal total = Convert.ToDecimal(payInfo[1]);
            decimal mPay = Convert.ToDecimal(payInfo[2]);
            String msg =
                Output.Blue().Bold().Text($"Here is all the payment information about {Output.Yellow().Text((string)loanInfo[0])} we could find") + EOL._

                + Output.White().Underline().Text("Name".PadRight(15) + "Loan Interest".PadLeft(2)
                    + "Total Due".PadLeft(14) + "Montly Payment".PadLeft(16)) + EOL._

                + Output.Green().Text(name.PadRight(15) + lIntrest.ToString("c").PadLeft(13)
                    + total.ToString("c").PadLeft(14) + mPay.ToString("c").PadLeft(16));
            return msg + EOL._ + "Press Enter to continue";
        }

        public String Error(string msg) {
            return Output.Bold().Red().Text(msg) + EOL._ + "Press Enter to continue";
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
