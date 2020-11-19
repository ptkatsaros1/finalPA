using System;
using System.IO;

namespace PA5
{
    class Program
    {
        static void Main(string[] args)
        {

            Book[] myBooks = Book.GetBookData();
            BookUtility bookUtility = new BookUtility(myBooks);
            BookFile bookFile = new BookFile("books.txt");
            TranFile tranFile = new TranFile("transactions.txt");
            Transactions[] myTransactions = new Transactions[200];
            TranUtility tranUtility = new TranUtility(myTransactions);
            BookReport bookReport = new BookReport(myTransactions);
    
            
            // sort the array
            bookUtility.SelectionSort(myBooks);
            Book.PrintBooks(myBooks);
            // clear and re-send sorted and edited array (update the text file)
            string path = "books.txt";
            File.WriteAllText(path, String.Empty);
            TextWriter tW = new StreamWriter(path, true);
            tW.Close();
            Book.ToFile(myBooks);

            // // edit function
            // Console.WriteLine("Enter the ISBN of the book you would like to edit: ");
            // int tempISBN = int.Parse(Console.ReadLine());
            // //searching ISBN 
            // int indexFound = BookUtility.BinarySearch(myBooks, tempISBN);
            // Console.WriteLine("Please enter:\n'Title' to edit the title\n'Author' to edit the author\n'Genre' to edit the genre\n'Listening Time' to edit the listening time\n'Copy Count' to chnage the number of available copies");
            // string answer = Console.ReadLine().ToLower();
            // if(answer == "title")
            // {
            //     Console.WriteLine("\nEnter a new title:");
            //     string newTitle = Console.ReadLine();
            //     myBooks[indexFound].SetTitle(newTitle);
            // }
            // else if(answer == "author")
            // {
            //     Console.WriteLine("\nEnter a new author:");
            //     string newAuthor = Console.ReadLine();
            //     myBooks[indexFound].SetAuthor(newAuthor);
            // }
            // else if(answer == "genre")
            // {
            //     Console.WriteLine("\nEnter a new genre:");
            //     string newGenre = Console.ReadLine();
            //     myBooks[indexFound].SetGenre(newGenre);
            // }
            // else if(answer == "listening time")
            // {
            //     Console.WriteLine("\nEnter a new listening time:");
            //     double newLT = double.Parse(Console.ReadLine());
            //     myBooks[indexFound].SetListeningTime(newLT);
            // }
            // else if(answer == "copy count")
            // {
            //     Console.WriteLine("Enter 'add' to add a copy of the book or 'subtract' to subtract a copy");
            //     string copy = Console.ReadLine().ToLower();
            //     if(copy == "add")
            //     {
            //         int copiesAdd = myBooks[indexFound].GetBookCount() + 1;
            //         myBooks[indexFound].SetBookCount(copiesAdd);
            //         Console.WriteLine("\nBook count is currently: " + myBooks[indexFound].GetBookCount());
            //     }
            //     else if(copy == "subtract")
            //     {
            //         int copies = myBooks[indexFound].GetBookCount() - 1;
            //         myBooks[indexFound].SetBookCount(copies);
            //         Console.WriteLine("\nBook count is currently: " + myBooks[indexFound].GetBookCount());
            //     }
            // }

        
            // // clear and re-send sorted and edited array (update the text file)
            // string path2 = "books.txt";
            // File.WriteAllText(path, String.Empty);
            // TextWriter tW2 = new StreamWriter(path2, true);
            // tW2.Close();
            // Book.ToFile(myBooks);


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

            //Display books available for rent
            // Console.Clear();
            Console.WriteLine("\nBooks available for rent:\n\n");
            for(int i = 0; i < Book.GetCount(); i++)
            {
                if(myBooks[i].GetStatus() == "Available")
                {
                    Console.WriteLine(myBooks[i].GetTitle());
                }
            }


            // //return book
            // Console.WriteLine("\nEnter the ISBN of the book to be returned: ");
            // //search user return ISBN in transaction file
            // int tempTranISBN = int.Parse(Console.ReadLine());
            // Console.WriteLine("\nEnter the customer's email: ");
            // string tempEmail = Console.ReadLine();

            // for(int i = 1; i < Transactions.GetTranCount(); i++)
            // {
            //     int tempFound = tranUtility.BinarySearch(myTransactions, tempTranISBN);
            //     if(tempEmail == myTransactions[i].GetCustomerEmail())
            //     {
            //         if(i == tempFound)
            //         {
            //             //find the isbn instance in the transaction file and replace the N/A with the current date (date of return)
            //             string newReturnDate = DateTime.Now.ToString("M/d/yyyy");
            //             myTransactions[tempFound].SetReturnDate(newReturnDate);
            //         }
            //     }
            // }

            //clear and re-send sorted and edited array (update the text file)
            string returnPath = "transactions.txt";
            File.WriteAllText(returnPath, String.Empty);
            TextWriter tWReturn = new StreamWriter(returnPath, true);
            tWReturn.Close();
            Transactions.ToFile(myTransactions);

            //sort by date
            BookReport.SortDate(myTransactions);
            Console.WriteLine("\nTotal rentals by month and year: \n");
            Transactions.PrintRent(myTransactions);
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

            //Individual customer rentals
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

            for (int i = 0; i < Transactions.GetTranCount(); i++)
            {
                string[] names = myTransactions[i].GetCustomerEmail();
            }
           
            var sortedNames = names.OrderBy(n => n);
            foreach (var item in sortedNames)
            {
            Console.WriteLine(item);
            }
            

            //Historical customer rentals
            // string cust = myTransactions[0].GetCustomerEmail();

            // for(int i = 1; i < Transactions.GetTranCount(); i++)
            // {
            //     if(cust == myTransactions[i].GetCustomerEmail())
            //     {
            //         Console.WriteLine
            //     }
            //     else
            //     {
            //         averageT = sum / count;
            //         Console.WriteLine(team + "\t\t" + averageT);

            //         team = players[i].GetTeam();
            //         count = 1;
            //         sum = players[i].GetAverage();
            //     }
            // }
            // Console.WriteLine(team + "\t\t" + averageT);

        }

    }
}
