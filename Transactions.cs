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
            Console.Clear();
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
                Console.Clear();
                Console.WriteLine("\nEnter the customer's name:\n");
                string customerName = Console.ReadLine();

                Console.WriteLine("\nEnter the customer's email:\n");
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

                Console.Clear();
                Console.WriteLine("Enter the ISBN of the book to be rented (-1 to stop):\n ");
                tranISBN = int.Parse(Console.ReadLine());
                }
                //if user tries to rent a book that there are no available copies of, error message is presented
                else
                {
                    Console.WriteLine("\nERROR... There are no available copies of this book\n");
                    GetTransactionData(myBooks);
                }
                Console.WriteLine("Books rented. Press enter to continue");
                Console.ReadLine();

            }
            return myTransactions;

         }

         public static void Edit(Transactions[] myTransactions)
        {
            // edit function
            Console.WriteLine("Enter the rental ID of the transaction you would like to edit: ");
            int tempID = int.Parse(Console.ReadLine());
            //searching ISBN to be able to edit a specific book 
            int indexFound = TranUtility.BinarySearch(myTransactions, tempID);
            Console.Clear();
            Console.WriteLine("Please enter:\n'ID' to edit the Rental ID\n'ISBN' to edit the ISBN\n'Name' to edit the Customer Name\n'Email' to edit the Customer Email\n'Rent Date' to change the Rent Date\n'Return' to change the 'Return Date'");
            string answer = Console.ReadLine().ToLower();
            if(answer == "id")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new ID:");
                int newID = int.Parse(Console.ReadLine());
                //finds the instance of the ID entered and makes an edit to ID at that instance
                myTransactions[indexFound].SetRentID(newID);
            }
            else if(answer == "isbn")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new ISBN:");
                int newISBN = int.Parse(Console.ReadLine());
                //finds the instance of the ID entered and makes an edit to ID at that instance
                myTransactions[indexFound].SetTranISBN(newISBN);
            }
            else if(answer == "name")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new customer name:");
                string newName = Console.ReadLine();
                //finds the instance of the ISBN entered and makes an edit to genre at that instance
                myTransactions[indexFound].SetCustomerName(newName);
            }
            else if(answer == "email")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new customer email:");
                string newCE = Console.ReadLine();
                //finds the instance of the ISBN entered and makes an edit to listening time at that instance
                myTransactions[indexFound].SetCustomerEmail(newCE);
            }
            else if(answer == "rent date")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new rental date (MM/DD/YYYY):");
                string newRD = Console.ReadLine();
                //finds the instance of the ISBN entered and makes an edit to listening time at that instance
                myTransactions[indexFound].SetRentDate(newRD);
            }
            else if(answer == "return")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new return date (MM/DD/YYYY):");
                string newRetD = Console.ReadLine();
                //finds the instance of the ISBN entered and makes an edit to listening time at that instance
                myTransactions[indexFound].SetReturnDate(newRetD);
            }

            // clear and re-send sorted and edited array (update the text file)
                string path2 = "transactions.txt";
                File.WriteAllText(path2, String.Empty);
                TextWriter tW2 = new StreamWriter(path2, true);
                tW2.Close();
                ToFile(myTransactions);

            Console.WriteLine("\nTransaction edited. Press enter to continue.");
            Console.ReadLine();
        }


         public static void BookReturn(Transactions[] myTransactions, Book[] myBooks)
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

                                //take the ISBN of the book returned and search it in the book array, if found, increase the count of copies (since one was returned)
                                int tempISBN = myTransactions[i].GetTranISBN();
                                int indexFound = BookUtility.BinarySearch(myBooks, tempISBN);
                                int copiesAdd = myBooks[indexFound].GetBookCount() + 1;
                                myBooks[indexFound].SetBookCount(copiesAdd);

                                if(myBooks[indexFound].GetBookCount() > 0)
                                {
                                    string status = "Available";
                                    myBooks[indexFound].SetStatus(status);
                                }
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
        public int CompareToID(Transactions count)
        {
            return this.rentID.CompareTo(count.GetRentID());
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