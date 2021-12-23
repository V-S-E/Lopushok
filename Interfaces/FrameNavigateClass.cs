using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Interfaces
{
    public class FrameNavigateClass
    {
        private static Frame mainFrame;
        private static event closed_del closed_page;
        public delegate void closed_del();

        public static void Go(System.Windows.Controls.Page page, closed_del closed=null)
        {
            mainFrame.Navigate(page);
            if (closed != null)
            {
                closed_page += closed;
            }
        }

        public static void Back()
        {
            if (mainFrame.CanGoBack)
            {
                mainFrame.Refresh();
                mainFrame.GoBack();mainFrame.RemoveBackEntry();
                closed_page();
                closed_page-=closed_page;
            }
        }

        public static void SetFrame(Frame f)
        {
            mainFrame = f;
        }
    }
}
