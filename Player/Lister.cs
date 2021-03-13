using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Player
{
    public class Lister
    {
        public string CurrentPath { get; private set; } 
        public Lister(string path = @"C:\")
        {
            CurrentPath = path;
        }
        public List<string> GetChildrenNames()
        {
            var len = CurrentPath.Length;
            return Directory.GetDirectories(CurrentPath)
                .Select(x => x.Substring(len))
                .Concat(Directory.GetFiles(CurrentPath)
                    .Select(x => x.Substring(len)))
                .ToList();
            
        }
        /*
        public string[] GetDirectories()
        {
            return Directory.GetDirectories(CurrentPath);
        }
        public string[] GetFiles()
        {
            return Directory.GetFiles(CurrentPath);
        }
        */
    }
}
