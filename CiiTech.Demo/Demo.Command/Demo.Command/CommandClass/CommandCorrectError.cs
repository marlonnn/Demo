using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDemo.CommandClasses
{
    class CommandCorrectError : Command
    {
        #region Constructor

        public CommandCorrectError(int operand, Model model) : base(operand, model)
        {
        }

        #endregion

        #region Methods

        public override void Execute()
        {
            /* m_Operand is the current value being displayed. We convert
             * it to a string and truncate the last digit. */

            string display = m_Operand.ToString();
            display = display.Substring(0, display.Length - 1);
            if (display == string.Empty) display = "0";

            // Set the calculator's display value
            m_Model.Display = display;
        }

        public override void Undo()
        {
            /* m_Operand is the value that was displayed before we corrected
             * the error. To undo, we simply reset the register to that value. */
            
            int newRegisterValue = m_Operand;
            m_Model.Register = newRegisterValue;
        }

        #endregion
    }
}
