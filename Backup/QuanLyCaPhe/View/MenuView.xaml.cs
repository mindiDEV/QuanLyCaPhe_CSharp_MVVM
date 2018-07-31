using QuanLyCaPhe.Message;
using QuanLyCaPhe.ViewModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media.Animation;

namespace QuanLyCaPhe.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
            Messenger.Default.Register<UserMessage>(this, (action) => ReceiveUserMessage(action));
            this.DataContext = new MenuViewModel();
        }
        private void ReceiveUserMessage(UserMessage msg)
        {
            UIMessage.Opacity = 1;
            UIMessage.Text = msg.Message;
            Storyboard sb = (Storyboard)this.FindResource("FadeUIMessage");
            sb.Begin();
        }
    }
}