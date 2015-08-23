namespace Competancies
{
    using IDisposableExample;

    public class Program
    {
        static void Main(string[] args)
        {
            /* (Run  as Admin) */

            var misusingIDisposable = new MisusingIDisposable();
            
            /*
             * Example using a file
             */
            //misusingIDisposable.WriteToFile();

            //var writeToFileUsingStatement = new WriteToFileUsingStatement();
            //writeToFileUsingStatement.WriteToFileCorrectly();

            //

            /*
             * Example with a SQLConnection
             */

            for (var i = 0; i < 10000; i++)
            {
                misusingIDisposable.OpeningSQLConnection();
            }

            //for (var i = 0; i < 10000; i++)
            //{
            //    using (var disposable = new UsingIDisposableCorrectlySQlConnection())
            //    {
            //        disposable.OpeningSQLConnectionCorrectly();
            //        Console.WriteLine("Opened Connection");
            //    }           
            //}   
        }
    }
}
