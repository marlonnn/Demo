using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDemo.CommandClasses
{
    class CommandClear : Command
    {
        #region Declarations

        private int m_OldRegister = 0;

        #endregion

        #region Constructor

        public CommandClear(int operand, Model model) : base(operand, model)
        {
        }

        #endregion

        #region Methods

        public override void Execute()
        {
            m_OldRegister = m_Model.Register;
            m_Model.Clear();
        }

        public override void Undo()
        {
            m_Model.Register = m_OldRegister;
        }

        #endregion
    }
}
