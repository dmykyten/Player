using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Player
{
    public interface IListItem
    {
        List<IListItem> GetChildren();
        string GetPath();
        bool IsAccessibleDirectory();
        string ToString();
    }
    class DisksListItem : IListItem
    {
        public List<IListItem> GetChildren()
        {
            var drives = DriveInfo.GetDrives().Where(x => x.IsReady == true).Select(x => new ListItem(x.RootDirectory.FullName)).ToList<IListItem>();
            return drives;
        }
        public bool IsAccessibleDirectory()
        {
            return true;
        }
        public string GetPath()
        {
            return "\\\\";
        }
        public override string ToString()
        {
            return "..";
        }
    }
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

    public class ListItem : IListItem
    {
        private List<FileSystemInfo> childrenFiles = null;

        private FileSystemInfo CurrentPath { get; set; }
        public ListItem(string path)
        {
            CurrentPath = GetPathInfo(path);
        }
        public string GetPath()
        {
            return CurrentPath.FullName;
        }
        public bool IsAccessibleDirectory()
        {
            try
            {
                return CurrentPath.Attributes.HasFlag(FileAttributes.Directory) && GetChildren().Count > -1;
            }
            catch (UnauthorizedAccessException e)
            {
                return false;
            }

        }
        public List<IListItem> GetChildren()
        {
            var children = GetChildrenFiles().Select(x => new ListItem(x.FullName)).ToList<IListItem>();
            if (CurrentPath.Attributes.HasFlag(FileAttributes.Directory))
            {
                var parent = (CurrentPath as DirectoryInfo).Parent;
                if (parent != null)
                {
                    children = children.Prepend(new ParentListItem(parent.FullName)).ToList();
                }
                else
                {
                    children = children.Prepend(new DisksListItem()).ToList();
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
            if (childrenFiles != null)
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
            if (CurrentPath.Attributes.HasFlag(FileAttributes.Directory))
            {
                return '[' + CurrentPath.Name + ']';
            }
            return CurrentPath.Name;
        }
    }
}
