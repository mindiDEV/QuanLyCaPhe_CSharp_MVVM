using AForge.Video;
using AForge.Video.DirectShow;
using QuanLyCaPhe.ClassSupport;
using QuanLyCaPhe.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ZXing;

namespace QuanLyCaPhe.ViewModel
{
    public class WebcamViewModel : BaseViewModel
    {
        #region Properties

        private IVideoSource _videoSource;

        private ObservableCollection<FilterInfo> _videoDevices;
        public ObservableCollection<FilterInfo> VideoDevices { get => _videoDevices; set => _videoDevices = value; }

        private FilterInfo _currentDevice;

        public FilterInfo CurrentDevice
        {
            get
            {
                return _currentDevice;
            }
            set
            {
                _currentDevice = value;
                RaisePropertyChanged("CurrentDevice");
            }
        }

        public static string getDecoded= "";

        public static Window tmp;

        private DispatcherTimer dispatcherTime;

        public bool IsLoginByQrCode;

        #endregion Properties

        #region ICommand Properties

        private ICommand _startCamera;

        private ICommand _stopCamera;

        private ICommand _checkLogin;

        private ICommand _closeWebcamCommand;

        public ICommand StartCamera
        {
            get
            {
                return _startCamera;
            }
            set
            {
                _startCamera = value;
            }
        }

        public ICommand StopCamera
        {
            get
            {
                return _stopCamera;
            }
            set
            {
                _stopCamera = value;
            }
        }

        public ICommand CheckLogin
        {
            get
            {
                return _checkLogin;
            }
            set
            {
                _checkLogin = value;
            }
        }

        public ICommand CloseWebcamCommand
        {
            get
            {
                return _closeWebcamCommand;
            }
            set
            {
                _closeWebcamCommand = value;
            }
        }

        

        #endregion ICommand Properties

        #region Constructor

        public WebcamViewModel()
        {
            IsLoginByQrCode = false;

            GetVideoDevices();

            dispatcherTime = new DispatcherTimer();

            dispatcherTime.Tick += new EventHandler(DispatcherTimer_Tick);

            StartCamera = new RelayCommand<WebcamView>((p) =>
            {
                return true;
            },
            (p) =>
            {
                StartWebcam();
                p.Closing += P_Closing;
            });
            StopCamera = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                StopWebcam();
            });

            CheckLogin = new RelayCommand<Window>((p) =>
            {
                tmp = p;
                return true;
            },
            (p) =>
            {
                dispatcherTime.IsEnabled = true;

                dispatcherTime.Start();
            });

            CloseWebcamCommand = new RelayCommand<Window>((p) =>
            {
                
                return true;
            },
            (p) =>
            {
                CloseWebcamView(p);
            });
        }

        #endregion Constructor

        #region Methods

        private void CloseWebcamView(Window p)
        {
            if (p == null)
            {
                return;
            }
            p.DialogResult = true;
            p.Close();
        }



        
        private bool CheckLoginByQRCode(Bitmap bitmap)
        {
            BarcodeReader _reader = new BarcodeReader();
            Result res = _reader.Decode(bitmap);
            try
            {
                string decoded = res.ToString().Trim();

                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork; ;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync();

                if (decoded != "")
                {
                    getDecoded = decoded;
                    
                   // IsLoginByQrCode = true;

                    dispatcherTime.Stop();
                   
                    

                }
                else
                {
                    return false;
                }
            }
            catch
            {
            }

            return true;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            RenderTargetBitmap rtBmp = new RenderTargetBitmap((int)WebcamView.VideoPlayerView.ActualWidth, (int)WebcamView.VideoPlayerView.ActualHeight,
            96.0, 96.0, PixelFormats.Pbgra32);

            WebcamView.VideoPlayerView.Measure(new System.Windows.Size((int)WebcamView.VideoPlayerView.ActualWidth, (int)WebcamView.VideoPlayerView.ActualHeight));
            WebcamView.VideoPlayerView.Arrange(new Rect(new System.Windows.Size((int)WebcamView.VideoPlayerView.ActualWidth, (int)WebcamView.VideoPlayerView.ActualHeight)));

            rtBmp.Render(WebcamView.VideoPlayerView);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            MemoryStream stream = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(rtBmp));

            // Save to memory stream and create Bitamp from stream
            encoder.Save(stream);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(stream);

            CheckLoginByQRCode(bitmap);
        }

        private void P_Closing(object sender, CancelEventArgs e)
        {
            StopWebcam();
        }

        private void StartWebcam()
        {
            if (CurrentDevice != null)
            {
                _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                _videoSource.NewFrame += _videoSource_NewFrame;
                _videoSource.Start();
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WebcamView.ProgressBarView.Value = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            StopWebcam();
            tmp.Close();
            WebcamView.ProgressBarView.Value = 0;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            int sum = 0;
            worker.ReportProgress(0, String.Format("Processing 1"));
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                worker.ReportProgress((sum += 5));
            }
        }

        private void _videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = bitmap.ToBitmapImage();
                }
                bi.Freeze();
                System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() => { WebcamView.VideoPlayerView.Source = bi; }));
            }
            catch
            {
                StopWebcam();
                
                tmp.Close();
                return;
            }
        }

        private void StopWebcam()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= new NewFrameEventHandler(_videoSource_NewFrame);
            }
        }

        private void GetVideoDevices()
        {
            VideoDevices = new ObservableCollection<FilterInfo>();
            foreach (FilterInfo filterInfo in new FilterInfoCollection(FilterCategory.VideoInputDevice))
            {
                VideoDevices.Add(filterInfo);
            }
            if (VideoDevices.Any())
            {
                CurrentDevice = VideoDevices[0];
            }
            else
            {
                MessageBox.Show("No video sources found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion Methods
    }
}