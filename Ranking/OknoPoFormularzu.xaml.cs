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
using System.Collections.ObjectModel;

namespace Ranking
{
    public partial class OknoPoFormularzu : Window
    {
        public ICommand DodajCommand { get; }
        public ICommand EdytujCommand { get; }
        public ICommand UsunCommand { get; }
        public ICommand ZamknijCommand { get; }
        private object edytowanyElement = null;
        private ListBox edytowanaLista = null;
        private string edytowanyTier = null;
        public ObservableCollection<string> TierSItems { get; set; }
        public ObservableCollection<string> TierAItems { get; set; }
        public ObservableCollection<string> TierBItems { get; set; }
        public ObservableCollection<string> TierCItems { get; set; }
        public ObservableCollection<string> TierDItems { get; set; }
        public ObservableCollection<string> TierFItems { get; set; }
        public OknoPoFormularzu()
        {
            InitializeComponent();
            TierSItems = new ObservableCollection<string>();
            TierAItems = new ObservableCollection<string>();
            TierBItems = new ObservableCollection<string>();
            TierCItems = new ObservableCollection<string>();
            TierDItems = new ObservableCollection<string>();
            TierFItems = new ObservableCollection<string>();
            DodajCommand = new RelayCommand(DodajSlowo);
            EdytujCommand = new RelayCommand(EdytujSlowo);
            UsunCommand = new RelayCommand(UsunSlowo, CanUsunSlowo);
            ZamknijCommand = new RelayCommand(ZamknijOkno);
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            OknoPoFormularzu oknoPoFormularzu = this;
            oknoPoFormularzu.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NiePrzydatneOkno NiePrzydatneOkno = new NiePrzydatneOkno();
            NiePrzydatneOkno.Show();
            OknoPoFormularzu oknoPoFormularzu = this;
            oknoPoFormularzu.Close();
        }

        private void ZamknijOkno(object parameter)
        {
            OknoPoFormularzu oknoPoFormularzu = this;
            oknoPoFormularzu.Close();
        }

        private void DodajSlowo(object parameter)
        {
            string slowo = Slowa.Text;
            if (!string.IsNullOrWhiteSpace(slowo))
            {
                ComboBoxItem selectedTier = TierComboBox.SelectedItem as ComboBoxItem;
                if (selectedTier != null)
                {
                    string nowyTier = selectedTier.Content.ToString();
                    if (edytowanyElement != null && edytowanaLista != null)
                    {
                        if (edytowanaLista == TierS) TierS.Items.Remove(edytowanyElement);
                        else if (edytowanaLista == TierA) TierA.Items.Remove(edytowanyElement);
                        else if (edytowanaLista == TierB) TierB.Items.Remove(edytowanyElement);
                        else if (edytowanaLista == TierC) TierC.Items.Remove(edytowanyElement);
                        else if (edytowanaLista == TierD) TierD.Items.Remove(edytowanyElement);
                        else if (edytowanaLista == TierF) TierF.Items.Remove(edytowanyElement);
                        edytowanyElement = null;
                        edytowanaLista = null;
                    }
                    switch (nowyTier)
                    {
                        case "S":
                            TierSItems.Add(slowo);
                            break;
                        case "A":
                            TierAItems.Add(slowo);
                            break;
                        case "B":
                            TierBItems.Add(slowo);
                            break;
                        case "C":
                            TierCItems.Add(slowo);
                            break;
                        case "D":
                            TierDItems.Add(slowo);
                            break;
                        case "F":
                            TierFItems.Add(slowo);
                            break;
                    }
                    Slowa.Clear();
                }
            }        
        }

        private void UsunSlowo(object parameter)
        {
            if (TierS.SelectedItem != null)
            {
                string element = TierS.SelectedItem.ToString();
                if (MessageBox.Show($"Usunac {element}?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    TierSItems.Remove(element);
            }
            else if (TierA.SelectedItem != null)
            { 
                string element = TierA.SelectedItem.ToString();
                if (MessageBox.Show($"Usunac {element}?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    TierAItems.Remove(element);
            }
            else if (TierB.SelectedItem != null)
            {
                string element = TierB.SelectedItem.ToString();
                if (MessageBox.Show($"Usunac {element}?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    TierBItems.Remove(element);
            }
            else if (TierC.SelectedItem != null)
            {
                string element = TierC.SelectedItem.ToString();
                if (MessageBox.Show($"Usunac {element}?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    TierCItems.Remove(element);
            }
            else if (TierD.SelectedItem != null)
            {
                string element = TierD.SelectedItem.ToString();
                if (MessageBox.Show($"Usunac {element}?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    TierDItems.Remove(element);
            }
            else if (TierF.SelectedItem != null)
            {
                string element = TierF.SelectedItem.ToString();
                if (MessageBox.Show($"Usunac {element}?", "Pytanie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    TierFItems.Remove(element);
            }
            else
            {
                MessageBox.Show("Najpier zaznacz element");
            }

        }
        private bool CanUsunSlowo(object parameter)
        {
            if (TierS.SelectedItem != null) return true;
            if (TierA.SelectedItem != null) return true;
            if (TierB.SelectedItem != null) return true;
            if (TierC.SelectedItem != null) return true;
            if (TierD.SelectedItem != null) return true;
            if (TierF.SelectedItem != null) return true;
            return false;
        }

        private void EdytujSlowo(object parameter)
        {
            string zaznaczonytekst = null;
            string wybranypoziom = null;
            if (TierS.SelectedItem != null)
            {
                zaznaczonytekst = TierS.SelectedItem.ToString();
                wybranypoziom = "S";
            }
            else if (TierA.SelectedItem != null)
            {
                zaznaczonytekst = TierA.SelectedItem.ToString();
                wybranypoziom = "A";
            }
            else if (TierB.SelectedItem != null)
            {
                zaznaczonytekst = TierB.SelectedItem.ToString();
                wybranypoziom = "B";
            }
            else if (TierC.SelectedItem != null)
            {
                zaznaczonytekst = TierC.SelectedItem.ToString();
                wybranypoziom = "C";
            }
            else if (TierD.SelectedItem != null)
            {
                zaznaczonytekst = TierD.SelectedItem.ToString();
                wybranypoziom = "D";
            }
            else if (TierF.SelectedItem != null)
            {
                zaznaczonytekst = TierF.SelectedItem.ToString();
                wybranypoziom = "F";
            }
            if (zaznaczonytekst != null && wybranypoziom != null)
            {
                string noweSlowo = Microsoft.VisualBasic.Interaction.InputBox("Edytuj słowo:", "Edytuj", zaznaczonytekst);
                if (!string.IsNullOrWhiteSpace(noweSlowo))
                {
                    switch (wybranypoziom)
                    {
                        case "S":
                            TierSItems[TierS.SelectedIndex] = noweSlowo;
                            break;
                        case "A":
                            TierAItems[TierA.SelectedIndex] = noweSlowo;
                            break;
                        case "B":
                            TierBItems[TierB.SelectedIndex] = noweSlowo;
                            break;
                        case "C":
                            TierCItems[TierC.SelectedIndex] = noweSlowo;
                            break;
                        case "D":
                            TierDItems[TierD.SelectedIndex] = noweSlowo;
                            break;
                        case "F":
                            TierFItems[TierF.SelectedIndex] = noweSlowo;
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano słowa do edycji.");
            }
        }
    }
}
