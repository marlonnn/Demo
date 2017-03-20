using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommandDemo.CommandClasses;

namespace CommandDemo
{
    public partial class View : Form
    {
        #region Declarations

        Model m_Model = new Model();
        Controller m_Controller = new Controller();

        /* We set this flag to 'true' so that the zero in the
         * display when the application is launched will be 
         * cleared when the user enters the first digit. */

        bool m_DisplayIsShowingRegister = true;

        #endregion

        #region Constructor

        public View()
        {
            InitializeComponent();

            // Subscribe register to Model's DisplayChange event
            m_Model.DisplayChange += this.DisplayValue_Changed;

            textBoxLog.Text = "\tRegister= 0\r\n\r\n";
        }

        #endregion

        #region Event Handlers

        private void DisplayValue_Changed(object sender, DisplayChangeEventArgs e)
        {
            /* Updates the display when the Model's display value changes. */

            // Update display
            textBoxDisplay.Text = e.DisplayValue.ToString();

            if (e.IsRegisterChange)
            {
                m_DisplayIsShowingRegister = true;
            }

            // Update log
            textBoxLog.Text += e.LogEntry;

            textBoxLog.SelectionStart = textBoxLog.Text.Length;
            textBoxLog.ScrollToCaret();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int operand = Convert.ToInt32(textBoxDisplay.Text);
            Command commandAdd = new CommandAdd(operand, m_Model);
            m_Controller.ExecuteCommand(commandAdd);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            int operand = m_Model.Register;
            Command commandClear = new CommandClear(operand, m_Model);
            m_Controller.ExecuteCommand(commandClear);
        }

        private void buttonCorrectError_Click(object sender, EventArgs e)
        {
            int currentDisplayValue = Convert.ToInt32(textBoxDisplay.Text.ToString());
            Command commandCorrectError = new CommandCorrectError(currentDisplayValue, m_Model);
            m_Controller.ExecuteCommand(commandCorrectError);
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            int operand = Convert.ToInt32(textBoxDisplay.Text);
            Command commandDivide = new CommandDivide(operand, m_Model);
            m_Controller.ExecuteCommand(commandDivide);
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            int operand = Convert.ToInt32(textBoxDisplay.Text);
            Command commandMultiply = new CommandMultiply(operand, m_Model);
            m_Controller.ExecuteCommand(commandMultiply);
        }

        private void buttonNumeric_Click(object sender, EventArgs e)
        {
            if (m_DisplayIsShowingRegister)
            {
                textBoxDisplay.Clear();
                m_DisplayIsShowingRegister = false;
            }

            Button buttonPressed = (Button) sender;
            textBoxDisplay.Text += buttonPressed.Text;
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            /* Note that we don't create a Command object for Redo.
             * That's because it wouldn't make sense to undo a redo.
             * We could simply undo the action that was redone. Nor 
             * would it make sense to redo a redo. */

            m_Controller.Redo();
        }

        private void buttonSubtract_Click(object sender, EventArgs e)
        {
            int operand = Convert.ToInt32(textBoxDisplay.Text);
            Command commandSubtract = new CommandSubtract(operand, m_Model);
            m_Controller.ExecuteCommand(commandSubtract);
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            /* Note that we don't create a Command object for Undo.
             * That's because it wouldn't make sense to undo an undo.
             * We could simply redo the action that was undone. Nor 
             * would it make sense to redo an undo. */

            m_Controller.Undo();
        }

        #endregion
    }
}