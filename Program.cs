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
                // Book[] myBooks = Book.GetBookData();
                // Book.SortAndSend(myBooks);
                BookFile data = new BookFile("books.txt");
                Book[] myBooks = data.ReadBookData();
                BookUtility bookUtility = new BookUtility(myBooks);
                BookFile bookFile = new BookFile("books.txt");
                TranFile tranFile = new TranFile("transactions.txt");
                Transactions[] myTransactions = new Transactions[200];
                TranUtility tranUtility = new TranUtility(myTransactions);
                BookReport bookReport = new BookReport(myTransactions);

                if(selection == "add")
                {
                    myBooks = Book.GetBookData();
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
                    myTransactions = tranFile.ReadTranData();
                    Transactions.PrintRent(myTransactions);
                    //return book
                    Console.WriteLine("\nEnter the ISBN of the book to be returned: ");
                    //search user return ISBN in transaction file
                    int tempTranISBN = int.Parse(Console.ReadLine());
                    Console.WriteLine("\nEnter the customer's email: ");
                    string tempEmail = Console.ReadLine();

                    for(int i = 1; i < Transactions.GetTranCount(); i++)
                    {
                        int tempFound = TranUtility.BinarySearch(myTransactions, tempTranISBN);
                        if(tempEmail == myTransactions[i].GetCustomerEmail())
                        {
                            if(i == tempFound)
                            {
                                //find the isbn instance in the transaction file and replace the N/A with the current date (date of return)
                                string newReturnDate = DateTime.Now.ToString("M/d/yyyy");
                                myTransactions[tempFound].SetReturnDate(newReturnDate);
                            }
                        }
             
                    }
                    string tranPath = "transactions.txt";
                    File.WriteAllText(tranPath, String.Empty);
                    TextWriter tWTran = new StreamWriter(tranPath, true);
                    tWTran.Close();
                    Transactions.ToFile(myTransactions);

                    Console.WriteLine("\nBook returned.");
                    Console.WriteLine("\nPress enter to continue: ");
                    Console.ReadLine();
                }
                if(selection == "total")
                {
                    myTransactions = tranFile.ReadTranData();
                    tranUtility.SelectionSort(myTransactions);
                    //clear and re-send sorted and edited array (update the text file)
                    string returnPath = "transactions.txt";
                    File.WriteAllText(returnPath, String.Empty);
                    TextWriter tWReturn = new StreamWriter(returnPath, true);
                    tWReturn.Close();
                    Transactions.ToFile(myTransactions);

                    BookReport.SortDate(myTransactions);
                    Console.WriteLine("\nTotal rentals by month and year: \n");
                    Transactions.PrintRent(myTransactions);
                    Console.ReadLine();
                    Console.WriteLine("Would you like to save these to a text file? (yes or no) ");
                    string answer = Console.ReadLine();

                    if(answer.ToLower() == "yes")
                    {
                        Console.WriteLine("\nEnter the name of the text file: ");
                        string tempTxt = Console.ReadLine();
                        StreamWriter outFile = new StreamWriter(tempTxt);

                        for(int i = 0; i < Transactions.GetTranCount(); i++)
                        {
                            outFile.WriteLine(myTransactions[i].GetRentID() + "#" + myTransactions[i].GetTranISBN() + "#" + myTransactions[i].GetCustomerName() + "#" + myTransactions[i].GetCustomerEmail() + "#" + myTransactions[i].GetRentDate() + "#" + myTransactions[i].GetReturnDate());
                        }

                        outFile.Close();
                    }
                }
                if(selection == "individual")
                {
                    myTransactions = tranFile.ReadTranData();
                    tranUtility.SelectionSort(myTransactions);
                    //clear and re-send sorted and edited array (update the text file)
                    string returnPath = "transactions.txt";
                    File.WriteAllText(returnPath, String.Empty);
                    TextWriter tWReturn = new StreamWriter(returnPath, true);
                    tWReturn.Close();
                    Transactions.ToFile(myTransactions);
                
                    Console.WriteLine("\nEnter the customer's email: ");
                    string tempIndvEmail = Console.ReadLine();
                    Console.WriteLine("\nCustomer rental history: \n");

                    for(int i = 1; i < Transactions.GetTranCount(); i++)
                    {
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
                        for(int i = 1; i < Transactions.GetTranCount(); i++)
                        {

                            if(tempIndvEmail == myTransactions[i].GetCustomerEmail())
                            {
                                outFile.WriteLine(myTransactions[i].GetRentID() + "#" + myTransactions[i].GetTranISBN() + "#" + myTransactions[i].GetCustomerName() + "#" + myTransactions[i].GetCustomerEmail() + "#" + myTransactions[i].GetRentDate() + "#" + myTransactions[i].GetReturnDate());
                            }
                        }
                        outFile.Close();
                    }

                    Console.WriteLine("\nPress enter to continue: ");
                    Console.ReadLine();
                }
                if(selection == "historical")
                {
                    myTransactions = tranFile.ReadTranData();
                    BookReport.CustDateSort(myTransactions);
                }
            }

        }

            static void DisplayMenu()
            {
                Console.WriteLine("\n\nPlease type:\n'Add' to add books\n'Edit' to edit a book\n'View' to view books available for rent\n'Rent' to mark a book as rented\n'Return' to mark a book as returned\n'Total' to see total rentals by month and year\n'Individual' to see individual customer rentals\n'Historical' to see all rentals to date");
            }

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
