using System;
using System.IO;
namespace PA5
{
    public class BookUtility
    {
        private Book[] myBooks;

        public BookUtility(Book[] myBooks)
        {
            this.myBooks = myBooks;
        }

        //sorts book array to prepare for binary search
        public static void SelectionSort(Book[] myBooks)
        {
            for (int i = 0; i < Book.GetCount() - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < Book.GetCount(); j++)
                { 
                    if(myBooks[min].CompareTo(myBooks[j]) > 0)
                    {
                        min = j;
                    }
                }
                if(min != i)
                {
                    Swap(myBooks, min, i);
                }
            }
        }

        public static void Swap(Book[] myBooks, int x, int y )
        {
            Book temp = myBooks[x];
            myBooks[x] = myBooks[y];
            myBooks[y] = temp;
        }

        public static void SelectionSortGenre(Book[] myBooks)
        {
            for (int i = 0; i < Book.GetCount() - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < Book.GetCount(); j++)
                { 
                    if(myBooks[min].CompareToGenre(myBooks[j]) > 0)
                    {
                        min = j;
                    }
                }
                if(min != i)
                {
                    SwapGenre(myBooks, min, i);
                }
            }
        }

        public static void SwapGenre(Book[] myBooks, int x, int y )
        {
            Book temp = myBooks[x];
            myBooks[x] = myBooks[y];
            myBooks[y] = temp;
        }


        //binary search used for finding ISBN within book array, returns index of ISBN found
        public static int BinarySearch(Book[] myBooks, int searchVal)
        {
            bool notFound = true;
            int foundIndex = -1;

            int first = 0;
            int last = Book.GetCount() - 1;

            while(notFound && first <= last)
            {
                int middle = (first + last) / 2;
                
                if(searchVal == myBooks[middle].GetISBN())
                {
                    notFound = false;
                    foundIndex = middle;
                }
                else if(searchVal > myBooks[middle].GetISBN())
                {
                    first = middle + 1;
                }
                else 
                {
                    last = middle -1;
                }
            }
            return foundIndex;
        }

    }
}