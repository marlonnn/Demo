using System;
using System.Collections.Generic;
using System.Text;
using CommandDemo.CommandClasses;

namespace CommandDemo
{

    class Controller
    {
        #region Declarations

        // Member variables
        List<Command> m_HistoryList = new List<Command>();
        int m_NextUndo = -1;

        #endregion

        #region Constructor

        #endregion

        #region Public Methods

        public void ExecuteCommand(Command command)
        {
            // Purge history list
            this.TrimHistoryList();

            // Execute the command and add it to the history list
            command.Execute();
            m_HistoryList.Add(command);

            // Move the 'next undo' pointer to point to the new command
            m_NextUndo++;
        }

        public void Undo()
        {
            // If the NextUndo pointer is -1, no commands to undo
            if (m_NextUndo < 0) return;

            // Get the Command object to be undone
            Command command = m_HistoryList[m_NextUndo];

            // Execute the Command object's undo method
            command.Undo();

            // Move the pointer up one item
            m_NextUndo--;
        }

        public void Redo()
        {
            // If the NextUndo pointer points to the last item, no commands to redo
            if (m_NextUndo == m_HistoryList.Count - 1) return;

            // Get the Command object to redo
            int itemToRedo = m_NextUndo + 1;
            Command command = m_HistoryList[itemToRedo];

            // Execute the Command object
            command.Execute();

            // Move the undo pointer down one item
            m_NextUndo++;
        }

        #endregion

        #region Private Methods

        private void TrimHistoryList()
        {
            /* We can redo any undone command until we execute a new 
             * command. The new command takes us off in a new direction,
             * which means we can no longer redo previously undone actions. 
             * So, we purge all undone commands from the history list.*/

            // Exit if no items in History list
            if (m_HistoryList.Count == 0) return;

            // Exit if NextUndo points to last item on the list
            if (m_NextUndo == m_HistoryList.Count - 1) return;

            // Purge all items below the NextUndo pointer
            for (int i = m_HistoryList.Count - 1; i > m_NextUndo; i--)
            {
                m_HistoryList.RemoveAt(i);
            }
        }

        #endregion
    }

}
