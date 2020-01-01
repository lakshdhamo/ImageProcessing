using BusinessService.Managers.Action;
using BusinessService.Managers.IManger;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessService.CommonOperation;
using Autofac;
using BusinessService.Repository;

namespace BusinessService.Managers
{
    public class EmployeeManager : IEmployeeManager
    {
        /// <summary>
        /// Adds the employee.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public List<string> AddEmployee(string name)
        {
            /// Input parameter cannot be empty.
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();

            ExecuteAction ExecuteAction = new EmployeeAction(name);
            ExecuteAction.Execute();
            return DataContext.Current.EmployeeList;
        }
    }
}
