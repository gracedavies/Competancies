namespace Competancies.IDisposableExample
{
    using System;
    using System.IO;
    using System.Text;

    public class MisusingIDisposable
    {
        /*When running the below, if you try and open the 
        * file it will come up with a pop up saying you can't access it as it is being
        * used by another process. This is because .Net does not know that we are done
        * with the stream so it keeps it alive. One way to over come this is by using a 
        * using statement.
        * */
        public void WriteToFile()
        {
            var bytes = Encoding.Default.GetBytes("Writing to file");
            var stream = File.Create(@"c:\canNotDispose.txt");
            stream.Write(bytes, 0, bytes.Length);
            Console.ReadLine();
        }

        /*
         * Running this will cause an InvalidOperationException due to a timeout expiry as all 
         * previous objects which have made a connection have not been disposed of. Therefore there will
         * be no free connections in the pool left. 
         * To fix this we would need to end the app, as we don't know when the GC will be called. 
         */
        public void OpeningSQLConnection()
        {
            var usingIDisposableIncorrectly = new UsingIDisposableCorrectlySQlConnection();
            usingIDisposableIncorrectly.OpeningSQLConnectionCorrectly();
        }
    }
}
