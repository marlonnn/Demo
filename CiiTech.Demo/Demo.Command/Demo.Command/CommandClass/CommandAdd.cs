using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDemo.CommandClasses
{
    class CommandAdd : Command
    {
        #region Constructor

        public CommandAdd(int operand, Model model) : base(operand, model)
        {
        }

        #endregion

        #region Methods

        public override void Execute()
        {
            m_Model.Add(m_Operand);
        }

        public override void Undo()
        {
            m_Model.Subtract(m_Operand);
        }

        #endregion
}
}
