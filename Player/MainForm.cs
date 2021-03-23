using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Player
{
    public partial class MainForm : Form
    {
        Lister lister = new Lister();
        public MainForm()
        {
            InitializeComponent();
            lister.GetChildren().ForEach(x => FilesListBox.Items.Add(x));
            this.FilesListBox.SetSelected(0, true);
        }

        private void FilesListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if(e.KeyChar == '\r')
            {
                var selectedItem = (ListItem)this.FilesListBox.SelectedItem;
                lister.ChangeItem(selectedItem);
                FilesListBox.Items.Clear();
                lister.GetChildren().ForEach(x => FilesListBox.Items.Add(x));
                this.FilesListBox.SetSelected(0, true);
            }
        }
    }
}
