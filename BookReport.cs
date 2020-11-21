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


        public static void CustDateSort(Transactions[] myTransactions)
        {   
            TranUtility.SelectionSortHist(myTransactions);

            string tempName = myTransactions[0].GetCustomerName();
            // string tempDate = myTransactions[0].GetRentDate();
            int count = 1;

            for (int i = 0; i < Transactions.GetTranCount(); i++)
            {
                if(tempName == myTransactions[i].GetCustomerName())
                {
                    Console.WriteLine(myTransactions[i]);
                }
                else
                {
                    ProcessBreak(myTransactions, ref tempName, ref count, i);
                }

             }
             ProcessBreak(myTransactions, ref tempName, ref count, 0);
        }

        public static void ProcessBreak(Transactions[] myTransactions, ref string tempName, ref int count, int i)
        {
            SortDate(myTransactions);
            Transactions.PrintRent(myTransactions);
            Console.ReadLine();

            tempName = myTransactions[i].GetCustomerName();
            count = 1;
        }

    } 
}