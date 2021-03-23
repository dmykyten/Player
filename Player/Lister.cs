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
        public IListItem currentItem;
        public IListItem CurrentItem
        {
            get
            {
                return currentItem;
            }
            set
            {
                currentItem = value;
            }
        }
        public Lister(string path = @"C:\")
        {
            CurrentItem = new ListItem(path);
        }
        public List<IListItem> GetChildren()
        {
            return CurrentItem.GetChildren();
        }
        public bool ChangeItem(IListItem selectedItem)
        {
            if (selectedItem.IsAccessibleDirectory())
            {
                currentItem = selectedItem;
            }
            return selectedItem.IsAccessibleDirectory();
        }
        public string GetPath()
        {
            return currentItem.GetPath();
        }
    }
}
