using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Counter> Counters { get; } = new ObservableCollection<Counter>();

        public MainPage()
        {
            InitializeComponent();
            CounterListView.ItemsSource = Counters;
        }

        private void OnAddCounterClicked(object sender, EventArgs e)
        {
            string counterName = CounterNameEntry.Text;
            if (!string.IsNullOrEmpty(counterName) && !Counters.Any(c => c.Name == counterName))
            {
                Counters.Add(new Counter(counterName));
                CounterNameEntry.Text = string.Empty;
            }
        }

        private void OnIncreaseClicked(object sender, EventArgs e)
        {
            UpdateCounter(sender, 1);
        }

        private void OnDecreaseClicked(object sender, EventArgs e)
        {
            UpdateCounter(sender, -1);
        }

        private void UpdateCounter(object sender, int delta)
        {
            if (sender is Button button && button.CommandParameter is string counterName)
            {
                var counter = Counters.FirstOrDefault(c => c.Name == counterName);
                if (counter != null)
                {
                    counter.Count += delta;
                }
            }
        }
    }

    public class Counter : INotifyPropertyChanged
    {
        private int _count;

        public string Name { get; }

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public Counter(string name)
        {
            Name = name;
        }

        public string DisplayText => $"Counter '{Name}' clicked: {Count} times";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
