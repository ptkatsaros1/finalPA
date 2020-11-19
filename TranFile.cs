using System.IO;
namespace PA5
{
    public class TranFile
    {

        private string fileName;
        public TranFile(string fileName)
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
        public Transactions[] ReadTranData()
        {

            StreamReader inFile = new StreamReader(fileName);

            Transactions[] myTransactions = new Transactions[200];
            string data = inFile.ReadLine();
            Transactions.SetTranCount(0);
            while(data != null)
            {
                string[] temp = data.Split('#');
                
                int tempRentID = int.Parse(temp[0]);
                int tempTranISBN = int.Parse(temp[1]);
                string custName = temp[2];
                string custEmail = temp[3];
                string rentDate = temp[4];
                string returnDate = temp[5];

                myTransactions[Transactions.GetTranCount()] = new Transactions(tempRentID, tempTranISBN, temp[2], temp[3], temp[4], temp[5]);
                myTransactions[Transactions.GetTranCount()].ToString();
                Transactions.IncrTranCount();

                data = inFile.ReadLine();
            }

            inFile.Close();

            return myTransactions;

        }
    }
}