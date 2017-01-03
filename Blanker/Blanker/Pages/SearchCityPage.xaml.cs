using Blanker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Blanker
{
    public partial class SearchCityPage : ContentPage
    {
        int CountryId;

        public List<City> Cities { get; set; }

        public City SelectedCity { get; set; }

        public SearchCityPage(int countryId)
        {
            CountryId = countryId;
            InitializeComponent();
            FillCities();
        }

        private async void FillCities()
        {
            Adapter adapter = new Adapter();
            try
            {
                Cities = await adapter.GetCities(CountryId, searchBarCity.Text);
                listViewCities.ItemsSource = Cities;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            
        }

        private void OnSearch(object sender, EventArgs e)
        {
            FillCities();
        }

        private void OnSelect(object sender, EventArgs e)
        {
            SelectedCity = (City)listViewCities.SelectedItem;

            if (SelectedCity != null)
            {
                addButton.IsEnabled = true;
            }
        }

        private async void OnClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)Application.Current.MainPage;
            IReadOnlyList<Page> navStack = navPage.Navigation.NavigationStack;
            MainPage homePage = navStack[navPage.Navigation.NavigationStack.Count - 1] as MainPage;

            if (homePage != null)
            {
                homePage.AddCity(SelectedCity);
            }
        }

    }
}
