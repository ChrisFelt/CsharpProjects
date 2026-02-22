namespace Chatter
{
    // define logic for event handlers/actions triggered by controls on MainPage
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            // accessibility support - specifies text read by screen reader when user selects button
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
