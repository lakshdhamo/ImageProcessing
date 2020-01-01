using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Managers.IManger
{
    public interface IEmployeeManager
    {
        List<string> AddEmployee(string name);
    }
}
