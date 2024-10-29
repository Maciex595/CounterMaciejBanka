namespace Counter
{
    public partial class MainPage
    {
        public List<Counter> Counters { get; } = new List<Counter>();

        public MainPage()
        {
            InitializeComponent();
            CounterListView.ItemsSource = Counters;
        }

        private void OnAddCounterClicked(object sender, EventArgs e)
        {
            string name = CounterNameEntry.Text;
            if (!string.IsNullOrEmpty(name))
            {
                Counters.Add(new Counter(name));
                CounterNameEntry.Text = "";
                CounterListView.ItemsSource = null;
                CounterListView.ItemsSource = Counters;
            }
        }

        private void OnIncreaseClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string name)
            {
                var counter = Counters.FirstOrDefault(c => c.Name == name);
                if (counter != null)
                {
                    counter.Count++;
                    CounterListView.ItemsSource = null;
                    CounterListView.ItemsSource = Counters;
                }
            }
        }

        private void OnDecreaseClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string name)
            {
                var counter = Counters.FirstOrDefault(c => c.Name == name);
                if (counter != null)
                {
                    counter.Count--;
                    CounterListView.ItemsSource = null;
                    CounterListView.ItemsSource = Counters;
                }
            }
        }
    }

    public class Counter
    {
        public string Name { get; }
        public int Count { get; set; }
        public string DisplayText => $"Licznik '{Name}': {Count}";

        public Counter(string name)
        {
            Name = name;
            Count = 0;
        }
    }
}
