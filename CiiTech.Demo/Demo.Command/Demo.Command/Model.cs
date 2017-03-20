using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDemo
{
    class Model
    {
        #region Declarations

        // DisplayChange event
        public delegate void DisplayChangeEventHandler(object sender, DisplayChangeEventArgs e);
        public event DisplayChangeEventHandler DisplayChange;

        // Member variables
        private int p_Register = 0;
        private string p_Display = "0";

        #endregion

        #region Properties

        internal string Display
        {
            get { return p_Display; }
            set
            {
                p_Display = value;
                string logEntry = "Display reset.\r\n\r\n";
                this.FireDisplayChangeEvent(Convert.ToInt32(p_Display), logEntry, false);
            }
        }

        internal int Register
        {
            get { return p_Register; }
            set 
            { 
                p_Register = value;
                string logEntry = "Register reset";
                this.FireDisplayChangeEvent(p_Register, logEntry, true);
            }
        }

        #endregion

        #region Internal Methods

        internal void Add(int operand)
        {
            p_Register += operand;
            string logEntry = String.Format("Added {0} to Register.\r\n\tRegister = {1}\r\n\r\n", operand, p_Register);
            this.FireDisplayChangeEvent(p_Register, logEntry, true);
        }

        internal void Divide(int operand)
        {
            p_Register /= operand;
            string logEntry = String.Format("Divided Register by {0}.\r\n\tRegister = {1}\r\n\r\n", operand, p_Register);
            this.FireDisplayChangeEvent(p_Register, logEntry, true);
        }

        internal void Multiply(int operand)
        {
            p_Register *= operand;
            string logEntry = String.Format("Multiplied Register by {0}.\r\n\tRegister = {1}\r\n\r\n", operand, p_Register);
            this.FireDisplayChangeEvent(p_Register, logEntry, true);
        }

        internal void Subtract(int operand)
        {
            p_Register -= operand;
            string logEntry = String.Format("Subtracted {0} from Register.\r\n\tRegister = {1}\r\n\r\n", operand, p_Register);
            this.FireDisplayChangeEvent(p_Register, logEntry, true);
        }

        internal void Clear()
        {
            p_Register = 0;
            string logEntry = String.Format("Register cleared.\r\n\tRegister = {0}\r\n\r\n", p_Register);
            this.FireDisplayChangeEvent(p_Register, logEntry, true);
        }

        #endregion

        #region Private Methods

        private void FireDisplayChangeEvent(int register, string logEntry, bool isRegisterChange)
        {
            if (DisplayChange != null)
            {
                DisplayChange(this, new DisplayChangeEventArgs(register, logEntry, isRegisterChange));
            }
       }

        #endregion
   }

   #region DisplayChangeEventArgs Class

   class DisplayChangeEventArgs : System.EventArgs
    {
        private int p_DisplayValue = 0;
        private string p_LogEntry = string.Empty;
        private bool p_IsRegisterChange = false;

        public DisplayChangeEventArgs(int displayValue, string logEntry, bool isRegisterChange)
        {
            p_DisplayValue = displayValue;
            p_LogEntry = logEntry;
            p_IsRegisterChange = isRegisterChange;
        }

        public int DisplayValue
        {
            get { return p_DisplayValue; }
        }

       public bool IsRegisterChange
       {
           get { return p_IsRegisterChange; }
       }

       public string LogEntry
       {
           get { return p_LogEntry; }
       }
    }

   #endregion
}
