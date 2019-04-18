using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/******************************************************************************************
 * Devin Ragotzy Assignment #4
 * 04/29/19
 * ***************************************************************************************
 *
 * BUSSINES POINT OF VIEW
 *  An app that allows a business 
 * ***************************************************************************************
 * 
 * CLASSROOM POINT OF VIEW
 *  Read a data file and populate a 2 dimensional array (name, loan amount, years) 
 *  
 *          
 *  Properties:
 *      selectedIndex -
 *          used to
 *          
 *  Extras:
 *      
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
                        if (selectedIndexInt < 6) {
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
                                Console.WriteLine("Press Enter to continue");
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
                                        Console.WriteLine(fmtIt.Error("No amount found press enter"));
                                        Console.ReadLine();
                                        break;
                                    }
                                    // TODO
                                    foreach (string name in names) {
                                        Console.WriteLine(name);
                                    }
                                } catch (Exception err) {
                                    Console.WriteLine(err.ToString() + fmtIt.Error("You must enter a numeric amount ex 100.00"));
                                }
                                Console.WriteLine(EOL._ + "Press Enter to continue");
                                Console.ReadLine();
                                break;
                            // sort first name
                            case 2:
                                loans.QSort(0, loans.LoanInfo.GetLength(0));
                                // CHECK
                                Console.Write(fmtIt.Display(loans.LoanInfo));
                                Console.WriteLine(EOL._ + "Press Enter to continue");
                                Console.ReadLine();
                                break;
                            // sort last name
                            case 3:
                                loans.QSort(0, loans.LoanInfo.GetLength(0), true);
                                // CHECK
                                Console.Write(fmtIt.Display(loans.LoanInfo));
                                Console.WriteLine(EOL._ + "Press Enter to continue");
                                Console.ReadLine();
                                break;
                            // Calc payments
                            case 4:
                                Console.WriteLine("Enter a name to search customers by");
                                string person = Console.ReadLine();
                                
                                Console.WriteLine("Enter the rate customer must pay");
                                string rateStr = Console.ReadLine();
                                try {
                                    decimal rateDec = decimal.Parse(rateStr);
                                    (object[] payInfo, object[] custLoan) = loanCalc.CalcPaymentInfo(person, rateDec);
                                    Console.WriteLine(fmtIt.Display(payInfo, custLoan));
                                    if (custLoan == null) {
                                        Console.WriteLine(fmtIt.Error("No customer found press enter"));
                                        Console.ReadLine();
                                        break;
                                    }
                                } catch (Exception err) {
                                    Console.WriteLine(err.ToString() + fmtIt.Error("You must enter a decimal for the rate"));
                                }

                                Console.WriteLine(EOL._ + "Press Enter to continue");
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
