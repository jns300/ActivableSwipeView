using SwipeViewActivationReproDemo.ViewModel;

namespace SwipeViewActivationReproDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ItemList();
        }
    }

}
