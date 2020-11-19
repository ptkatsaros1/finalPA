using System;
using System.IO;
namespace PA5
{
    public class BookFile
    {
        private string fileName;

        public BookFile(string fileName)
        {
            this.fileName = fileName;
        }
        public void SetFileName(string fileName)
        {
            this.fileName = fileName;
        }

        public string GetFileName()
        {
            return fileName;
        }
        public Book[] ReadBookData()
        {

            StreamReader inFile = new StreamReader(fileName);

            Book[] myBooks = new Book[200];
            string data = inFile.ReadLine();
            Book.SetCount(0);
            while(data != null)
            {
                string[] temp = data.Split('#');
                
                int tempISBN = int.Parse(temp[0]);
                string tempTitle = temp[1];
                string tempAuthor = temp[2];
                string tempGenre = temp[3];
                double tempListeningTime = double.Parse(temp[4]);
                string tempStatus = temp[5];
                int tempBookCount  = int.Parse(temp[6]);

                myBooks[Book.GetCount()] = new Book(tempISBN, temp[1], temp[2], temp[3], tempListeningTime, temp[5], tempBookCount);
                myBooks[Book.GetCount()].ToString();
                Book.IncrCount();

                data = inFile.ReadLine();
            }

            inFile.Close();

            return myBooks;

        }

    }
}