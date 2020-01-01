using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Managers.IManger
{
    public interface IUndoRedoManager
    {
        string Undo();
        string Redo();
    }
}
