using System;
using System.IO;

namespace PA5
{
    internal class NewBaseType
    {
        static void Main(string[] args)
        {

            string selection = "";

            while (selection != "exit")
            {
                selection = GetSelection();
                BookFile data = new BookFile("books.txt");
                Book[] myBooks = data.ReadBookData();
                BookUtility bookUtility = new BookUtility(myBooks);
                BookFile bookFile = new BookFile("books.txt");
                TranFile tranFile = new TranFile("transactions.txt");
                Transactions[] myTransactions = new Transactions[200];
                TranUtility tranUtility = new TranUtility(myTransactions);
                BookReport bookReport = new BookReport(myTransactions);

                //check the user selection and take them to the appropriate methods
                if(selection == "add")
                {
                    myBooks = Book.GetBookData();
                    //everytime a book is added, the array gets sorted (to prepare for any binary searches), and then the book text file is cleared and the sorted info is inputed
                    Book.SortAndSend(myBooks);
                }
                if(selection == "edit")
                {
                    Book.Edit(myBooks);
                }
                if(selection == "view")
                {
                    Console.WriteLine("\nBooks available for rent:\n\n");
                    for(int i = 0; i < Book.GetCount(); i++)
                    {
                        if(myBooks[i].GetStatus() == "Available")
                        {
                            Console.WriteLine(myBooks[i].GetTitle());
                        }
                    }
                    Console.WriteLine("\nPress enter to continue: ");
                    Console.ReadLine();
                }
                if(selection == "rent")
                {
                    //Lets the user input which book ISBN is to be rented
                    myTransactions = Transactions.GetTransactionData(myBooks);
                    Transactions.PrintRent(myTransactions);
                    //Marks the book as rented in the book text file
                    string rentPath = "books.txt";
                    File.WriteAllText(rentPath, String.Empty);
                    TextWriter twP = new StreamWriter(rentPath, true);
                    twP.Close();
                    Book.ToFile(myBooks);

                    //write what is in the transactions array to the text file, read the text file and send back to array, sort the array
                    Transactions.ToFile(myTransactions);
                    myTransactions = tranFile.ReadTranData();
                    tranUtility.SelectionSort(myTransactions);
                    Transactions.PrintRent(myTransactions);
                    //clear and re-send sorted and edited array (update the text file)
                    string tranPath = "transactions.txt";
                    File.WriteAllText(tranPath, String.Empty);
                    TextWriter tWTran = new StreamWriter(tranPath, true);
                    tWTran.Close();
                    Transactions.ToFile(myTransactions);
                }
                if(selection == "return")
                {
                    Console.Clear();
                    //reading transaction file data into array for use
                    myTransactions = tranFile.ReadTranData();
                    //show user books that can be returned
                    Transactions.PrintRent(myTransactions);
                   
                    Transactions.BookReturn(myTransactions);

                    Console.WriteLine("\nBook returned.");
                    Console.WriteLine("\nPress enter to continue: ");
                    Console.ReadLine();
                }
                if(selection == "total")
                {
                    //reading transaction file data into array for use
                   myTransactions = tranFile.ReadTranData();

                   bookReport.TotalReport(myTransactions);

                }
                if(selection == "individual")
                {
                    //reading transaction file data into array for use
                    myTransactions = tranFile.ReadTranData();
                    //sort by name before calling IndividualReport method
                    tranUtility.SelectionSort(myTransactions);
                    //calls method report
                    BookReport.IndividualReport(myTransactions);

                    Console.WriteLine("\nPress enter to continue: ");
                    Console.ReadLine();
                }
                if(selection == "historical")
                {
                    myTransactions = tranFile.ReadTranData();
                    TranUtility.SelectionSortHist(myTransactions);
                    Transactions.PrintRent(myTransactions);
                    Console.WriteLine("");

                    bookReport.CustDateSort(myTransactions);
                    Transactions.PrintRent(myTransactions);
                    Console.ReadLine();
                }
            }

        }

            //display menu options for user
            static void DisplayMenu()
            {
                Console.WriteLine("\n\nPlease type:\n'Add' to add books\n'Edit' to edit a book\n'View' to view books available for rent\n'Rent' to mark a book as rented\n'Return' to mark a book as returned\n'Total' to see total rentals by month and year\n'Individual' to see individual customer rentals\n'Historical' to see all rentals to date");
            }

            //method gets option from user, if option is not valid then it will display an error message and reprompt the user
            static string GetSelection()
            {
                Console.Clear();
                DisplayMenu();
                string selection = Console.ReadLine().ToLower();

                while (!ErrorMessage(selection))
                {
                    Console.Clear();
                    Console.WriteLine("ERROR: Invalid selection. Press enter to try again. ");
                    Console.ReadKey();

                    DisplayMenu();
                    selection = Console.ReadLine();
                }
                return selection;
            }

            //establish the valid choices the user can make using bool
            static bool ErrorMessage(string selection)
            {
                bool isNotValid = false;
                if (selection == "add" || selection == "edit" || selection == "view" || selection == "rent" || selection == "return" || selection == "total" || selection == "individual" || selection == "historical")
                {
                    return true;
                }
                else
                {
                    return isNotValid;
                }
            }
    }

}
