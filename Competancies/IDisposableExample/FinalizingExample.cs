using System;
using System.Data.SqlClient;

namespace Competancies.IDisposableExample
{
    /*
     * The most common type of unmanaged resource is an object that wraps an operating system resource such as 
     * a file, db connection etc. Although the GC can track the lifetime of an object that encapsulates an unmanaged
     * resource it does not have specific knowledge of how to clean it up. To do this we use the Finalize method. If you 
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
     * 
     * 
     * The below pattern ensures that all references are properly diposed and released. Using the finalizer along with the
     * dispose method will ensure all references will be properly released. 
     * 
     * 
     */

    public class FinalizingExample : IDisposable
    {
        private SqlConnection sqlConnection;
        private bool disposed = false;

        public FinalizingExample()
        {
            sqlConnection = new SqlConnection(@"Data Source=LPX-14-2797FS\GRACESDATABASE;Initial Catalog=DoctorsSurgery;Integrated Security=True");
                
        }


        /*
         * This is a finalizer method. Immediately before the GC releases an object instance it calls the object's finalizer.
         * Since an objects finalizer is only called by the GC and only when there are no other references to the object this shows
         * the Dispose method will never be called on it unless it is called from the Finalizer. 
         * The finalizer method will call into the Dispose(string disposing) method passing in false as it will only dispose
         * of itself and not of its objects it is referencing.  
        */
        ~FinalizingExample()
        {
            Dispose(false);
        }

        public void OpeningSQLConnectionCorrectly()
        {
            if (sqlConnection == null)
            {
                sqlConnection.Open();
            }

            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Appointments";
            }
        }

        /*
         * All resource cleanup should be contained within the below dispose method. If the value is true then the method
         * has been called directly or indirectly by user's code and both managed and unmanaged resources can be disposed.
         *  If disposing equals false the the method has be called by the runtime from inside the finalizer and you should 
         *  not reference other object. Only unmanaged resources can be disposed. 
         */

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (sqlConnection != null)
                    {
                        //freeing other managed objects that implement IDisposable only
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                    }
                }
                
                //here you can release any unmanaged objects and set their object reference to null.  
            } 
            
           disposed = true;
        }


        /*
         * Calls the above Dispose method. It is never automatically called by the CLR only explicitly by the
         * owner of the object. The ordering of the below ensures that GC.SupressFinalize only gets called if the
         * Dispose operation completes successfully. 
         */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
