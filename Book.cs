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

        public Book(int ISBN, string title, string author, string genre, double listeningTime, string status, int bookCount)
        {
            this.ISBN = ISBN; 
            this.title = title;
            this.author = author;
            this.genre = genre;
            this.listeningTime = listeningTime;
            this.status = status;
            this.bookCount = bookCount;
            //count++;
        }
        
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

        public static void DecrCount()
        {
            count--;
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


        public static Book[] GetBookData()
        {

            StreamReader sR = new StreamReader("books.txt");

            // string test = sR.ReadLine();
            // string[] array = test.Split('#');
            // Book[] myBooks = new Book[array.Length];

            BookFile data = new BookFile("books.txt");
            Book[] myBooks = data.ReadBookData();
            
            Console.WriteLine("Enter book ISBN (-1 to stop): \n");
            int ISBN = int.Parse(Console.ReadLine());
            
            while(ISBN != -1)
            {
                int indexFound = BookUtility.BinarySearch(myBooks, ISBN);
                Console.WriteLine(indexFound);
                if(indexFound == -1)
                {
                    int bookCount = 0;

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
                else
                {
                    int copies = myBooks[indexFound].GetBookCount() + 1;
                    myBooks[indexFound].SetBookCount(copies);
                    ISBN = -1;
                }
                
            }


            return myBooks;
        }

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