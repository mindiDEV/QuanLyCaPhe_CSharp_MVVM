using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyCaPhe.View
{
    /// <summary>
    /// Interaction logic for ScanQRCodeDiscountView.xaml
    /// </summary>
    public partial class ScanQRCodeDiscountView : Window
    {
        private static Image _videoPlayerView;
        public static Image VideoPlayerView { get => _videoPlayerView; set => _videoPlayerView = value; }
        public ScanQRCodeDiscountView()
        {
            InitializeComponent();
            VideoPlayerView = videoPlayer;
        }
    }
}
