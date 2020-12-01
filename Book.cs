using System;
using System.IO;
namespace PA5
{
    public class Book
    {
        private int ISBN;
        private string title;
        private string author;
        private string genre;
        private double listeningTime;
        private string status;
        private static int count;
        private int bookCount;


        public Book()
        {

        }
        //Book constructor
        public Book(int ISBN, string title, string author, string genre, double listeningTime, string status, int bookCount)
        {
            this.ISBN = ISBN; 
            this.title = title;
            this.author = author;
            this.genre = genre;
            this.listeningTime = listeningTime;
            this.status = status;
            this.bookCount = bookCount;
        }

        //getters and setters
        public void SetStatus(string status)
        {
            this.status = status;
        }
        public string GetStatus()
        {
            return status;
        }
        public void SetISBN(int ISBN)
        {
            this.ISBN = ISBN;
        }

        public int GetISBN()
        {
            return ISBN;
        }

        public void SetTitle(string title)
        {
            this.title = title;
        }

        public string GetTitle()
        {
            return title;
        }

        public void SetAuthor(string author)
        {
            this.author = author;
        }

        public string GetAuthor()
        {
            return author;
        }

        public void SetGenre(string genre)
        {
            this.genre = genre;
        }

        public string GetGenre()
        {
            return genre;
        }

        public void SetListeningTime(double listeningTime)
        {
            this.listeningTime = listeningTime;
        }

        public double GetListeningTime()
        {
            return listeningTime;
        }

         public static int GetCount()
        {
            return count;
        }
        
        public static void SetCount(int count)
        {
            Book.count = count;
        }

        public static void IncrCount()
        {
            count++;
        }

        public int GetBookCount()
        {
            return bookCount;
        }
        public void SetBookCount(int bookCount)
        {
           this.bookCount = bookCount;
        }

        public override string ToString()
        {
            return ISBN + " " + title + " " + author + " " + genre + " " + listeningTime; 
        }

        //Method gets books from user to be added to book file
        public static Book[] GetBookData()
        {
            //read existing data from book file into array
            BookFile data = new BookFile("books.txt");
            Book[] myBooks = data.ReadBookData();
            
            Console.WriteLine("Enter book ISBN (-1 to stop): \n");
            int ISBN = int.Parse(Console.ReadLine());
            
            while(ISBN != -1)
            {   
                //binary search checks to see if the book already exists, if not, it continues to get user info on the book
                int indexFound = BookUtility.BinarySearch(myBooks, ISBN);
                if(indexFound == -1)
                {
                    int bookCount = 1;

                    Console.Write("Enter the title: \n");
                    string title = Console.ReadLine();

                    Console.Write("Enter the author: \n");
                    string author = Console.ReadLine();

                    Console.Write("Enter the genre: \n");
                    string genre = Console.ReadLine();

                    Console.Write("Enter the listening time: \n");
                    double listeningTime = double.Parse(Console.ReadLine());

                    string status = "Available";

                    Book newBook = new Book(ISBN, title, author, genre, listeningTime, status, bookCount);
                    myBooks[GetCount()] = newBook;
                    IncrCount();

                    Console.Write("Enter book ISBN (- 1 to stop): \n");
                    ISBN = int.Parse(Console.ReadLine());
                }
                //if the book ISBN already exists, program will increase the number of copies by one
                else
                {
                    int copies = myBooks[indexFound].GetBookCount() + 1;
                    myBooks[indexFound].SetBookCount(copies);
                    //exits the GetBookData method
                    ISBN = -1;
                }
                Console.WriteLine("\nBook entered (press enter to continue)");
                Console.ReadLine();
            }


            return myBooks;
        }

        public static void Edit(Book[] myBooks)
        {
            // edit function
            Console.WriteLine("Enter the ISBN of the book you would like to edit: ");
            int tempISBN = int.Parse(Console.ReadLine());
            //searching ISBN to be able to edit a specific book 
            int indexFound = BookUtility.BinarySearch(myBooks, tempISBN);
            Console.Clear();
            Console.WriteLine("Please enter:\n'Title' to edit the title\n'Author' to edit the author\n'Genre' to edit the genre\n'Listening Time' to edit the listening time\n'Copy Count' to change the number of available copies");
            string answer = Console.ReadLine().ToLower();
            if(answer == "title")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new title:");
                string newTitle = Console.ReadLine();
                //finds the instance of the ISBN entered and makes an edit to title at that instance
                myBooks[indexFound].SetTitle(newTitle);
            }
            else if(answer == "author")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new author:");
                string newAuthor = Console.ReadLine();
                //finds the instance of the ISBN entered and makes an edit to author at that instance
                myBooks[indexFound].SetAuthor(newAuthor);
            }
            else if(answer == "genre")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new genre:");
                string newGenre = Console.ReadLine();
                //finds the instance of the ISBN entered and makes an edit to genre at that instance
                myBooks[indexFound].SetGenre(newGenre);
            }
            else if(answer == "listening time")
            {
                Console.Clear();
                Console.WriteLine("\nEnter a new listening time:");
                double newLT = double.Parse(Console.ReadLine());
                //finds the instance of the ISBN entered and makes an edit to listening time at that instance
                myBooks[indexFound].SetListeningTime(newLT);
            }
            else if(answer == "copy count")
            {
                Console.Clear();
                Console.WriteLine("Enter 'add' to add a copy of the book or 'subtract' to subtract a copy");
                string copy = Console.ReadLine().ToLower();
                //if the user selects add, the program increaes the copy count by one at the binary search index foud, and subracts if subtract is selected
                if(copy == "add")
                {
                    Console.Clear();
                    int copiesAdd = myBooks[indexFound].GetBookCount() + 1;
                    myBooks[indexFound].SetBookCount(copiesAdd);
                    Console.WriteLine("\nBook count is currently: " + myBooks[indexFound].GetBookCount());
                }
                else if(copy == "subtract")
                {
                    Console.Clear();
                    int copies = myBooks[indexFound].GetBookCount() - 1;
                    myBooks[indexFound].SetBookCount(copies);
                    Console.WriteLine("\nBook count is currently: " + myBooks[indexFound].GetBookCount());
                }
            }

            // clear and re-send sorted and edited array (update the text file)
                string path2 = "books.txt";
                File.WriteAllText(path2, String.Empty);
                TextWriter tW2 = new StreamWriter(path2, true);
                tW2.Close();
                ToFile(myBooks);

            Console.WriteLine("\nBook edited. Press enter to continue.");
            Console.ReadLine();
        }

        //*see add section of program.cs
        public static void SortAndSend(Book[] myBooks)
        {
            BookUtility.SelectionSort(myBooks);
            PrintBooks(myBooks);
            // clear and re-send sorted and edited array (update the text file)
            string path = "books.txt";
            File.WriteAllText(path, String.Empty);
            TextWriter tW = new StreamWriter(path, true);
            tW.Close();
            ToFile(myBooks);
        }

        public static void LineDelete(Book[] myBooks)
        {
            Console.WriteLine("Enter the ISBN of a book you'd like to remove from inventory (book file): ");
            int choice = int.Parse(Console.ReadLine());

            int indexFound = BookUtility.BinarySearch(myBooks, choice);

            string path2 = "books.txt";
            File.WriteAllText(path2, String.Empty);
            TextWriter tW2 = new StreamWriter(path2, true);
            tW2.Close();
            myBooks.ToString();
            using(StreamWriter sW = File.AppendText("books.txt"))
            for (int i = 0; i < indexFound; i++)
            {
                sW.WriteLine(myBooks[i].GetISBN() + "#" + myBooks[i].GetTitle() + "#" + myBooks[i].GetAuthor() + "#" + myBooks[i].GetGenre() + "#" + myBooks[i].GetListeningTime() + "#" + myBooks[i].GetStatus() + "#" + myBooks[i].GetBookCount());
            }
            using(StreamWriter outFile = File.AppendText("books.txt"))
            for(int j = indexFound + 1; j < GetCount(); j++)
            {
                outFile.WriteLine(myBooks[j].GetISBN() + "#" + myBooks[j].GetTitle() + "#" + myBooks[j].GetAuthor() + "#" + myBooks[j].GetGenre() + "#" + myBooks[j].GetListeningTime() + "#" + myBooks[j].GetStatus() + "#" + myBooks[j].GetBookCount());
            }
        }


        //tofile used to write info into text file with # delimeter
        public static void ToFile(Book[] myBooks)
        {
            myBooks.ToString();
            using(StreamWriter sW = File.AppendText("books.txt"))
            for (int i = 0; i < Book.GetCount(); i++)
                {
                    sW.WriteLine(myBooks[i].GetISBN() + "#" + myBooks[i].GetTitle() + "#" + myBooks[i].GetAuthor() + "#" + myBooks[i].GetGenre() + "#" + myBooks[i].GetListeningTime() + "#" + myBooks[i].GetStatus() + "#" + myBooks[i].GetBookCount());
                }
        }

        public int CompareTo(Book count)
        {
            return this.ISBN.CompareTo(count.GetISBN());
        }
         public int CompareToGenre(Book count)
        {
            return this.genre.CompareTo(count.GetGenre());
        }

        public bool Equals(string tempStatus) 
        {
            return this.status == tempStatus;
        }
        
        public static void PrintBooks(Book[] myBooks)
        {
            for (int i = 0; i < GetCount(); i++)
            {
                Console.WriteLine(myBooks[i].ToString());
            }
        }

    }

}