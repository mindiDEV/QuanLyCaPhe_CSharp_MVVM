﻿using GalaSoft.MvvmLight.Messaging;
using QuanLyCaPhe.Message;
using QuanLyCaPhe.ViewModel;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace QuanLyCaPhe.View
{
    /// <summary>
    /// Interaction logic for PromotionView.xaml
    /// </summary>
    public partial class PromotionView : UserControl
    {
        public PromotionView()
        {
            InitializeComponent();
            Messenger.Default.Register<UserMessage>(this, (action) => ReceiveUserMessage(action));
            this.DataContext = new PromotionViewModel();
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