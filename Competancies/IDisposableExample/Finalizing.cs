using System;
using System.Data.SqlClient;

namespace Competancies.IDisposableExample
{
    /*
     * The most common type of unmanaged resource is an object that wtaps an operating system resource such as 
     * a file, db connection etc. Although the GC can track the lifetime of an object that encapsulates an unmanaged
     * resource it does not have specific knowledge of how to clean it up. To do this we use Finalize method. If you 
     * want the GC to reclaim memory used by your object you must override this method - by default it does nothing.
     * The GC keeps track of objects which have finalize methods using the finalization queue. Each time your app creates
     * an object that has a Finalize method the GC places an entry into the queue that points to that object. 
     * 
     * Reclaiming memory used by objects with Finalize methods requires at least two GC collections. When the GC performs
     * a collection it reclaims memory for inaccessible objects without finalizers. At this time it cannot collect the 
     * inaccessible objects with finalizers. Instead it removes the entries for these objects from the finalization queue
     * and places them in a list of objects marked to have their finalization code called. A future collection will ensure 
     * they are no longer in memory. 
     * 
     */

    public class Finalizing : IDisposable
    {
        private SqlConnection sqlConnection;
        private bool disposed = false;

        public Finalizing()
        {
            sqlConnection = new SqlConnection(@"Data Source=LPX-14-2797FS\GRACESDATABASE;Initial Catalog=DoctorsSurgery;Integrated Security=True");
                
        }

        ~Finalizing()
        {
            Dispose(false);
        }

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (sqlConnection != null)
                    {
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                    }
                }
                disposed = true;
            }
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
