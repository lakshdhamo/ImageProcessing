using Autofac;
using BusinessService.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Managers.IManger
{
    public abstract class ExecuteAction
    {
        /// <summary>
        /// Defines whether it is Undo/Redo
        /// </summary>
        protected bool IsUndo = false;

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public abstract void Execute();

    }
}
