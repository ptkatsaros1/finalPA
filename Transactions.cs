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
        //constructor
        public Transactions(int rentID, int tranISBN, string customerName, string customerEmail, string rentDate, string returnDate)
        {
            this.rentID = rentID;
            this.tranISBN = tranISBN;
            this.customerName = customerName;
            this.customerEmail = customerEmail;
            this.rentDate = rentDate;
            this.returnDate = returnDate;
        }
        //getters and setters for transaction info
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
            //creates transaction array to input books to be marked as rented
            Transactions[] myTransactions = new Transactions[200];
            
            //get ISBN from user to identify boook being rented
            Console.WriteLine("Enter the ISBN of the book to be marked as rented (-1 to stop):\n ");
            int tranISBN = int.Parse(Console.ReadLine());

            while(tranISBN != -1) 
            {
                //search book array for ISBN
                int indexFound = BookUtility.BinarySearch(myBooks, tranISBN);
                //if the number of book copies is greater than 0 (there are still books available under this ISBN)
                if(myBooks[indexFound].GetBookCount() > 0)
                {
                    //subract one from the number of book copies (one has been rented)
                    int copies1 = myBooks[indexFound].GetBookCount() - 1;
                    myBooks[indexFound].SetBookCount(copies1);
                    //if the copy count now equals zero, mark the status under the book file as rented ie. there are none left
                    if(myBooks[indexFound].GetBookCount() == 0)
                    {
                        string status = "Rented";
                        myBooks[indexFound].SetStatus(status);
                    }
                //get rest of renter's info
                Console.WriteLine("Enter the customer's name:\n");
                string customerName = Console.ReadLine();

                Console.WriteLine("Enter the customer's email:\n");
                string customerEmail = Console.ReadLine();

                //rent ID randomized
                Random rnd = new Random();
                int rentID = rnd.Next();

                //rent date set to current date, return date set to N/A (per Dr. Saifee's instruction)
                string rentDate = DateTime.Now.ToString("M/d/yyyy");
                string returnDate = "N/A";  

                Transactions newTransactions = new Transactions(rentID, tranISBN, customerName, customerEmail, rentDate, returnDate);
                myTransactions[Transactions.GetTranCount()] = newTransactions;
                Transactions.IncrTranCount();

                Console.WriteLine("Enter the ISBN of the book to be rented (-1 to stop):\n ");
                tranISBN = int.Parse(Console.ReadLine());
                }
                //if user tries to rent a book that there are no available copies of, error message is presented
                else
                {
                    Console.WriteLine("\nERROR... There are no available copies of this book\n");
                    GetTransactionData(myBooks);
                }

            }
            return myTransactions;

         }

         public static void BookReturn(Transactions[] myTransactions)
         {
             //return book
            Console.WriteLine("\nEnter the ISBN of the book to be returned: ");
            //search user return ISBN in transaction file
            int tempTranISBN = int.Parse(Console.ReadLine());
            Console.WriteLine("\nEnter the customer's email: ");
            string tempEmail = Console.ReadLine();

            //nested for loop to search whole array, checking email with i and ISBN with j
            for(int i = 0; i < Transactions.GetTranCount(); i++)
            {
                for (int j = 0; j < Transactions.GetTranCount(); j++)
                {
                    //check if user input email equals any email on the array
                    if(tempEmail == myTransactions[i].GetCustomerEmail())
                    {
                        //if the email is found, check if the user ISBN equals that instance
                        if(tempTranISBN == myTransactions[j].GetTranISBN())
                        {
                            //if the email found and ISBN found match each other's instances, replace the return date
                            if(i == j)
                            {
                                //find the isbn instance in the transaction file and replace the N/A with the current date (date of return)
                                string newReturnDate = DateTime.Now.ToString("M/d/yyyy");
                                myTransactions[i].SetReturnDate(newReturnDate);
                            }
                        }
                    } 
                }
            }
            //clear text file and resend updated info
            string path5 = "transactions.txt";
            File.WriteAllText(path5, String.Empty);
            TextWriter tW5 = new StreamWriter(path5, true);
            tW5.Close();
            ToFile(myTransactions);
         }

        //ToFile to write transaction array to text file with # delimeter
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

        public int CompareToDate(Transactions count)
        {
            return this.rentDate.CompareTo(count.GetRentDate());
        }

        public int CompareToHist(Transactions count)
        {
            return this.customerName.CompareTo(count.GetCustomerName());
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