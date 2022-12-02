using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CreateBlocks.Classes
{
    public class DataEventArgs : EventArgs
    {
        private string[] Publisher = new string[2];
   
        public void SetData(int _i, string _s)
        {
            if (_i <= 10)
            {
                Publisher[_i] = _s;
            }
            else
            {
                MessageBox.Show("Wrong Input");
            }
            
        }
        public int GetCount()
        {
            return Publisher.Length;
        }
        public string GetData(int _i)
        {
            return Publisher[_i];
        }

    }
}
