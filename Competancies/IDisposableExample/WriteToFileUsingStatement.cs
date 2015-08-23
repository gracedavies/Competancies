namespace Competancies.IDisposableExample
{
    using System;
    using System.IO;
    using System.Text;

    public class WriteToFileUsingStatement
    {
        public void WriteToFileCorrectly()
        {
            /* The File.Create method returns a FileStream which inherits from the abstract 
             * Stream class which implements IDisposable. Therefore we need to explicitly say
             *  when we need to clean up. One way to do this is by using a using statement like below.
             *  When running now we can open our text file in notepad. As the using statement 
             *  creates a scope where the object can be used and then automatically calls Dispose
             *  after it is no longer needed.
             */
            var bytes = Encoding.Default.GetBytes("Writing to file");
            using (var stream = File.Create(@"c:\canNotDispose.txt"))
            {
                stream.Write(bytes, 0, bytes.Length);
                Console.ReadLine();
            }
        }
    }
}
