using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class AppPage : TabbedPage
    {
        public AppPage()
        {
            Page journalsPage, aboutPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    journalsPage = new NavigationPage(new JournalsPage())
                    {
                        Title = "Journals"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    journalsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    journalsPage = new JournalsPage()
                    {
                        Title = "Journals"
                    };

                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };
                    break;
            }

            Children.Add(journalsPage);
            Children.Add(aboutPage);

            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
