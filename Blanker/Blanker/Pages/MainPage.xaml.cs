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
        public Blank Blank = new Blank();

        public List<Entry> Entries = new List<Entry>();

        public List<SearchBar> SearchBars = new List<SearchBar>();

        public List<Country> Countries { get; set; }

        Adapter Adapter;

        public MainPage()
        {
            InitializeComponent();

            FillEntries();

            LoadCountries();
        }

        private async void LoadCountries()
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
            catch (Exception ex)
            {
                await DisplayAlert("Attention", Properties.Resources.InternetConnectionError, "OK");
            }

        }

        private async void SelectCity(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchCityPage(DetermineCountryId()));
        }

        private async void SelectUniversity(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchUniversityPage(Blank.Country.cid, Blank.City.cid));
        }

        private int DetermineCountryId()
        {
            string country = pickerCountry.Items[pickerCountry.SelectedIndex];
            int countryId = 0;

            foreach (var item in Countries)
            {
                if (item.title == country)
                {
                    countryId = item.cid;
                    Blank.Country = item;
                }
            }
            
            return countryId;
        }

        protected internal void AddCity(City city)
        {
            this.Blank.City = city;
            cityLabel.Text = this.Blank.City.title;
        }

        protected internal void AddUniversity(University university)
        {
            this.Blank.University = university;
            universityLabel.Text = this.Blank.University.title;
        }

        private void FillEntries()
        {
            Entries.Add(nameEntry);
            Entries.Add(surnameEntry);
        }

        async void ShowBlank(object sender, EventArgs e)
        {
            FillBlank();
            await Navigation.PushAsync(new BlankViewPage(Blank));
        }

        private void FillBlank()
        {
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
