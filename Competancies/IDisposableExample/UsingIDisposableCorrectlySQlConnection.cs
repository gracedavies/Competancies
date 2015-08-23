namespace Competancies.IDisposableExample
{
    using System;
    using System.Data.SqlClient;

    public class UsingIDisposableCorrectlySQlConnection : IDisposable
    {
        private SqlConnection sqlConnection;

        /*
         * This following class implements the IDisposable interface. It has a dispose method which
         * closes the sql connection object it uses and calls its dispose method. Using this class
         * within a Using block will ensure that its Dispose method will be called.
         * 
         */

        public void OpeningSQLConnectionCorrectly()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(@"Data Source=LPX-14-2797FS\GRACESDATABASE;Initial Catalog=DoctorsSurgery;Integrated Security=True");
                sqlConnection.Open();
            }

            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Appointments";
            }       
        }

        public void Dispose()
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }
    }
}
