using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDemo.CommandClasses
{
    class CommandMultiply : Command
    {
        #region Constructor

        public CommandMultiply(int operand, Model model) : base(operand, model)
        {
        }

        #endregion

        #region Methods

        public override void Execute()
        {
            m_Model.Multiply(m_Operand);
        }

        public override void Undo()
        {
            m_Model.Divide(m_Operand);
        }

        #endregion
    }
}
