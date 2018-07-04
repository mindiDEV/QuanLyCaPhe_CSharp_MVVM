using AForge.Video;
using AForge.Video.DirectShow;
using QuanLyCaPhe.ClassSupport;
using QuanLyCaPhe.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ZXing;

namespace QuanLyCaPhe.ViewModel
{
    public class ScanQRCodeDiscountViewModel:BaseViewModel
    {
        #region Properties

        private Window getObject;

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

        private DispatcherTimer dispatcherTime;

        public bool IsCheckQrCode;

        #endregion Properties

        #region ICommand Properties

        private ICommand _startCamera;

        private ICommand _stopCamera;

        private ICommand _checkQRCodeDiscount;

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

        public ICommand CheckQRCodeDiscount
        {
            get
            {
                return _checkQRCodeDiscount;
            }
            set
            {
                _checkQRCodeDiscount = value;
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

        public ScanQRCodeDiscountViewModel()
        {
            IsCheckQrCode = false;

            GetVideoDevices();

            dispatcherTime = new DispatcherTimer();

            dispatcherTime.Tick += new EventHandler(DispatcherTimer_Tick);

            StartCamera = new RelayCommand<ScanQRCodeDiscountView>((p) =>
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

            CheckQRCodeDiscount = new RelayCommand<object>((p) =>
            {

                return true;
            },
            (p) =>
            {
                dispatcherTime.IsEnabled = true;

                dispatcherTime.Start();
            });

            CloseWebcamCommand = new RelayCommand<Window>((p) =>
            {
                getObject = p;

                return true;
            },
            (p) =>
            {
                CloseWebcamView(p);
            });
        }

        #endregion Constructor

        #region Methods

        //Khi đóng sẽ xử lý giảm giá sản phẩm...
        private void CloseWebcamView(Window p)
        {
            if (p == null)
            {
                return;
            }
            p.DialogResult = true;

            p.Close();

        }


        private bool CheckDiscountByQRCode(Bitmap bitmap)
        {
            BarcodeReader _reader = new BarcodeReader();

            Result res = _reader.Decode(bitmap);
            try
            {
                string decoded = res.ToString().Trim();

                if (decoded != "")
                {
                    IsCheckQrCode = true;

                    dispatcherTime.Stop();

                    CloseWebcamView(getObject);
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

        private void GetObject_Closing(object sender, CancelEventArgs e)
        {
            MessageBox.Show("AAAA");
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            RenderTargetBitmap rtBmp = new RenderTargetBitmap((int)ScanQRCodeDiscountView.VideoPlayerView.ActualWidth, (int)ScanQRCodeDiscountView.VideoPlayerView.ActualHeight,
            96.0, 96.0, PixelFormats.Pbgra32);

            ScanQRCodeDiscountView.VideoPlayerView.Measure(new System.Windows.Size((int)ScanQRCodeDiscountView.VideoPlayerView.ActualWidth, (int)ScanQRCodeDiscountView.VideoPlayerView.ActualHeight));
            ScanQRCodeDiscountView.VideoPlayerView.Arrange(new Rect(new System.Windows.Size((int)ScanQRCodeDiscountView.VideoPlayerView.ActualWidth, (int)ScanQRCodeDiscountView.VideoPlayerView.ActualHeight)));

            rtBmp.Render(ScanQRCodeDiscountView.VideoPlayerView);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            MemoryStream stream = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(rtBmp));

            // Save to memory stream and create Bitamp from stream
            encoder.Save(stream);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(stream);

            CheckDiscountByQRCode(bitmap);
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
                System.Windows.Application.Current.Dispatcher.BeginInvoke(
                                      (Action)(() => { ScanQRCodeDiscountView.VideoPlayerView.Source = bi; }));
            }
            catch
            {
                
                StopWebcam();

               
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
