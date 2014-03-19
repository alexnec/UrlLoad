using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlLoad.SampleDispose
{
    public class Speсial : BaseFile
    {
        public void WriteSpecialInfo()
        {
            byte[] array = System.Text.Encoding.Default.GetBytes("It`s special info");
            fs.Write(array, 0, array.Length);
        }
    }
}
