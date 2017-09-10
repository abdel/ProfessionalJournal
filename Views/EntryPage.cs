using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class AppPage : TabbedPage
    {
        public AppPage()
        {
            Page entryDetailPage, entryHistoryPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    entryDetailPage = new NavigationPage(new EntryDetailPage())
                    {
                        Title = "Entry Details"
                    };

                    entryHistoryPage = new NavigationPage(new EntryHistoryPage())
                    {
                        Title = "History"
                    };
                    entryDetailPage.Icon = "tab_feed.png";
                    entryHistoryPage.Icon = "tab_about.png";
                    break;
                default:
                    entryDetailPage = new EntryDetailPage()
                    {
                        Title = "Journals"
                    };

                    entryHistoryPage = new EntryHistoryPage()
                    {
                        Title = "About"
                    };
                    break;
            }

            Children.Add(entryDetailPage);
            Children.Add(entryHistoryPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
