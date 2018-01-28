using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FiremanWin.Controls
{
    public partial class AutoCompleteListControl : UserControl
    {
        public AutoCompleteListControl()
        {
            InitializeComponent();
        }

        public List<String> Items
        {
            get
            {
                var list = (from object item in listBox.Items select item.ToString()).ToList();
                return list;
            }
            set
            {
                listBox.Items.Clear();
                listBox.Items.AddRange(value?.ToArray());
            }
        }
    }
}
