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

        public void Dispose()
        {
            fs.Close();
        }
    }
}
