using System;
using System.IO;
using System.Linq;
namespace PA5
{
    public class BookReport
    {
        private Transactions[] myTransactions;

        public BookReport(Transactions[] myTransactions)
        {
            this.myTransactions = myTransactions;
        }

        //sorts transactions array by date
        public static void SortDate(Transactions[] myTransactions)
        {
            for (int i = 0; i < Transactions.GetTranCount() - 1; i++)
            {
                int minIndex = i;

                for(int j = i + 1; j < Transactions.GetTranCount(); j++)
                {
                    DateTime dateObjMin = DateTime.Parse(myTransactions[minIndex].GetRentDate());
                    DateTime dateObjJ = DateTime.Parse(myTransactions[j].GetRentDate());
                    
                    if(DateTime.Compare(dateObjMin, dateObjJ) == 1)
                    {
                        minIndex = j;
                    }
                }

                if(minIndex != i)
                {
                    Swap(myTransactions, minIndex, i);
                }
            }
        }

        public static void Swap(Transactions[] myTransactions, int x, int y )
        {
            Transactions temp = myTransactions[x];
            myTransactions[x] = myTransactions[y];
            myTransactions[y] = temp;
        }

        public void TotalReport(Transactions[] myTransactions)
        {

            BookReport.SortDate(myTransactions);
            Console.WriteLine("\nTotal rentals by month and year: \n");
            Transactions.PrintRent(myTransactions);
            Console.WriteLine("");
            Console.WriteLine("");

            int monthCount = 1;
            int yearCount = 0; 
            string str = myTransactions[0].GetRentDate().Substring(0,2);

            for (int i = 1; i < Transactions.GetTranCount(); i++)
            {
                if(str == myTransactions[i].GetRentDate().Substring(0,2))
                {
                    monthCount++;
                }
                else
                {
                    ProcessBreak(myTransactions, ref str, ref monthCount, ref yearCount, i);
                }
            }
            ProcessBreak(myTransactions, ref str, ref monthCount, ref yearCount, 0);
        }

        public void ProcessBreak(Transactions[] myTransactions, ref string str, ref int monthCount, ref int yearCount, int i)
        {
            yearCount += monthCount;

            for (int j = (yearCount - monthCount); j < yearCount; j++)
            {
                Console.WriteLine(myTransactions[j]);
            }
            Console.WriteLine("");
            Console.WriteLine("Rentals this month: " + monthCount);
            Console.WriteLine("Rentals this year: " + yearCount);
            Console.ReadLine();

            str = myTransactions[i].GetRentDate().Substring(0,2);
            monthCount = 1;
        }

        //individual report (gives all rentals by that customer)
         public static void IndividualReport(Transactions[] myTransactions)
         {
            //clear and re-send sorted and edited array (update the text file)
            string returnPath = "transactions.txt";
            File.WriteAllText(returnPath, String.Empty);
            TextWriter tWReturn = new StreamWriter(returnPath, true);
            tWReturn.Close();
            Transactions.ToFile(myTransactions);

            Console.WriteLine("\nEnter the customer's email: ");
            string tempIndvEmail = Console.ReadLine();
            Console.WriteLine("\nCustomer rental history: \n");

            //simple for loop
            for(int i = 1; i < Transactions.GetTranCount(); i++)
            {
                //checks if user inputed email is equal to any email in the text file, if yes, display
                if(tempIndvEmail == myTransactions[i].GetCustomerEmail())
                {
                    Console.WriteLine(myTransactions[i]);
                }
            }

            Console.WriteLine("\nWould you like to enter this into a text file? (yes or no)");
            string choice = Console.ReadLine();
            if(choice.ToLower() == "yes")
            {
                Console.WriteLine("\nEnter the name of the text file: ");
                string tempTxt = Console.ReadLine();
                StreamWriter outFile = new StreamWriter(tempTxt);
                //Streamwrite the info written out above into a text file of the user's choice
                for(int i = 1; i < Transactions.GetTranCount(); i++)
                {
                    if(tempIndvEmail == myTransactions[i].GetCustomerEmail())
                    {
                        outFile.WriteLine(myTransactions[i].GetRentID() + "#" + myTransactions[i].GetTranISBN() + "#" + myTransactions[i].GetCustomerName() + "#" + myTransactions[i].GetCustomerEmail() + "#" + myTransactions[i].GetRentDate() + "#" + myTransactions[i].GetReturnDate());
                    }
                }
                outFile.Close();
            }
         }

        public void CustDateSort(Transactions[] myTransactions)
        {
            for (int i = 0; i < Transactions.GetTranCount() - 1; i++)
            {
                int minIndex = i;
                for(int j = i + 1; j < Transactions.GetTranCount(); j++)
                {
                    if(myTransactions[i].GetCustomerName() == myTransactions[j].GetCustomerName())
                    {
                        DateTime dateObjMin = DateTime.Parse(myTransactions[minIndex].GetRentDate());
                        DateTime dateObjJ = DateTime.Parse(myTransactions[j].GetRentDate());
                        
                        if(DateTime.Compare(dateObjMin, dateObjJ) == 1)
                        {
                            minIndex = j;
                        }
                        if(minIndex != i)
                        {
                            Swap(i, minIndex);
                        }
                    }
                }
                if(minIndex != i)
                {
                    Swap(myTransactions, minIndex, i);
                }
            }
        }

        public void Swap(int x, int y)
        {
            Transactions temp = myTransactions[x];
            myTransactions[x] = myTransactions[y];
            myTransactions[y] = temp;
        }




        // Console.ReadLine();
            // Console.WriteLine("Would you like to save these to a text file? (yes or no) ");
            // string answer = Console.ReadLine();

            // if(answer.ToLower() == "yes")
            // {
            //     Console.WriteLine("\nEnter the name of the text file: ");
            //     string tempTxt = Console.ReadLine();
            //     StreamWriter outFile = new StreamWriter(tempTxt);

            //     for(int i = 0; i < Transactions.GetTranCount(); i++)
            //     {
            //         outFile.WriteLine(myTransactions[i].GetRentID() + "#" + myTransactions[i].GetTranISBN() + "#" + myTransactions[i].GetCustomerName() + "#" + myTransactions[i].GetCustomerEmail() + "#" + myTransactions[i].GetRentDate() + "#" + myTransactions[i].GetReturnDate());
            //     }

            //  outFile.Close();
            // }
        
    } 
}