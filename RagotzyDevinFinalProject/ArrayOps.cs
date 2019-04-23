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

        private int Partition(object[,] arr, int left, int right) {
            string pivot = arr[right, 0].ToString().Split(' ')[0].Trim();
            int idx = left - 1;
            for (int j = left; j < arr.GetLength(0) - 1; j++) {
                if (arr[j, 0].ToString().CompareTo(pivot) < 0) {
                    idx++;
                    // temp = arr[right]
                    object[] temp = new object[3];
                    for (int i = 0; i < 3; i++) {
                        temp[i] = arr[idx, i];
                    }
                    // arr[right] = arr[left]
                    for (int i = 0; i < 3; i++) {
                        arr[idx, i] = arr[j, i];
                        //arr.SetValue(arr.GetValue(left, i), right, i);
                    }
                    // arr[left] = temp;
                    for (int i = 0; i < 3; i++) {
                        arr[j, i] = temp[i];
                        //arr.SetValue(temp[i], left, i);
                    }
                }
            }
            // temp = arr[right]
            object[] temp1 = new object[3];
            for (int i = 0; i < 3; i++) {
                temp1[i] = arr[idx + 1, i];
            }
            // arr[right] = arr[left]
            for (int i = 0; i < 3; i++) {
                arr[idx + 1, i] = arr[right, i];
                //arr.SetValue(arr.GetValue(left, i), right, i);
            }
            // arr[left] = temp;
            for (int i = 0; i < 3; i++) {
                arr[right, i] = temp1[i];
                //arr.SetValue(temp[i], left, i);
            }
            return idx + 1;
        }

        // sorts for first name
        public void QSort(object[,] arr, int left, int right) {
            if (left < right) {
                // move element by element moving pivot by recursivly calling QSort
                // the splitInx is the pivot and is swapped with all elements larger 
                // than it moved to the left of the array
                int pivot = this.Partition(arr, left, right);
                // recursivly checks elements to the left
                this.QSort(arr, left, pivot - 1);
                // recursivly take large 'half' and swap elements
                this.QSort(arr, pivot + 1, right);
            }
        }

        // overide for last name sort split and Q sort
        private int SplitQSort(object[,] arr, int left, int right, bool lastName) {
            string pivot = arr[right, 0].ToString().Split(' ')[1].Trim();
            int idx = left - 1;
            for (int j = left; j < arr.GetLength(0) - 1; j++) {
                if (arr[j, 0].ToString().Split(' ')[1].Trim().CompareTo(pivot) < 0) {
                    idx++;
                    // temp = arr[right]
                    object[] temp = new object[3];
                    for (int i = 0; i < 3; i++) {
                        temp[i] = arr[idx, i];
                    }
                    // arr[right] = arr[left]
                    for (int i = 0; i < 3; i++) {
                        arr[idx, i] = arr[j, i];
                        //arr.SetValue(arr.GetValue(left, i), right, i);
                    }
                    // arr[left] = temp;
                    for (int i = 0; i < 3; i++) {
                        arr[j, i] = temp[i];
                        //arr.SetValue(temp[i], left, i);
                    }
                }
            }
            // temp = arr[right]
            object[] temp1 = new object[3];
            for (int i = 0; i < 3; i++) {
                temp1[i] = arr[idx + 1, i];
            }
            // arr[right] = arr[left]
            for (int i = 0; i < 3; i++) {
                arr[idx + 1, i] = arr[right, i];
                //arr.SetValue(arr.GetValue(left, i), right, i);
            }
            // arr[left] = temp;
            for (int i = 0; i < 3; i++) {
                arr[right, i] = temp1[i];
                //arr.SetValue(temp[i], left, i);
            }
            return idx + 1;
        }

        public void QSort(object[,] arr, int left, int right, bool lastName) {
            if (left < right) {
                // move element by element moving pivot by recursivly calling QSort
                // the splitInx is the pivot and is swapped with all elements larger 
                // than it moved to the left of the array
                int pivot = this.Partition(arr, left, right);
                // recursivly checks elements to the left
                this.QSort(arr, left, pivot - 1);
                // recursivly take large 'half' and swap elements
                this.QSort(arr, pivot + 1, right);
            }
        }
    }
}
