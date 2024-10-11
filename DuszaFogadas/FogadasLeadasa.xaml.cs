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
    /// Interaction logic for FogadasLeadasa.xaml
    /// </summary>
    public partial class FogadasLeadasa : Window
    {
        MySqlConnection conn;
        ObservableCollection<Game> games = new ObservableCollection<Game>();
        ObservableCollection<string> participants = new ObservableCollection<string>();
        ObservableCollection<string> events = new ObservableCollection<string>();
        
        public FogadasLeadasa()
        {
            InitializeComponent();
            cbEvent.ItemsSource = events;
            cbParticipant.ItemsSource = participants;
            conn = GetMysqlConnection.getMysqlConnection();
            conn.Open();
            MySqlCommand com = new MySqlCommand("SELECT * from jatekok", conn);
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                games.Add(new Game(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), new List<string>(), new List<string>()));
            }
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
                while ( reader.Read())
                {
                    game.Events.Add(reader.GetString("nev"));
                }
                reader.Close();
            }
            conn.Close();
            cbGame.ItemsSource = games;
            cbGame.SelectedIndex = 0;
        }

        private void cbGame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbParticipant.ItemsSource = games[cbGame.SelectedIndex].Participants;
            cbEvent.ItemsSource = games[cbGame.SelectedIndex].Events;
        }
    }
}
