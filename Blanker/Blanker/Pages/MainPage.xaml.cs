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

        Adapter Adapter;

        public MainPage()
        {
            InitializeComponent();

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
            cityEntry.Text = this.Blank.City.title;
        }

        protected internal void AddUniversity(University university)
        {
            this.Blank.University = university;
            universityEntry.Text = this.Blank.University.title;
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

            //for (int i = 0; i < Entries.Count; i++)
            //{
            //    if (i == Entries.Count - 1)
            //    {
            //        break;
            //    }

            //    if (!string.IsNullOrWhiteSpace(Entries[i].Text))
            //    {
            //        Entries[i + 1].IsEnabled = true;
            //    }
            //}
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
            if (pickerCountry.SelectedIndex>=0)
            {
                cityButton.IsEnabled = true;
            }
            if (!string.IsNullOrWhiteSpace(cityEntry.Text))
            {
                universityButton.IsEnabled = true;
            }
            if (!string.IsNullOrWhiteSpace(universityEntry.Text))
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
