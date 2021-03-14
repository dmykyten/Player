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
            lister.GetChildrenNames().ForEach(x => FilesListBox.Items.Add(x));

        }

        private void FilesListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if(e.KeyChar == '\r')
            {
                var selectedItem = this.FilesListBox.SelectedItem.ToString();
                lister.ChangePath(selectedItem);
                FilesListBox.Items.Clear();
                lister.GetChildrenNames().ForEach(x => FilesListBox.Items.Add(x));
            }
        }
    }
}
