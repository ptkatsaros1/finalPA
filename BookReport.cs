using System;
using System.IO;
using System.Linq;
namespace PA5
{
    public class BookReport
    {
        private Transactions[] myTransactions;
        private Book[] myBooks;

        public BookReport(Transactions[] myTransactions)
        {
            this.myTransactions = myTransactions;
        }
        public BookReport(Book[] myBooks)
        {
            this.myBooks = myBooks;
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
            Console.Clear();
            BookReport.SortDate(myTransactions);
            Console.WriteLine("\nTotal rentals by month and year: \n");
            Transactions.PrintRent(myTransactions);
            Console.WriteLine("");
            Console.WriteLine("");

            string yearStr = myTransactions[0].GetRentDate().Substring(6,4);
            int monthCount = 0;
            string monthStr = myTransactions[0].GetRentDate().Substring(0,2);

            Console.WriteLine("\nWould you like to see a report for a specific month? ('yes' or 'no')");
            string yes = Console.ReadLine();
            if(yes == "yes")
            {
                Console.Clear();
                Console.WriteLine("Enter a month and year to see the report from that month:\n");
                Console.WriteLine("ATTENTION: enter month as 'MM' and year as 'YYYY'");
                Console.WriteLine("\nEnter a month:\n");
                int monthIntTemp = int.Parse(Console.ReadLine());
                string monthTemp = monthIntTemp.ToString();
                Console.WriteLine("\nEnter a year:\n");
                string yearTemp = Console.ReadLine();

                for (int i = 0; i < Transactions.GetTranCount(); i++)
                {
                    if(yearTemp ==  myTransactions[i].GetRentDate().Substring(6,4) && monthTemp == myTransactions[i].GetRentDate().Substring(0,2))
                    {
                        monthCount++;
                    }
                }

                DateTime date = new DateTime(2020, monthIntTemp, 1);
                string wordMonth = date.ToString("MMMM");

                Console.Clear();
                Console.WriteLine("\nNumber of rentals in " + wordMonth + ": " + monthCount);
                Console.WriteLine("");
                for (int i = 0; i < Transactions.GetTranCount(); i++)
                {
                    if(yearTemp ==  myTransactions[i].GetRentDate().Substring(6,4) && monthTemp == myTransactions[i].GetRentDate().Substring(0,2))
                    {
                        Console.WriteLine(myTransactions[i]);
                    }
                }
                Console.WriteLine("\nPress enter to continue...");
                Console.ReadLine();

                Console.WriteLine("\nWould you like to write this to a text file? ('yes' or 'no')");
                string choice = Console.ReadLine();
                if(choice.ToLower() == "yes")
                {
                    Console.WriteLine("\nEnter the name of the text file: ");
                    string tempTxt = Console.ReadLine();
                    StreamWriter outFile = new StreamWriter(tempTxt);
                    //Streamwrite the info written out above into a text file of the user's choice
                    for (int i = 0; i < Transactions.GetTranCount(); i++)
                    {
                        if(yearTemp ==  myTransactions[i].GetRentDate().Substring(6,4) && monthTemp == myTransactions[i].GetRentDate().Substring(0,2))
                        {
                            outFile.WriteLine(myTransactions[i]);
                        }
                    }
                    outFile.Close();
                    Console.WriteLine("\nSuccess! Text entered. (Press enter to continue)");
                    Console.ReadLine();
                }
            }

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

            Console.WriteLine("\nWould you like to enter this into a text file? ('yes' or 'no')");
            string choice = Console.ReadLine();
            if(choice.ToLower() == "yes")
            {
                Console.WriteLine("\nEnter the name of the text file: ");
                string tempTxt = Console.ReadLine();
                StreamWriter outFile = new StreamWriter(tempTxt);
                //Streamwrite the info written out above into a text file of the user's choice
                for(int i = 0; i < Transactions.GetTranCount(); i++)
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
            //nested for loop
            for (int i = 0; i < Transactions.GetTranCount() - 1; i++)
            {
                //set min equal to i (first value we will see)
                int minIndex = i;
                for(int j = i + 1; j < Transactions.GetTranCount(); j++)
                {
                    //nested for loop compares each index to the one next to it
                    if(myTransactions[i].GetCustomerName() == myTransactions[j].GetCustomerName())
                    {
                        DateTime dateObjMin = DateTime.Parse(myTransactions[minIndex].GetRentDate());
                        DateTime dateObjJ = DateTime.Parse(myTransactions[j].GetRentDate());
                        
                        //if any value J we see is smaller than min, swap the two
                        if(DateTime.Compare(dateObjMin, dateObjJ) == 1)
                        {
                            minIndex = j;
                        }
                        //swap function changes order of dates for each customer
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

        //simple write method to prompt a text file, and write text into it
        public void Write(Transactions[] myTransactions)
        {
            Console.WriteLine("\nWould you like to enter this into a text file? ('yes' or 'no')");
            string choice = Console.ReadLine();
            if(choice.ToLower() == "yes")
            {
                Console.WriteLine("\nEnter the name of the text file: ");
                string tempTxt = Console.ReadLine();
                StreamWriter outFile = new StreamWriter(tempTxt);
                //Streamwrite the info written out above into a text file of the user's choice
                for(int i = 0; i < Transactions.GetTranCount(); i++)
                {
                    outFile.WriteLine(myTransactions[i].GetRentID() + "#" + myTransactions[i].GetTranISBN() + "#" + myTransactions[i].GetCustomerName() + "#" + myTransactions[i].GetCustomerEmail() + "#" + myTransactions[i].GetRentDate() + "#" + myTransactions[i].GetReturnDate());
                }
                outFile.Close();
                Console.WriteLine("\nSuccess! Text entered. (Press enter to continue)");
                Console.ReadLine();
            }
        }

        public void GenreReport(Book[] myBooks)
        {
            int count = 1;
            int max = 1;
            string maxGenre = myBooks[0].GetGenre();
            string tempGenre = myBooks[0].GetGenre();
            for (int i = 0; i < Book.GetCount(); i++)
            {
                if(tempGenre == myBooks[i].GetGenre())
                {
                    //increment count if the beginning genre equals any of the next
                    count++;
                    if(count > max)
                    {
                        //update the max variable if any count for a genre is greater than the max
                        max = count;
                        //update the genre to be the max of whichever has the max count
                        maxGenre = myBooks[i].GetGenre();
                    }
                }
                else
                {
                    GenreBreak(myBooks, ref tempGenre, ref count, ref maxGenre, i);
                }
            }
            GenreBreak(myBooks, ref tempGenre, ref count, ref maxGenre, 0);
            //once for loop is finished counting, display the one with the max copies
            Console.WriteLine("\nThe most popular genre is: " + maxGenre);
                    
        }

        public void GenreBreak(Book[] myBooks, ref string tempGenre, ref int count, ref string maxGenre, int i)
        {
            //print the number of copies for each genre
            Console.WriteLine("The number of " + tempGenre + " books is " + count);

            tempGenre = myBooks[i].GetGenre();
            count = 1;
        }
    } 
}