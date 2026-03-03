using Chatter.ViewModels;

namespace Chatter
{
    // define logic for event handlers/actions triggered by controls on MainPage
    public partial class MainPage : ContentPage
    {
        private readonly ChatViewModel ViewModel;

        public MainPage(ChatViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = ViewModel = viewModel;

            MessagesDisplay.Scrolled += MessagesDisplay_Scrolled;
        }

        private void MessagesDisplay_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            ButtonScrollToBottom.IsVisible = e.LastVisibleItemIndex != ViewModel.MessageCollection.Count - 1;
        }

        private void ButtonScrollToBottom_Clicked(object sender, EventArgs e)
        {
            MessagesDisplay.ScrollTo(ViewModel.MessageCollection.Count - 1);
        }
    }
}
