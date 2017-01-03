using Blanker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Blanker
{
    public partial class SearchUniversityPage : ContentPage
    {
        public List<University> Universities { get; set; }

        public University SelectedUniversity { get; set; }

        private int CountryId, CityId;

        public SearchUniversityPage(int countryId, int cityId)
        {
            CountryId = countryId;
            CityId = cityId;
            InitializeComponent();
            FillUniversities();
        }

        private async void FillUniversities()
        {
            Adapter adapter = new Adapter();
            try
            {
                Universities = await adapter.GetUniversities(CountryId, CityId, searchBarUniversity.Text);
                listViewUniversities.ItemsSource = Universities;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            
        }

        private void OnSearch(object sender, EventArgs e)
        {
            FillUniversities();
        }

        private void OnSelect(object sender, EventArgs e)
        {
            SelectedUniversity = (University)listViewUniversities.SelectedItem;

            if (SelectedUniversity != null)
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
                homePage.AddUniversity(SelectedUniversity);
            }
        }
    }
}
