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
        public ListItem currentItem;
        public ListItem CurrentItem
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
        public List<ListItem> GetChildren()
        {
            return CurrentItem.GetChildren();
        }
        public void ChangeItem(ListItem selectedItem)
        {
            currentItem = selectedItem;

        }
    }
}
