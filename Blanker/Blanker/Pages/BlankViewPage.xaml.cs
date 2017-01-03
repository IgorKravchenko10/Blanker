using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Blanker
{
    public partial class BlankViewPage : ContentPage
    {
        Blank Blank;
        public BlankViewPage(Blank blank)
        {
            Blank = blank;
            InitializeComponent();
            ShowData();
        }

        private void ShowData()
        {
            labelName.Text = Blank.Name;
            labelSurname.Text = Blank.Surname;
            labelCountry.Text = Blank.Country.title;
            labelCity.Text = Blank.City.title;
            labelUniversity.Text = Blank.University.title;
        }
    }
}
