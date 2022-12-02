using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes; //rectangle
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CreateBlocks.Modules
{
    internal class FourDO
    {
        private readonly Rectangle Module = new Rectangle();
        private readonly List<Rectangle> LEDs = new List<Rectangle>();
        private readonly List<Label> Labels = new List<Label>();
        private readonly Label HexOut = new Label();
        private readonly bool[] Outputs = new bool[4] { false, false, false, false };

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
            UpdateHexOut();
        }
        public void SetInputOn(int _i)
        {
            Outputs[_i] = true;
            UpdateHexOut();
            UpdateLEDStatus();
        }
        public void SetInputOff(int _i)
        {
            Outputs[_i] = false;
            UpdateHexOut();
            UpdateLEDStatus();
        }
        public bool GetOutputStatus(int _i)
        {
            return Outputs[_i];
        }
        private void UpdateHexOut()
        {
            string _temp = "";
            for (int i = 3; i >= 0; i--)
            {
                _ = !Outputs[i] ? _temp += "0" : _temp += "1";
            }
            HexOut.Content = _temp;
        }
        private void UpdateLEDStatus()
        {
            for (int i = 0; i < 4; i++)
            {
                LEDs[i].Fill = Outputs[i] ? Brushes.Yellow : Brushes.Transparent;
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
            Labels.Add(new Label { Content = "Output Module : " + _num, Width = 200, HorizontalAlignment = System.Windows.HorizontalAlignment.Center });
            _c.Children.Add(Labels[0]);
            Canvas.SetLeft(Labels[0], _left + 60);
            Canvas.SetTop(Labels[0], 75);
            // Input labels 1-4
            for (int i = 1; i <= 4; i++)
            {
                Labels.Add(new Label { Content = "Output " + i, Width = 200, HorizontalAlignment = System.Windows.HorizontalAlignment.Left });
                _c.Children.Add(Labels[i]);
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
