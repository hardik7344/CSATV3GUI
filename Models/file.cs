using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OwnYITCSAT.Models
{
    public class file
    {
        public string filepath { get; set; }
        public string filename { get; set; }

        internal static Task CopyToAsync(FileStream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}
