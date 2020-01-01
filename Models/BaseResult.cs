using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BaseResult
    {
        public string ActionName { get; set; }
        public bool EnableUndo { get; set; }
        public bool EnableRedo { get; set; }

    }
}
