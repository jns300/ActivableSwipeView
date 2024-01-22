using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivableSwipeViewSample.ViewModel
{
    internal class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int clickCount;

        public Item()
        {
            ClickCommand = new Command(Clicked);
            ClickCountText = "Click Counter";
        }

        private void Clicked()
        {
            ClickCountText = $"Click Count: {++clickCount}";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClickCountText)));
        }

        public Command ClickCommand { get; }

        public string ClickCountText { get; set; }

    }
}
