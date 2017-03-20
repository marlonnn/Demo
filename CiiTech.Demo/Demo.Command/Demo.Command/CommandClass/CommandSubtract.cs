using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDemo.CommandClasses
{
    class CommandSubtract : Command
    {
        #region Constructor

        public CommandSubtract(int operand, Model model) : base(operand, model)
        {
        }

        #endregion

        #region Methods

        public override void Execute()
        {
            m_Model.Subtract(m_Operand);
        }

        public override void Undo()
        {
            m_Model.Add(m_Operand);
        }

        #endregion

    }
}
