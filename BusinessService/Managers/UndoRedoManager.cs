using BusinessService.Managers.Action;
using BusinessService.Managers.IManger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BusinessService.Repository;
using Newtonsoft.Json.Linq;
using Models;

namespace BusinessService.Managers
{
    public class UndoRedoManager : BaseManager, IUndoRedoManager
    {
        static List<ExecuteAction> _actionList = new List<ExecuteAction>();
        static List<ExecuteAction> _undoList = new List<ExecuteAction>();

        /// <summary>
        /// Adds the specified execute action.
        /// </summary>
        /// <param name="_executeAction">The execute action.</param>
        public static void AddAction(ExecuteAction _executeAction)
        {
            _actionList.Add(_executeAction);
        }

        /// <summary>
        /// Performs the undo.
        /// </summary>
        public string Undo()
        {
            /// Perform Undo
            _actionList[_actionList.Count() - 1].Execute();

            /// Manipulate Undo/Redo list
            string actionName = _actionList[_actionList.Count() - 1].GetType().Name;
            _undoList.Add(_actionList[_actionList.Count() - 1]);
            _actionList.RemoveAt(_actionList.Count() - 1);

            /// Generate output for Redo operation
            return PrepareUndoRedoOutput(actionName);

        }

        /// <summary>
        /// Performs the redo.
        /// </summary>
        public string Redo()
        {
            /// Perform Redo
            _undoList[_undoList.Count() - 1].Execute();

            /// Manipulate Undo/Redo list
            string actionName = _undoList[_undoList.Count() - 1].GetType().Name;
            _actionList.Add(_undoList[_undoList.Count() - 1]);
            _undoList.RemoveAt(_undoList.Count() - 1);

            /// Generate output for Redo operation
            return PrepareUndoRedoOutput(actionName);
        }

        /// <summary>
        /// Prepares the undo redo output.
        /// </summary>
        /// <returns></returns>
        private string PrepareUndoRedoOutput(string actionName)
        {
            BaseResult _baseResult = null;

            switch (actionName)
            {
                case "EmployeeAction":
                    _baseResult = new EmployeeResult()
                    {
                        ActionName = "EmployeeAction",
                        EnableUndo = _actionList.Count() > 0 ? true : false,
                        EnableRedo = _undoList.Count() > 0 ? true : false,
                        EmployeeList = DataContext.Current.EmployeeList

                    };
                    break;

            }

            /// Not every layer contains Model project reference. So, Serialized into JSON and passing over the layers.
            return JsonConvert.SerializeObject(_baseResult);
        }


    }
}