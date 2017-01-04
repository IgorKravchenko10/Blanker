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

        public List<Country> Countries { get; set; }

        Adapter Adapter = new Adapter();

        public MainPage()
        {
            InitializeComponent();

            LoadCountries();
        }

        private async void LoadCountries()
        {
            try
            {
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

        private void Refresh(bool result)
        {
            if (result)
            {
                LoadCountries();
            }
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
            cityLabel.TextColor = Color.Black;
            cityLabel.Text = this.Blank.City.title;
            CheckFilling();
        }

        protected internal void AddUniversity(University university)
        {
            this.Blank.University = university;
            universityLabel.TextColor = Color.Black;
            universityLabel.Text = this.Blank.University.title;
            CheckFilling();
        }

        private void FillBlank()
        {
            Blank.Name = nameEntry.Text;
            Blank.Surname = surnameEntry.Text;

        }

        async void ShowBlank(object sender, EventArgs e)
        {
            FillBlank();
            await Navigation.PushAsync(new BlankViewPage(Blank));
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            CheckFilling();
        }

        private void CheckFilling()
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                surnameEntry.IsEnabled = true;
            }
            if (!string.IsNullOrWhiteSpace(surnameEntry.Text))
            {
                pickerCountry.IsEnabled = true;
            }
            if (pickerCountry.SelectedIndex >= 0)
            {
                cityButton.IsEnabled = true;
            }
            if (!string.IsNullOrWhiteSpace(cityLabel.Text))
            {
                universityButton.IsEnabled = true;
            }
            if (!string.IsNullOrWhiteSpace(universityLabel.Text))
            {
                fillButton.IsEnabled = true;
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
    }
}
