# `ActivableSwipeView`

Repository contains the `ActivableSwipeView` package which provides a workaround for an issue which appears on touchscreen devices. When the `SwipeView` contains a view with buttons then clicking the buttons is very hard since the swipe is triggered first.

Package provides the `CustomSwipeView` class (derived from the `SwipeView` class)  with the added `ActivationThreshold` property. The activation threshold is a number of device-independent units, which represents a minimum distance, between a dragging initial point and a dragging current point, to activate the swipe.

The `CustomSwipeView` class and the `ActivationThreshold` property are available for Android, Windows, iOS and MacCatalyst. However, the property takes effect only on Android. On the other platforms it is ignored. 

## API Usage
### Setup
First you need to install the [Jns.Util.ActivableSwipeView](https://www.nuget.org/packages/Jns.Util.ActivableSwipeView) NuGet package. 
When the package installation completes, go into your MauiProgram.cs and add the following initialization line:

``` csharp
var builder = MauiApp.CreateBuilder();
builder
    .UseMauiApp<App>()
    // Initialize the ActivableSwipeView util, by adding the below line of code
    .UseActivableSwipeView()
    // After initializing the ActivableSwipeView util, optionally add other things
    .ConfigureFonts(fonts =>
    {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    });
```

The extension method `UseActivableSwipeView` comes from the class `ActivableSwipeView.Helper.ActivableSwipeViewConfigurator`.

### XAML File

When you want to use the `CustomSwipeView` class in an XAML file then you need to add the namespace:
``` xaml
xmlns:activable="clr-namespace:ActivableSwipeView.Views;assembly=ActivableSwipeView"
```

After this step you can start using the `CustomSwipeView` class:
``` xaml
<activable:CustomSwipeView ActivationThreshold="15">
    <activable:CustomSwipeView.LeftItems>
        <SwipeItems>
            <SwipeItem Text="Previous"/>
        </SwipeItems>
    </activable:CustomSwipeView.LeftItems>
    <activable:CustomSwipeView.RightItems>
        <SwipeItems>
            <SwipeItem Text="Next"/>
        </SwipeItems>
    </activable:CustomSwipeView.RightItems>
    <CollectionView ItemsSource="{Binding Items}">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <ContentView>
                    <VerticalStackLayout Margin="5,10">
                        <Button Text="Button" Command="{Binding ClickCommand}" BackgroundColor="DarkGray"/>
                        <Label Text="{Binding ClickCountText}" HorizontalOptions="Fill" HorizontalTextAlignment="Center" TextColor="Gray"/>
                    </VerticalStackLayout>
                </ContentView>
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>
</activable:CustomSwipeView>
```

### C# Code
To leverage the `CustomSwipeView` in a C# code file you need to add the using statement at the top:
``` csharp
using SwipeView = ActivableSwipeView.Views.CustomSwipeView;
```

Now you can instantiate the `SwipeView`:
``` csharp
swipeView = new SwipeView
{
    Content = scrollView,
    Threshold = 126,
    LeftItems = new SwipeItems(new List<SwipeItem>() { goToPreviousItem })
    {
        Mode = SwipeMode.Execute
    },
    RightItems = new SwipeItems(new List<SwipeItem>() { goToNextItem })
    {
        Mode = SwipeMode.Execute
    },
    ActivationThreshold = 15
};
```
