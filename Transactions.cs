using System;
using System.IO;
namespace PA5
{
    public class Transactions
    {
        private int rentID;
        private int tranISBN;
        private string customerName;
        private string customerEmail;
        private string rentDate;
        private string returnDate;
        private static int count;

        public Transactions()
        {

        }

        public Transactions(int rentID, int tranISBN, string customerName, string customerEmail, string rentDate, string returnDate)
        {
            this.rentID = rentID;
            this.tranISBN = tranISBN;
            this.customerName = customerName;
            this.customerEmail = customerEmail;
            this.rentDate = rentDate;
            this.returnDate = returnDate;
        }

        public void SetRentID(int rentID)
        {
            this.rentID = rentID;
        }

        public int GetRentID()
        {
            return rentID;
        }

         public void SetTranISBN(int tranISBN)
        {
            this.tranISBN = tranISBN;
        }
        public int GetTranISBN()
        {
            return tranISBN;
        }

        public void SetCustomerName(string customerName)
        {
            this.customerName = customerName;
        }

        public string GetCustomerName()
        {
            return customerName;
        }

        public void SetCustomerEmail(string customerEmail)
        {
            this.customerEmail = customerEmail;
        }

        public string GetCustomerEmail()
        {
            return customerEmail;
        }

        public void SetRentDate(string rentDate)
        {
            this.rentDate = rentDate;
        }

        public string GetRentDate()
        {
            return rentDate;
        }

        public void SetReturnDate(string returnDate)
        {
            this.returnDate = returnDate;
        }

        public string GetReturnDate()
        {
            return returnDate;
        }

         public static int GetTranCount()
        {
            return count;
        }
        
        public static void SetTranCount(int count)
        {
            Transactions.count = count;
        }

        public static void IncrTranCount()
        {
            count++;
        }

        public override string ToString()
        {
            return rentID + " " + tranISBN + " " + customerName + " " + customerEmail + " "  + " " + rentDate + " " + returnDate; 
        }

         public static Transactions[] GetTransactionData(Book[] myBooks)
         {

            Transactions[] myTransactions = new Transactions[200];

            Console.WriteLine("Enter the ISBN of the book to be marked as rented (-1 to stop):\n ");
            int tranISBN = int.Parse(Console.ReadLine());

            while(tranISBN != -1) 
            {

                int indexFound = BookUtility.BinarySearch(myBooks, tranISBN);
                if(myBooks[indexFound].GetBookCount() > 0)
                {
                    int copies1 = myBooks[indexFound].GetBookCount() - 1;
                    myBooks[indexFound].SetBookCount(copies1);
                    if(myBooks[indexFound].GetBookCount() == 0)
                    {
                        string status = "Rented";
                        myBooks[indexFound].SetStatus(status);
                    }
                Console.WriteLine("Enter the customer's name:\n");
                string customerName = Console.ReadLine();

                Console.WriteLine("Enter the customer's email:\n");
                string customerEmail = Console.ReadLine();

                Random rnd = new Random();
                int rentID = rnd.Next();

                string rentDate = DateTime.Now.ToString("M/d/yyyy");
                string returnDate = "N/A";  

                Transactions newTransactions = new Transactions(rentID, tranISBN, customerName, customerEmail, rentDate, returnDate);
                myTransactions[Transactions.GetTranCount()] = newTransactions;
                Transactions.IncrTranCount();

                Console.WriteLine("Enter the ISBN of the book to be rented (-1 to stop):\n ");
                tranISBN = int.Parse(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("ERROR");
                    GetTransactionData(myBooks);
                }

            }
            return myTransactions;

         }

         public static void ToFile(Transactions[] myTransactions)
        {
            myTransactions.ToString();
            using(StreamWriter sW = File.AppendText("transactions.txt"))
            for (int i = 0; i < Transactions.GetTranCount(); i++)
                {
                    sW.WriteLine(myTransactions[i].GetRentID() + "#" + myTransactions[i].GetTranISBN() + "#" + myTransactions[i].GetCustomerName() + "#" + myTransactions[i].GetCustomerEmail() + "#" + myTransactions[i].GetRentDate() + "#" + myTransactions[i].GetReturnDate());
                }
        }

         public int CompareTo(Transactions count)
        {
            return this.tranISBN.CompareTo(count.GetTranISBN());
        }

          public static void PrintRent(Transactions[] myTransactions)
        {
            for (int i = 0; i < Transactions.GetTranCount(); i++)
            {
                Console.WriteLine(myTransactions[i].ToString());
            }
        }
    }
}