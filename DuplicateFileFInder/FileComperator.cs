using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class FileComperator
    {
        private readonly string path;

        public FileComperator( string path )
        {
            this.path = path;
        }

        public List<List<FileHandle>> run()
        {
            string[] files = Directory.GetFiles(path);
            Console.WriteLine("path: {0}", path);
            Console.WriteLine("file count: {0}", files.Length);

            var dict = new Dictionary<long, List<FileHandle>>();

            for (int i = 0; i < files.Length; i++)
            {
                string filePath = files[i];

                var handle = new FileHandle(filePath);
                
                if (dict.ContainsKey(handle.Length))
                {
                    dict[handle.Length].Add(handle);
                } else
                {
                    dict.Add(handle.Length, new List<FileHandle> { handle });
                }
            }

            var result = new List<List<FileHandle>>();
            foreach ( long key in dict.Keys)
            {
                if ( dict[key].Count > 1 )
                {
                    result.Add(dict[key]);
                }
            }
            return result;
        }
    }
}
