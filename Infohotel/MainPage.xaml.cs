

namespace Infohotel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnPrevClicked(object sender, EventArgs e)
        {
            try
            {
               
                if (carousel.Position > 0)
                    carousel.Position--;
                else
                    carousel.Position = GetItemsCount() - 1;
            }
            catch { }
        }

        void OnNextClicked(object sender, EventArgs e)
        {
            try
            {
                int count = GetItemsCount();
                if (carousel.Position < count - 1)
                    carousel.Position++;
                else
                    carousel.Position = 0;
            }
            catch { }
        }

        int GetItemsCount()
        {
  
            if (carousel.ItemsSource is System.Collections.IEnumerable en)
            {
                int c = 0;
                foreach (var _ in en) c++;
                return c;
            }
            return 0;
        }
    }

}
