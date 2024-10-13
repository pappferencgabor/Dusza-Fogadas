using DuszaFogadas.Helpers;
using DuszaFogadas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for JatekLezarasa.xaml
    /// </summary>
    public partial class JatekLezarasa : Window
    {
        MySqlConnection conn;
        ObservableCollection<Game> games = new ObservableCollection<Game>();
        ObservableCollection<string> closedEvents = new ObservableCollection<string>();
        ObservableCollection<string> eventValue = new ObservableCollection<string>();
        User user;
        
        public JatekLezarasa(User usr)
        {
            user = usr;
            InitializeComponent();
            conn = GetMysqlConnection.getMysqlConnection();
            conn.Open();
            MySqlCommand com = new MySqlCommand("SELECT * FROM jatekok WHERE status = 'Aktiv'", conn);
            MySqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                games.Add(new Game(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), new List<string>(), new List<string>()));
            }

            cbGame.ItemsSource = games;
            cbGame.SelectedIndex = 0;
            cbEvent.ItemsSource = games[cbGame.SelectedIndex].Events;
            reader.Close();
            foreach (Game game in games)
            {
                com = new MySqlCommand("SELECT * from alanyok WHERE jatekid = (@id)", conn);
                com.Parameters.AddWithValue("@id", game.id);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    game.Participants.Add(reader.GetString("nev"));
                }
                reader.Close();
                com = new MySqlCommand("SELECT * from esemenyek WHERE jatekid = (@id)", conn);
                com.Parameters.AddWithValue("@id", game.id);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    game.Events.Add(reader.GetString("nev"));
                }
                reader.Close();
            }

            lbClosedEvents.ItemsSource = closedEvents;
            conn.Close();
        }

        private void btnSaveResult_Click(object sender, RoutedEventArgs e)
        {
            closedEvents.Add(games.ElementAt(cbGame.SelectedIndex).Events.ElementAt(cbEvent.SelectedIndex));
            eventValue.Add(txtResult.Text);
            games.ElementAt(cbGame.SelectedIndex).Events.RemoveAt(cbEvent.SelectedIndex);
            cbEvent.Items.Refresh();

        }

        private void btnCloseGame_Cick(object sender, RoutedEventArgs e)
        {
            //if ( games.ElementAt(cbGame.SelectedIndex).Events.Count != 0)
            //{
            //    MessageBox.Show("Nem menthetsz ameddig nem toltotted ki az osszes esemeny kimenetet!");

            //} else
            //{
            //    MySqlCommand com;
            //    for (int i = 0; i < closedEvents.Count; i++)
            //    {
            //        conn.Open();
            //        com = new MySqlCommand("INSERT INTO esemenyek (jatekId, alanyId, esemenyId, esemenyErteke, szorzo) SELECT @jatekId, @alanyId, (SELECT id from esemenyek WHERE nev = @esemenynev), @esemenyErteke, @szorzo", conn);
            //        com.Parameters.AddWithValue("@jatekId", games[cbGame.SelectedIndex].id);
            //        com.Parameters.AddWithValue("@alanyId", user.Id);
            //        com.Parameters.AddWithValue("@esemenynev", closedEvents[i]);
            //        com.Parameters.AddWithValue("@esemenyErteke", eventValue[i]);
            //        com.Parameters.AddWithValue("@szorzo", float.Parse("2"));
            //        com.ExecuteNonQuery();
            //        conn.Close();
            //    }
            //    conn.Open();
            //    com = new MySqlCommand("UPDATE jatekok SET status = 'Inaktiv' WHERE id = @id", conn);
            //    com.Parameters.AddWithValue("@id", games[cbGame.SelectedIndex].id);
            //    com.ExecuteNonQuery();
            //}
                
            
        }
    }
}
