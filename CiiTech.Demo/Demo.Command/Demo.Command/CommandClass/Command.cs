using System;
using System.Collections.Generic;
using System.Text;

/// Undo-Redo code is written using the article:
/// https://www.codeproject.com/Articles/12263/The-Command-Pattern-and-MVC-Architecture
//  The Command Pattern and MVC Architecture
//  By David Veeneman.
namespace CommandDemo.CommandClasses
{
    abstract class Command
    {
       #region Declarations

        protected int m_Operand;
        protected Model m_Model;

        #endregion

        #region Constructor

        public Command(int operand, Model model)
        {
            m_Operand = operand;
            m_Model = model;
        }

        #endregion

        #region Methods

        public abstract void Execute();

        public abstract void Undo();

        #endregion
    }
}
