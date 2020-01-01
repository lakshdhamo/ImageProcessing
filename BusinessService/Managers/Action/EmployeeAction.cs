using BusinessService.Managers.IManger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessService.Repository;

namespace BusinessService.Managers.Action
{
    public class EmployeeAction : ExecuteAction
    {
        string _name;
        bool _isDo = true;
        private readonly IUndoRedoManager _undoRedoManager;

        public EmployeeAction(string paramName)
        {
            _name = paramName;
        }

        public override void Execute()
        {
            if (!IsUndo)
            {
                /// Do/Redo Operation
                DataContext.Current.EmployeeList.Add(_name);

                if (_isDo)
                {
                    UndoRedoManager.AddAction(this);
                    _isDo = false;
                }
                IsUndo = true;
            }
            else
            {
                /// Undo Operation
                DataContext.Current.EmployeeList.Remove(_name);
                IsUndo = false;
            }

        }
    }
}
