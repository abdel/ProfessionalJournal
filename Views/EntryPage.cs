using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class EntryPage : TabbedPage
    {
        public EntryPage(Entry entry)
        {
            Page entryDetailPage, entryHistoryPage = null;

            entryDetailPage = new EntryDetailPage(new EntryDetailViewModel(entry))
            {
                Title = "Entry Details"
            };

            entryHistoryPage = new EntryHistoryPage(new EntryHistoryViewModel(entry.Id))
            {
                Title = "History"
            };

            if (Device.RuntimePlatform == Device.iOS) {
                entryHistoryPage.Icon = "tab_feed.png";
                entryDetailPage.Icon = "tab_about.png";
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
