using Blanker.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;
using Xamarin.Forms;


namespace Blanker
{
    public partial class MainPage : ContentPage
    {
        public static Blank Blank { get; set; }

        public List<Entry> Entries = new List<Entry>();

        public List<SearchBar> SearchBars = new List<SearchBar>();

        public List<Country> Countries { get; set; }

        public City City { get; set; }

        Adapter Adapter;

        public MainPage()
        {
            InitializeComponent();

            FillEntries();

            FillCountries();
        }

        private async void FillCountries()
        {

            try
            {
                Adapter = new Adapter();

                Countries = await Adapter.GetCountries();

                foreach (var item in Countries)
                {
                    pickerCountry.Items.Add(item.title);
                }
            }
            catch
            {
                await DisplayAlert("Attention", "You don't have the Internet connection", "OK");
            }
        }

        private async void SelectCity(object sender, EventArgs e)
        {

            string country = pickerCountry.Items[pickerCountry.SelectedIndex];
            int countryId = 0;

            foreach (var item in Countries)
            {
                if (item.title == country)
                {
                    countryId = item.cid;
                }
            }

            await Navigation.PushAsync(new SearchCityPage(countryId));

        }

        protected internal void AddCity(City city)
        {
            this.City = city;
            cityLabel.Text = City.title;
        }

        private void FillEntries()
        {
            Entries.Add(nameEntry);
            Entries.Add(surnameEntry);
        }

        async void OnFill(object sender, EventArgs e)
        {
            CreateBlank();
            await Navigation.PushAsync(new BlankViewPage());
        }

        private void CreateBlank()
        {
            Blank = new Blank();
            Blank.Name = nameEntry.Text;
            Blank.Surname = surnameEntry.Text;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Entries.Count; i++)
            {
                if (i == Entries.Count - 1)
                {
                    break;
                }

                if (!string.IsNullOrWhiteSpace(Entries[i].Text))
                {
                    Entries[i + 1].IsEnabled = true;
                }
            }
        }
    }
}
