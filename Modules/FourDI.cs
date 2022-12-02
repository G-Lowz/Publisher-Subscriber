using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes; //rectangle
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CreateBlocks.Classes;

namespace CreateBlocks.Modules
{
    internal class FourDI
    {
        private readonly Rectangle Module = new Rectangle();
        private readonly List<Rectangle> LEDs = new List<Rectangle>();
        private readonly List<Label> Labels = new List<Label>();
        private readonly Label HexOut = new Label();
        private readonly bool[] Inputs = new bool[4] { false, false, false, false };
        private readonly List<Button> Controls = new List<Button>(4);
        private readonly String[] Publisher = new String[10];
        public event EventHandler ButtonIsClicked;
        public event EventHandler HexIsUpdated;
        private string[] Publish = new string[3];

        public void SetModuleLocation(int _top, int _left, Canvas _c, int _num)
        {
            ModuleDetails();
            
            if (_top < 0) { _top = 0; }
            if (_left < 0) { _left = 0; }

            _c.Children.Add(Module);
            Canvas.SetTop(Module, _top);
            Canvas.SetLeft(Module, _left);

            LEDDetails(_top, _left, _c);
            LabelDetails(_top, _left, _c, _num);
            ButtonDetails(4, _top, _left, _c);
            MonitorStatus();
            UpdateHexOut();
        }
        public bool GetInputStatus(int _i)
        {
            return Inputs[_i];
        }
        private void ButtonDetails(int _b, int _top, int _left, Canvas _c)
        {
            for(int i = 0; i < _b; i++)
            {
                Controls.Add(new Button { Width = 50, Height = 20});
                Controls[i].Content = i;
            }
            int j = 0;
            foreach(var _butt in Controls)
            {
                _c.Children.Add(_butt);
                Canvas.SetLeft(_butt, _left + 20);
                Canvas.SetTop(_butt, ((_top + 45 ) + 50 * (j + 1)));
                j += 1;
            }
        }
        private void MonitorStatus()
        {
            for(int i = 0; i < 4; i++)
            {
                Controls[i].Click += SetInputOnOff;
            }
        }
        public void SetInputOnOff(Object _ValueToPass, EventArgs e)
        {
            Button button = (Button)_ValueToPass;
            int content = Convert.ToInt32(button.Content);
            Inputs[content] = !Inputs[content];
            Publish[1] = content.ToString();
            ButtonIsClicked?.Invoke(null, EventArgs.Empty);
            UpdateHexOut();
            UpdateLEDStatus();
        }
        private void UpdateHexOut()
        {
            string _temp = "";
            for (int i = 3; i >= 0; i--)
            {
                _ = !Inputs[i] ? _temp += "0" : _temp += "1";
            }
            HexOut.Content = _temp;
            // Publisher 1
            Publish[2] = _temp;
            HexIsUpdated?.Invoke(Publish, EventArgs.Empty);
        }
        private void UpdateLEDStatus()
        {
            for (int i = 0; i < 4; i++)
            {
                LEDs[i].Fill = Inputs[i] ? Brushes.Yellow : Brushes.Transparent;
            }
        }
        private void ModuleDetails()
        {
            Module.Height = 400;
            Module.Width = 200;
            Module.Stroke = Brushes.Black;
            Module.StrokeThickness = 1;
        }
        // Adding LEDs
        private void LEDDetails(int _top, int _left, Canvas _c)
        {
            for (int i = 0; i < 4; i++)
            {
                LEDs.Add(new Rectangle { Width = 20, Height = 10, Stroke = Brushes.Black });
                _c.Children.Add(LEDs[i]);
                Canvas.SetTop(LEDs[i], _top + 50 * (i + 2));
                Canvas.SetLeft(LEDs[i], _left + 150);
            }
        }
        private void LabelDetails(int _top, int _left, Canvas _c, int _num)
        {
            // First label 0
            Labels.Add(new Label { Content = "Input Module : " + _num, Width = 200, HorizontalAlignment = System.Windows.HorizontalAlignment.Center });
            _c.Children.Add(Labels[0]);
            Canvas.SetLeft(Labels[0], _left + 60);
            Canvas.SetTop(Labels[0], 75);
            // Input labels 1-4
          for (int i = 1; i <= 4; i++)
          {
              Labels.Add(new Label { Content = "Input " + i, Width = 200, HorizontalAlignment = System.Windows.HorizontalAlignment.Left });
         //   _c.Children.Add(Labels[i]);
              Canvas.SetLeft(Labels[i], _left + 20);
              Canvas.SetTop(Labels[i], ((_top - 10) + 50 * (i + 1)));
          }
            // Module out label 5
          Labels.Add(new Label { Content = "Output (hex) :", Width = 200, HorizontalAlignment = System.Windows.HorizontalAlignment.Left });
          _c.Children.Add(Labels[5]);
          Canvas.SetLeft(Labels[5], _left + 20);
          Canvas.SetTop(Labels[5], 350);
            //Hexoutput label
            _c.Children.Add(HexOut);
            //HexOut.Content = "0000";
            HexOut.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            Canvas.SetLeft(HexOut, _left + 130);
            Canvas.SetTop(HexOut, 350);
        }
    }
}
