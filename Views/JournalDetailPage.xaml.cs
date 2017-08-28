using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class JournalDetailPage : ContentPage
    {
        JournalDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public JournalDetailPage()
        {
            InitializeComponent();

            var journal = new Journal
            {
                Name = "Journal 1",
                Description = "This is a journal description."
            };

            viewModel = new JournalDetailViewModel(journal);
            BindingContext = viewModel;
        }

        public JournalDetailPage(JournalDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
