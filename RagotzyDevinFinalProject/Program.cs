using System;
using System.Collections.Generic;
/******************************************************************************************
 * Devin Ragotzy Final Project
 * 04/29/19
 * ***************************************************************************************
 *
 * BUSINESS POINT OF VIEW
 *  An app that allows a bank to keep track of customers with loans.  App opens with a menu
 *  that allows user to choose between six options.  User search by loan amount to find names, 
 *  they can choose print all customers information unsorted or sorted by first or last name. 
 *  Calculation of total amount and monthly payments as well as total interest earned for life
 *  of the loan.  All output is formatted and colored for easy reading.
 * ***************************************************************************************
 * 
 * CLASSROOM POINT OF VIEW
 *  Read a data file and populate a 2 dimensional array (name, loan amount, years) this array
 *  drives all the functionality of the app.  Using a case statement the program tracks user
 *  input from the keyboard, up/down arrow, and enter are the important ones.  once an option
 *  is selected one of two business teir classes handels the sorting or calculation.  once the 
 *  calculation is complete the Formatter class (presentation) formats the output.  We use a 
 *  linear search, a quicksort and binary search to sort and find customers in the array. 
 *  Once sorted we can search the array for a customer to give a more detailed loan report.
 *  This contains total interest accumulated over the term at a user specified rate, the total
 *  interest plus principal and monthly payments. Finally there is also a quit option that breaks
 *  the loop and ends the program.
 *          
 *  Properties:
 *      selectedIndexInt -
 *          used to track users selection 
 *      CursorVisible - 
 *          since I want the menu to be used with arrow keys the cursor only shows up when typing
 *          is required.
 *      ConsoleKey - 
 *          an enum of every key on a keyboard used in the switch statement to match the key
 *          pressed
 *      
 *  Methods:
 *      Clear -
 *          clears the entire terminal screen
 *      Split -
 *          using the signature of an array of strings and an options object i split on more
 *          than just a single char
 *      GetLength -
 *          returns the length int for the specified dimension of the array
 *      GetValue -
 *          returns the value at the specified dimension and index of array
 *      SetValue -
 *          sets the value at the specified dimension and index of array
 *  Classes:
 *      Formatter--
 *          ShowOptions -
 *              keeps track of the selected index visualy, the index is passed to it but it is responsible
 *              for moving the colored line of text from index to index of the options array
 *          Display -
 *              an overloaded method with signatures object[,], List<string>, and object[,] object[,]
 *              they all return a formated colored string
 *          GenerateRGBValue -
 *              using a sine wave and shifting R by 0, G by 2 and B by 4 you get a even changing of
 *              each value when run in a for loop (the index is a param) the resulting value has to 
 *              be normalized out to a number between 0 and 256
 *      LoanCalc -
 *          BSearch -
 *              binary search splits the array in half until you find your match, does not work if unsorted
 *          CalcPaymentInfo -
 *              calculates interest total, total due and montly payments
 *      ArrayOps -
 *          Constructor - 
 *              is important because it opens the file and initializes the array with the contents of the file
 *          Qsort -
 *              sorts the array by first name and last name, overloaded, uses quicksort algorithm 
 *          Search - 
 *              just a single for loop to find if there is a matching loan amount and adds it to a list
 *  Extras:
 *      Imported a package called Crayon to color the console more easily its main class, Output is
 *      how i interact with it. It uses chainable methods to add color or styles to a string with ascii 
 *      color codes. Using a sine wave and the sine func i was able to get rgb values to increment 
 *      and decrement in a uniform way so i could color the text output like a rainbow yay!! 
******************************************************************************/
namespace RagotzyDevinFinalProject {
    class Program {
        static void Main(string[] args) {

            bool quit = false;
            int selectedIndexInt = 0;

            Formatter fmtIt = new Formatter();

            ArrayOperations loans = new ArrayOperations();
            LoanCalculator loanCalc = new LoanCalculator(loans.LoanInfo);
            
            while(!quit) {
                Console.CursorVisible = false;
                Console.Clear();

                Console.Write(fmtIt.ShowOptions(selectedIndexInt));
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.DownArrow:
                        if (selectedIndexInt < 5) {
                            selectedIndexInt++;
                        }
                        fmtIt.ShowOptions(selectedIndexInt);
                        break;
                    case ConsoleKey.UpArrow:
                        if (selectedIndexInt > 0) {
                            selectedIndexInt--;
                        }
                        fmtIt.ShowOptions(selectedIndexInt);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.CursorVisible = true;

                        switch (selectedIndexInt) {
                            // Display
                            case 0:
                                Console.WriteLine(fmtIt.Display(loans.LoanInfo));
                                Console.ReadLine();
                                break;
                            // search
                            case 1:
                                Console.WriteLine("Enter an amount to search loans by");
                                string amtStr = Console.ReadLine();
                                try {
                                    decimal amtDec = decimal.Parse(amtStr);
                                    List<string> names = loans.Search(amtDec);
                                    // none found
                                    if (names.Count < 1) {
                                        Console.Clear();
                                        Console.WriteLine(fmtIt.Error("No customer found press enter"));
                                        Console.ReadLine();
                                        break;
                                    }
                                    // TODO
                                    Console.Write(fmtIt.Display(names));
                                } catch {
                                    Console.WriteLine(fmtIt.Error("You must enter a numeric amount ex 100.00"));
                                    Console.ReadLine();
                                    break;
                                }
                                Console.ReadLine();
                                break;
                            // sort first name
                            case 2:
                                loans.QuickSort(loans.LoanInfo, 0, loans.LoanInfo.GetLength(0) - 1);
                                // CHECK
                                Console.Write(fmtIt.Display(loans.LoanInfo));
                                Console.ReadLine();
                                break;
                            // sort last name
                            case 3:
                                loans.QuickSortLast(loans.LoanInfo, 0, loans.LoanInfo.GetLength(0) - 1);
                                Console.Write(fmtIt.Display(loans.LoanInfo));
                                Console.ReadLine();
                                break;
                            // Calc payments
                            case 4:
                                Console.WriteLine("Enter a name to search customers by");
                                string person = Console.ReadLine();
                                
                                Console.WriteLine("Enter the rate customer must pay");
                                string rateStr = Console.ReadLine();
                                try {
                                    Console.Clear();
                                    decimal rateDec = decimal.Parse(rateStr);
                                    Payment payment = loanCalc.CalcPaymentInfo(person, rateDec);
                                    if (payment == null) {
                                        Console.WriteLine(fmtIt.Error("No customer found press enter"));
                                        Console.ReadLine();
                                        break;
                                    }
                                    Console.WriteLine(fmtIt.Display(payment));
                                } catch {
                                    Console.WriteLine(fmtIt.Error("You must enter a decimal for the rate"));
                                    Console.ReadLine();
                                    break;
                                }
                                Console.ReadLine();
                                break;
                            default:
                                quit = true;
                                break;
                        }
                        break;
                    case ConsoleKey.Escape:
                        quit = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
