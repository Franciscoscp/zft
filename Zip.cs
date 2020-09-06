using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace zft
{
    public class Zip 
    {
        public int id { get; set; }
        public string name { get; set; }
        public Uri zipUri { get; set; }

        public LinkedList<zip.ZipInfo> data { get; set; }
 
        public override string ToString()
        {
            
            return base.ToString()+ " - "+zipUri.ToString();

        }
    
    }
}
namespace zip
{
    public class ZipInfo
    {
       public string name { get; set; }

       long _compressedSize;

       public double compressedSize
        {
            
            get { return ConvertBytesToKbytes(_compressedSize); }
            set { _compressedSize = (long)value; }

        }


       long _fullSize;

       public double fullSize
        {
            get { return ConvertBytesToKbytes(_fullSize); }
            set { _fullSize = (long)value; }
        }





        static double ConvertBytesToKbytes(long bytes)
        {
            return (bytes / 1024f);
        }



    }
}
