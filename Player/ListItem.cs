using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Player
{
    class ParentListItem : ListItem
    {
        public ParentListItem(string path) : base (path)
        {

        }
        public override string ToString()
        {
            return "..";
        }
    }

    public class ListItem
    {
        private List<FileSystemInfo> childrenFiles = null;
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
                //children = GetChildren();
            }
        }
        public ListItem(string path)
        {
            currentPath = GetPathInfo(path);
        }
        public List<ListItem> GetChildren()
        {
            var children = GetChildrenFiles().Select(x => new ListItem(x.FullName)).ToList();
            if (currentPath.Attributes.HasFlag(FileAttributes.Directory))
            {
                var parent = (currentPath as DirectoryInfo).Parent;
                if (parent != null)
                {
                    children = children.Prepend(new ParentListItem(parent.FullName)).ToList();
                }
            }
            return children;  
        }
        private FileSystemInfo GetPathInfo(string path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path);
            }
            else if (Directory.Exists(path))
            {
                return new DirectoryInfo(path);
            }
            return null;
        }
        private List<FileSystemInfo> GetChildrenFiles()
        {
            if(childrenFiles != null)
            {
                return childrenFiles;
            }
            else if (CurrentPath != null && CurrentPath.Attributes.HasFlag(FileAttributes.Directory))
            {
                childrenFiles = (CurrentPath as DirectoryInfo).GetFileSystemInfos().ToList();
                return childrenFiles;
            }
            return null;
        }
        public override string ToString()
        {
            if (currentPath.Attributes.HasFlag(FileAttributes.Directory))
            {
                return '[' + currentPath.Name + ']';
            }
            return currentPath.Name;
        }
    }
}
