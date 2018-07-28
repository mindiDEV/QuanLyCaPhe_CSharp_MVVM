using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class CheckClosingViewModel:BaseViewModel
    {
        public ICommand CloseWindowCommand { get; set; }
        public CheckClosingViewModel()
        {
            CloseWindowCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }
                p.DialogResult = true;
                p.Close();
            });
        }
    }
}
