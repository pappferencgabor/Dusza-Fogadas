using DuszaFogadas.Helpers;
using DuszaFogadas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        User user;

        public FogadasLeadasa(User usr)
        {
            user = usr;
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

        private void txtBet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBet.Text != "")
            {
                if (Convert.ToInt32(txtBet.Text) > user.Points)
                {
                    MessageBox.Show("Nincs ennyi pontod!", "Ponttullepes", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtBet.Text = "0";
                }
                else if (Convert.ToInt32(txtBet.Text) < 0)
                {
                    MessageBox.Show("Nem lehet negativ a fogadas!", "Nullpont", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtBet.Text = "0";
                }
            }
            

        }

        private void txtBet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void btnSendGuess_Click(object sender, RoutedEventArgs e)
        {
            int bet = Convert.ToInt32(txtBet.Text);
            user.Points -= bet;
            conn.Open();
            MySqlCommand com = new MySqlCommand("INSERT INTO fogadasok (felhasznaloid, jatekId, alanyId, esemenyId, tet, fogadasErteke) SELECT @felhasznaloid, @jatekid, (SELECT id FROM alanyok WHERE nev = @alanynev AND jatekId = @jatekid), (SELECT id FROM esemenyek WHERE nev = @esemenynev AND jatekId = @jatekid), @tet, @fogadasErteke;", conn);
            com.Parameters.AddWithValue("@felhasznaloid", user.Id);
            com.Parameters.AddWithValue("@jatekId", games[cbGame.SelectedIndex].id);
            com.Parameters.AddWithValue("@alanynev", cbParticipant.Text);
            com.Parameters.AddWithValue("@esemenynev", cbEvent.Text);
            com.Parameters.AddWithValue("@tet", txtGuess.Text);
            com.Parameters.AddWithValue("@fogadasErteke", bet);

            com.ExecuteNonQuery();
            com = new MySqlCommand("UPDATE felhasznalok SET pontok = @pontok WHERE id=@felhasznaloid", conn);
            com.Parameters.AddWithValue("@pontok", user.Points);
            com.Parameters.AddWithValue("@felhasznaloid", user.Id);
            com.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Sikeresen feltette a tetet", "Tet feltetel", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
