using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class SearchPopupPage : PopupPage
    {
        public SearchPopupPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnDateSearch(object sender, EventArgs e)
        {
            string[] dates = {
                searchStartDate.Date.ToShortDateString(),
                searchEndDate.Date.ToShortDateString()
            };

            MessagingCenter.Send(this, "DateSearch", dates);

            CloseAllPopup();
        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
    }
}
