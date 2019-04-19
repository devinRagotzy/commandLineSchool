using System;
using System.Collections.Generic;
using System.IO;


namespace RagotzyDevinFinalProject {

    class ArrayOperations {

        private object[,] _personInfoArray;

        public object[,] LoanInfo {
            get => this._personInfoArray;
        }

        public ArrayOperations() {
            this.OpenFile(Environment.CurrentDirectory + "..\\..\\..\\..\\loans.txt");
        }

        private void OpenFile(string fileLocation) {
            try {
                StreamReader inputFile = new StreamReader(fileLocation);
                // I could read the file and then initialize the array then do nested for loops
                // when i read to the end to get length its endofstream 
                this._personInfoArray = new object[8, 3];
                int index = 0;
                while (!inputFile.EndOfStream) {
                    string chunk = inputFile.ReadLine();
                    string[] infoArr = chunk.Split(new string[] { ", "}, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < infoArr.Length; i++) {
                        if (i == 0) {
                            this._personInfoArray[index, i] = (string)infoArr[i];
                        } else {
                            this._personInfoArray[index, i] = decimal.Parse((string)infoArr[i]);
                        }
                    }
                    index++;
                }
                inputFile.Close();
            } catch (Exception err) {
                Console.WriteLine(err.ToString());
            }
        }

        // search for decimal amount option 1 in case statement
        public List<string> Search(decimal want) {
            List<string> collected = new List<string>();
            for (int i = 0; i < this._personInfoArray.GetLength(0); i++) {
                decimal curr = (decimal)this._personInfoArray[i, 1];
                if (curr == want) {
                    collected.Add((string)this._personInfoArray.GetValue(i, 0));
                }
            }
            return collected;
        }

        private int SplitQSort(object[,] arr, int low, int numEle) {
            string pivot = (string)arr[numEle, 0];
            int idx = low - 1;
            for (int j = low; j < numEle; j++) {
                // 1 if second is larger -1 if first is smaller
                int ifSmaller = string.Compare((string)arr[j, 0], pivot);
                if (ifSmaller == -1) {
                    idx++;
                    // temp = arr[idx]
                    object[] first = new object[3];
                    for (int i = 0; i < 3; i++) {
                        if (i == 0) {
                            first[i] = (string)arr.GetValue(idx, i);
                        } else {
                            first[i] = (decimal)arr.GetValue(idx, i);
                        }
                    }
                    // arr[idx] = arr[j]
                    for (int i = 0; i < 3; i++) {
                        arr.SetValue(arr.GetValue(j, i), idx, i);
                    }
                    // arr[j] = temp;
                    for (int i = 0; i < 3; i++) {
                        arr.SetValue(first[i], j, i);
                    }
                }
            }

            // int temp1 = arr[idx + 1];       
            object[] temp = new object[3];
            for (int i = 0; i < 3; i++) {
                if (i == 0) {
                    temp[i] = (string)arr.GetValue(idx + 1, i);
                } else {
                    temp[i] = (decimal)arr.GetValue(idx + 1, i);
                }
            }

            // arr[idx + 1] = arr[numEle];
            for (int i = 0; i < 3; i++) {
                arr.SetValue(arr.GetValue(numEle, i), idx + 1, i);
            }

            // arr[numEle] = temp1;
            for (int i = 0; i < 3; i++) {
                arr.SetValue(temp[i], numEle, i);
            }
            

            return idx + 1;
        }

        // sorts for first name
        public void QSort(int low, int high) {
            int numEle = high - 1;
            if (low < numEle) {
                // move element by element moving pivot by recursivly calling QSort
                // the splitInx is the pivot and is swapped with all elements larger 
                // than it moved to the left of the array
                int splitInx = this.SplitQSort(this._personInfoArray, low, numEle);
                // recursivly checks elements to the left
                this.QSort(low, splitInx - 1);
                // recursivly take large 'half' and swap elements
                this.QSort(splitInx + 1, numEle);
            }
        }

        // overide for last name sort split and Q sort
        private int SplitQSort(object[,] arr, int low, int numEle, bool lastName) {
            string pivot = arr[numEle, 0].ToString().Split(' ')[1].Trim();
            int idx = low - 1;
            for (int j = low; j < numEle; j++) {
                // string to compare pivot to if smaller swaps left
                string compStr = arr[j, 0].ToString().Split(' ')[1].Trim();
                // 1 if second is larger -1 if first
                int ifSmaller = string.Compare(compStr, pivot, true);
                if (ifSmaller < 0) {
                    idx++;
                    // temp = arr[idx]
                    object[] first = new object[3];
                    for (int i = 0; i < 3; i++) {
                        if (i == 0) {
                            first[i] = (string)arr.GetValue(idx, i);
                        } else {
                            first[i] = (decimal)arr.GetValue(idx, i);
                        }
                    }
                    // arr[idx] = arr[j]
                    for (int i = 0; i < 3; i++) {
                        arr.SetValue(arr.GetValue(j, i), idx, i);
                    }
                    // arr[j] = temp;
                    for (int i = 0; i < 3; i++) {
                        arr.SetValue(first[i], j, i);
                    }
                }
            }

            // int temp1 = arr[idx + 1];       
            object[] temp = new object[3];
            for (int i = 0; i < 3; i++) {
                if (i == 0) {
                    temp[i] = (string)arr.GetValue(idx + 1, i);
                }
                else {
                    temp[i] = (decimal)arr.GetValue(idx + 1, i);
                }
            }

            // arr[idx + 1] = arr[numEle];
            for (int i = 0; i < 3; i++) {
                arr.SetValue(arr.GetValue(numEle, i), idx + 1, i);
            }

            // arr[numEle] = temp1;
            for (int i = 0; i < 3; i++) {
                arr.SetValue(temp[i], numEle, i);
            }


            return idx + 1;
        }

        public void QSort(int low, int high, bool lastName) {
            int numEle = high - 1;
            if (low < numEle) {
                // move element by element moving pivot by recursivly calling QSort
                // the splitInx is the pivot and is swapped with all elements larger 
                // than it moved to the left of the array
                int splitInx = this.SplitQSort(this._personInfoArray, low, numEle, lastName);
                // recursivly checks elements to the left
                this.QSort(low, splitInx - 1, lastName);
                // recursivly checks the elements to right of pivot
                this.QSort(splitInx + 1, numEle, lastName);
            }
        }
    }
}
