using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DuszaFogadas
{
    /// <summary>
    /// Interaction logic for JatekLetrehozasa.xaml
    /// </summary>
    public partial class JatekLetrehozasa : Window
    {
        public JatekLetrehozasa()
        {
            InitializeComponent();
        }

        private void btnAddNewParticipant_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewParticipant.Text.Trim() != "")
            {
                lbParicipants.Items.Add(txtNewParticipant.Text);
                txtNewParticipant.Clear();
            }
        }

        private void lbParicipants_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = lbParicipants.SelectedIndex;

            if (index > -1)
            {
                lbParicipants.Items.RemoveAt(index);
            }
        }


        private void btnAddNewEvent_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewEvent.Text.Trim() != "")
            {
                lbEvents.Items.Add(txtNewEvent.Text);
                txtNewEvent.Clear();
            }
        }

        private void lbEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = lbEvents.SelectedIndex;

            if (index > -1)
            {
                lbEvents.Items.RemoveAt(index);
            }
        }

    }
}
