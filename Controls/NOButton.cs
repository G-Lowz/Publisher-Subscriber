using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes; //rectangle
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace CreateBlocks.Controls
{
    public class NOButton
    {
        private readonly Button Button1 = new Button();
        private readonly Label Button1Label = new Label();
        private bool _status = false;

        public NOButton(Canvas _c, string _s)
        {
            Button1.Width = 30;
            Button1.Height = 30;
            _c.Children.Add(Button1);
            Canvas.SetLeft(Button1, 50);
            Canvas.SetTop(Button1, 0);
            Button1.Name = _s;
            Button1.Click += ButtonPressedIn;
            Button1.Content = _s;
        }
        private void ButtonPressedIn(object sender, RoutedEventArgs e)
        {
            _status = true;
            Button1.Background = Brushes.Green;
        }
        private void ButtonPressedOut(object sender, RoutedEventArgs e)
        {
            _status = false;
            Button1.Background = Brushes.Black;
        }
        public bool GetStatus()
        {
            return _status;
        }
    }
}
