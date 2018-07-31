using System.Windows;
using System.Windows.Controls;

namespace QuanLyCaPhe.View
{
    /// <summary>
    /// Interaction logic for WebcamView.xaml
    /// </summary>
    public partial class WebcamView : Window
    {
        private static Image _videoPlayerView;
        public static Image VideoPlayerView { get => _videoPlayerView; set => _videoPlayerView = value; }

        private static ProgressBar _progressBarView;
        public static ProgressBar ProgressBarView { get => _progressBarView; set => _progressBarView = value; }

        public WebcamView()
        {
            InitializeComponent();
            VideoPlayerView = videoPlayer;
            ProgressBarView = ProgressBar;
        }
    }
}