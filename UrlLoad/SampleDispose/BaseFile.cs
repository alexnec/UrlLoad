using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlLoad.SampleDispose
{
    public class BaseFile : IDisposable
    {
        private bool disposed = false;
        protected FileStream fs;

        public BaseFile()
        {
            fs = new FileStream(Environment.CurrentDirectory + "test.txt", FileMode.Create, FileAccess.Write);
        }

        public void WriteBaseInfo()
        {
            byte[] array = System.Text.Encoding.Default.GetBytes("It`s base info");
            fs.Write(array, 0, array.Length);
        }

        // not sealed
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //free only manage resourses
                }

                //free unmanaged resourses
                disposed = true;
            }
        }

        //for sealed
        //private void Dispose(bool disposing) { }

        public void Dispose()
        {
            //fs.Close();
            Dispose(true);
            // stop run finalize
            GC.SuppressFinalize(this);
        }

        ~BaseFile()
        {
            Dispose(false);
        }
    }
}
