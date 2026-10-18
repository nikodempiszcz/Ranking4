using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ranking
{ 
    public partial class MainWindow : Window
    {
        private readonly string sciezka = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ranking", "dane.json");
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(sciezka))
            {
                var dane = JsonSerializer.Deserialize<DaneFormularza1>(File.ReadAllText(sciezka));
                
                if (dane != null)
                {
                    NameTextBox.Text = dane.Imie;
                    SurnameTextBox.Text = dane.Nazwisko;
                }
                
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(SurnameTextBox.Text))
                {
                    MessageBox.Show("Nie wypełniono wszystkich pól. Wypełnij wszystkie pola i spróbuj ponownie.");
                return;
                }
            var dane = new DaneFormularza1
            {
                Imie = NameTextBox.Text,
                Nazwisko = SurnameTextBox.Text
            };
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(sciezka)!);
            File.WriteAllText(sciezka, JsonSerializer.Serialize(dane));
            MessageBox.Show("Dane Ok");
            OknoPoFormularzu oknoPoFormularzu = new OknoPoFormularzu();
            oknoPoFormularzu.Show();
            MainWindow MainWindow = this;
            MainWindow.Close();
        }

    }
}