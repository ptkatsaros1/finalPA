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

        public int BinarySearch(Transactions[] myTransactions, int searchVal)
        {
            bool notFound = true;
            int foundIndex2 = -1;

            int first = 0;
            int last = Transactions.GetTranCount() - 1;

            while(notFound && first <= last)
            {
                int middle = (first + last) / 2;
                
                if(searchVal == myTransactions[middle].GetTranISBN())
                {
                    notFound = false;
                    foundIndex2 = middle;
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
            return foundIndex2;
        }
    }
}