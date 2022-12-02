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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CreateBlocks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int NUMOFIMODS = 3;
        private const int NUMOFOMODS = 1;
        private readonly List<Modules.FourDI> _IModules = new List<Modules.FourDI>(NUMOFIMODS);
        private readonly List<Modules.FourDO> _OModules = new List<Modules.FourDO>(NUMOFOMODS);
        private string[] Subscribe = new string[3];
        public EventHandler InputStatus;
        public EventHandler OutputStatus;
        //public EventHandler InputControl;
        //public EventHandler OutputControl;
        private bool[] _inputStatus = new bool[12];
        private bool[] _outputStatus = new bool[4];
        //private bool[] _inputControl = new bool[8];
        //private bool[] _outputControl = new bool[8];

        public MainWindow()
        {
            InitializeComponent();
            InsertModules(NUMOFIMODS, NUMOFOMODS);
            for(int i = 0; i < NUMOFIMODS; i++)
            {
                _IModules[i].ButtonIsClicked += new EventHandler(ProgramProper);
            }
        }
        //************************************ PROGRAM FOR THE MODULES - START *********************************************//
        public void ProgramProper(object sender, EventArgs e)
        {
            
            // Logic for Output 1
            if (_IModules[0].GetInputStatus(0) && _IModules[1].GetInputStatus(0) && _IModules[2].GetInputStatus(0))
            {
                _OModules[0].SetInputOn(0);
            }
            else
            {
                _OModules[0].SetInputOff(0);
            }
            // Logic for Output 2
            if (_IModules[0].GetInputStatus(1) && _IModules[1].GetInputStatus(1) && _IModules[2].GetInputStatus(1))
            {
                _OModules[0].SetInputOn(1);
            }
            else
            {
                _OModules[0].SetInputOff(1);
            }
            // Logic for Output 3
            if (_IModules[0].GetInputStatus(2) && (_IModules[1].GetInputStatus(2) || _IModules[2].GetInputStatus(2)))
            {
                _OModules[0].SetInputOn(2);
            }
            else
            {
                _OModules[0].SetInputOff(2);
            }
            // Logic for Output 4
            if ((_IModules[0].GetInputStatus(3) || _IModules[1].GetInputStatus(3)) && _IModules[2].GetInputStatus(3))
            {
                _OModules[0].SetInputOn(3);
            }
            else
            {
                _OModules[0].SetInputOff(3);
            }
            // Publishing
            PublishInputStatus();
            PublishOutputStatus();
        }
        private void PublishInputStatus()
        {
            int _m = 0;
            string _s = "";
            for (int _i = 0; _i < 3; _i++)
            {
                for (int _k = 0; _k < 4; _k++)
                {
                    _inputStatus[_m] = _IModules[_i].GetInputStatus(_k);
                    _m += 1;
                }
            }
            for(int i = 11; i >= 0; i--)
            {
                _s += _inputStatus[i] ? "1" : "0";
            }
            InputStatus?.Invoke(_inputStatus, EventArgs.Empty);
            //MessageBox.Show(_s, "Inputs :");
        }
        private void PublishOutputStatus()
        {
            int _m = 0;
            string _s = "";
            for (int _k = 0; _k < 4; _k++)
            {
                _outputStatus[_m] = _OModules[0].GetOutputStatus(_k);
                _m += 1;
            }
            for(int i = 3; i >= 0; i--)
            {
                _s += _outputStatus[i] ? "1" : "0";
            }
            OutputStatus?.Invoke(_outputStatus, EventArgs.Empty);
            //MessageBox.Show(_s, "Outputs :");
        }

            //************************************ PROGRAM FOR THE MODULES - END *********************************************//
            private void InsertModules(int _i, int _o)
        {
            for (int i = 0; i < _i; i++)
            {
                _IModules.Add(new Modules.FourDI());
            }
            for (int i = 0; i < _o; i++)
            {
                _OModules.Add(new Modules.FourDO());
            }
            int k = 0;
            int j = 0;
            foreach (Modules.FourDI _mod in _IModules)
            {
                _mod.SetModuleLocation(50, 50 + (230 * k), CanvasMain, 1 * (j + 1));
                k += 1;
                j += 1;
            }
            j = 0;
            foreach (var _mod in _OModules)
            {
                _mod.SetModuleLocation(50, 50 + (230 * k), CanvasMain, 1 * (j + 1));
                k += 1;
                j += 1;
            }
        }
    }
}
