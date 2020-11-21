namespace PA5
{
    public class TranUtility
    {
        private Transactions[] myTransactions;

        public TranUtility(Transactions[] myTransactions)
        {
            this.myTransactions = myTransactions;
        }
    
        public void SelectionSort(Transactions[] myTransactions)
        {
            for (int i = 0; i < Transactions.GetTranCount() - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < Transactions.GetTranCount(); j++)
                { 
                    if(myTransactions[min].CompareTo(myTransactions[j]) > 0)
                    {
                        min = j;
                    }
                }
                if(min != i)
                {
                    Swap(myTransactions, min, i);
                }
            }
        }

        public void Swap(Transactions[] myTransactions, int x, int y )
        {
            Transactions temp = myTransactions[x];
            myTransactions[x] = myTransactions[y];
            myTransactions[y] = temp;
        }

        public static void SelectionSortHist(Transactions[] myTransactions)
        {
            for (int i = 0; i < Transactions.GetTranCount() - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < Transactions.GetTranCount(); j++)
                { 
                    if(myTransactions[min].CompareToHist(myTransactions[j]) > 0)
                    {
                        min = j;
                    }
                }
                if(min != i)
                {
                    SwapHist(myTransactions, min, i);
                }
            }
        }

        public static void SwapHist(Transactions[] myTransactions, int x, int y )
        {
            Transactions temp = myTransactions[x];
            myTransactions[x] = myTransactions[y];
            myTransactions[y] = temp;
        }

        public static int BinarySearch(Transactions[] myTransactions, int searchVal)
        {
            bool notFound = true;
            int foundIndex = -1;

            int first = 0;
            int last = Transactions.GetTranCount() - 1;

            while(notFound && first <= last)
            {
                int middle = (first + last) / 2;
                
                if(searchVal == myTransactions[middle].GetTranISBN())
                {
                    notFound = false;
                    foundIndex = middle;
                }
                else if(searchVal > myTransactions[middle].GetTranISBN())
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