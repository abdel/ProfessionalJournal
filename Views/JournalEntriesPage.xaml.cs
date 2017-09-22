using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class JournalEntriesPage : ContentPage
    {
        JournalEntriesViewModel viewModel;

        public JournalEntriesPage()
        {
            InitializeComponent();

            var journal = new Journal
            {
                Title = "Journal 1",
                Description = "This is a journal description."
            };

            viewModel = new JournalEntriesViewModel(journal);
            BindingContext = viewModel;
        }

        public JournalEntriesPage(JournalEntriesViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

		async void OnAddEntryButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewEntryPage());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Entries.Count == 0)
				viewModel.LoadEntriesCommand.Execute(null);
		}

	}
}
