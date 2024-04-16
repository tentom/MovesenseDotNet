
namespace MovesenseTestingAndroidMaui;


public partial class MainPage : ContentPage
{
    
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";
        await Plugin.Movesense.CrossMovesense.Current.ConnectMdsAsync(new Guid());
        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}

public class Test //: Com.Movesense.Mds.IMdsNotificationListener
{
    
}


