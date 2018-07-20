using System.Windows;
using System.Windows.Controls;

namespace QuanLyCaPhe.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public static ProgressBar ProgressBarLV { get; set; }

        public static PasswordBox _floattingPasswordBox { get; set; }

        public LoginView()
        {
            InitializeComponent();
            ProgressBarLV = ProgessBar;
            _floattingPasswordBox = FloatingPasswordBox;
        }
    }
}