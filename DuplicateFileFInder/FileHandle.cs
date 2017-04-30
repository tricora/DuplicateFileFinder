using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class FileHandle
    {
        public string Path { get; private set; }

        public long Length { get; private set; }

        public FileHandle( string path )
        {
            Path = path;
            FileInfo fi = new FileInfo(Path);
            Length = fi.Length;
        }

        public override string ToString()
        {
            return string.Format("\"{0}\" - {1} Bytes", Path, Length);
        }
    }
}
