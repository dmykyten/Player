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
        private List<FileSystemInfo> children = null;
        public FileSystemInfo currentPath;
        public FileSystemInfo CurrentPath
        {
            get
            {
                return currentPath;
            }
            set
            {
                currentPath = value;
                children = GetChildren();
            }
        }
        private FileSystemInfo GetPathInfo(string path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path);
            }
            else if(Directory.Exists(path))
            {
                return new DirectoryInfo(path);
            }
            return null;
        }

        internal void ChangePath(string selectedItem)
        {
            if(children != null)
            {
                var path = children.Where(x => x.Name == selectedItem).SingleOrDefault();
                if(path != null)
                {
                    CurrentPath = path;
                }
            }
        }

        public Lister(string path = @"C:\")
        {
            CurrentPath = GetPathInfo(path);
        }
        private List<FileSystemInfo> GetChildren()
        {
            if (CurrentPath != null && CurrentPath.Attributes.HasFlag(FileAttributes.Directory))
            {
                return (CurrentPath as DirectoryInfo).GetFileSystemInfos().ToList();
            }
            return null;
        }
        public List<string> GetChildrenNames()
        {
            if(children != null)
            {
                return children.Select(x => x.Name).ToList();
            }
            return new List<string>();
        }
    }
}
