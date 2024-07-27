using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KalkulatorKalorijaWPF2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, int> hrana;
        public ObservableCollection<KeyValuePair<string, int>> KeyValuePairs { get; set; }
        public MainWindow()
        {
            
            InitializeComponent();
            DataContext = this;
            /*Dictionary<string, int>*/ hrana = new Dictionary<string, int>
             {
                 //naglasiti da je ovo na 100g
                 {"jabuka", 52},
                 {"breskva", 46},
                 {"narandza", 54},
                 {"banana", 99},
                 {"piletina", 144},
                 {"riza", 368},
                 {"jaje", 167},
                 {"mlijeko", 66},
                 {"kravlji sir", 72},
                 {"hljeb", 252},
                 {"krompir", 85},
                 {"mrkva", 35},
                 {"orah", 654}
             };

            KeyValuePairs = new ObservableCollection<KeyValuePair<string, int>>(hrana);

            OnPropertyChanged("KeyValuePairs");
        }

       

        

        private void IspisDugme_Click(object sender, RoutedEventArgs e)
        {
            //ovdje dodati da ispise iz dictionary
            Otvori.Visibility = Visibility.Visible;
            Otvori.IsExpanded = true;

            Dugme1.Background = Brushes.Gainsboro;
            Dugme1.Content = "";
            Dugme2.Background = Brushes.Gainsboro;
            Dugme2.Content = "";
            Dugme3.Background = Brushes.Red;
            Dugme3.Content = "Prekini";

            Naziv1.Text = "";
            Naziv2.Text = "";

            Linija1.Background = Brushes.Gainsboro;
            Linija2.Background = Brushes.Gainsboro;

            Linija1.Clear();
            Linija2.Clear();
            Dugme1.IsEnabled = false;
            Dugme2.IsEnabled = false;
            Dugme3.IsEnabled = true;
            Linija1.IsEnabled = false;
            Linija2.IsEnabled = false;

            TrenRac.Text = "";
            BrojKcal.Text = "";
            kcal.Text = "";
        }

        private void UnosBrisDugme_Click(object sender, RoutedEventArgs e)
        {
            Otvori.Visibility = Visibility.Visible;
            Otvori.IsExpanded = true;

            Dugme1.IsEnabled = true;
            Dugme2.IsEnabled = true;
            Dugme3.IsEnabled = true;
            Linija1.IsEnabled = true;
            Linija2.IsEnabled = true;

            Dugme1.Background = Brushes.DarkSeaGreen;
            Dugme1.Content = "Dodaj";
            Dugme2.Background = Brushes.DarkSeaGreen;
            Dugme2.Content = "Ukloni";
            Dugme3.Background = Brushes.Red;
            Dugme3.Content = "Prekini";

            Naziv1.Text = "Ime proizvoda";
            Naziv2.Text = "Kalorijska vrijednost (100g)";

            Linija1.Background = Brushes.White;
            Linija2.Background = Brushes.White;

            TrenRac.Text = "";
            BrojKcal.Text = "";
            kcal.Text = "";

        }

        private void Dugme3_Click(object sender, RoutedEventArgs e)
        {
            if(Dugme3.Content == "Prekini")
            {
                Dugme1.Background = Brushes.Gainsboro;
                Dugme1.Content = "";
                Dugme2.Background = Brushes.Gainsboro;
                Dugme2.Content = "";
                Dugme3.Background = Brushes.Gainsboro;
                Dugme3.Content = "";

                Naziv1.Text = "";
                Naziv2.Text = "";

                Linija1.Background = Brushes.Gainsboro;
                Linija2.Background = Brushes.Gainsboro;

                Linija1.Clear();
                Linija2.Clear();
                Dugme1.IsEnabled = false;
                Dugme2.IsEnabled = false;
                Dugme3.IsEnabled = false;
                Linija1.IsEnabled = false;
                Linija2.IsEnabled = false;

                Otvori.Visibility = Visibility.Collapsed;
                Otvori.IsExpanded = false;

                TrenRac.Text = "";
                BrojKcal.Text = "";
                kcal.Text = "";
            }
        }

        private void KalkulatorStart_Click(object sender, RoutedEventArgs e)
        {
            Otvori.Visibility = Visibility.Visible;
            Otvori.IsExpanded = true;
            Dugme1.IsEnabled = true; 
            Dugme2.IsEnabled = true;
            Dugme3.IsEnabled = true;
            Linija1.IsEnabled = true;
            Linija2.IsEnabled = true;
            Dugme1.Background = Brushes.DarkSeaGreen;
            Dugme1.Content = "Saberi";
            Dugme2.Background = Brushes.DarkSeaGreen;
            Dugme2.Content = "Resetuj";
            Dugme3.Background = Brushes.Red;
            Dugme3.Content = "Prekini";

            Naziv1.Text = "Ime proizvoda";
            Naziv2.Text = "Unesena količina";

            Linija1.Background = Brushes.White;
            Linija2.Background = Brushes.White;

            TrenRac.Text = "Trenutni unos:";
            BrojKcal.Text = "0";
            kcal.Text = "kcal";


        }

       /* private void Tabela_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }*/

        private void Dugme1_Click(object sender, RoutedEventArgs e)
        {
            if (Dugme1.Content == "Dodaj")
            {
                if (hrana == null)
                {
                    MessageBox.Show("The dictionary is not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!string.IsNullOrEmpty(Linija1.Text) && int.TryParse(Linija2.Text, out int value))
                {
                    hrana[Linija1.Text] = value;
                    UpdateKeyValuePairs();
                }
                Linija1.Clear();
                Linija2.Clear();
            }

            if (Dugme1.Content == "Saberi")
            {
                double total = Convert.ToInt32(BrojKcal.Text);
                double masa = Convert.ToDouble(Linija2.Text);
                //double masa = (double)m;
                string jelo = Linija1.Text;
                if (masa > 0)
                {
                    if (!hrana.ContainsKey(jelo))
                    {
                        MessageBox.Show($"Proizvod '{Linija1.Text}' ne postoji na listi", "Nepostojeći proizvod", MessageBoxButton.OK, MessageBoxImage.Warning);
                        Linija1.Clear();
                        Linija2.Clear();
                    }
                    else
                    {
                        int kal = hrana[jelo];
                        total += (masa / 100) * kal;
                        int t = (int)total;
                        BrojKcal.Text = Convert.ToString(t);

                        Linija1.Clear();
                        Linija2.Clear();
                    }
                }
                else
                {
                    Linija1.Clear();
                    Linija2.Clear();
                }
                    
                
            }
        }

        private void Dugme2_Click(object sender, RoutedEventArgs e)
        {
            if (Dugme2.Content == "Ukloni")
            {
                if (hrana.ContainsKey(Linija1.Text))
                {
                    hrana.Remove(Linija1.Text);
                    UpdateKeyValuePairs();
                }
                Linija1.Clear();
                Linija2.Clear();
            }
            if (Dugme2.Content == "Resetuj")
            {
                BrojKcal.Text = "0";

                Linija1.Clear();
                Linija2.Clear();
            }
        }

        private void UpdateKeyValuePairs()
        {
            KeyValuePairs.Clear();
            foreach (var kvp in hrana)
            {
                KeyValuePairs.Add(kvp);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
        
}
