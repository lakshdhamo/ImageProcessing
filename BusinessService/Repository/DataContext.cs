using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Repository
{
    public sealed class DataContext
    {
        private DataContext()
        {
        }
        private readonly static Lazy<DataContext> lazy = new Lazy<DataContext>(() => new DataContext());

        /// <summary>
        /// Gets single instance.
        /// </summary>
        /// <value>
        /// The current instance.
        /// </value>
        public static DataContext Current
        {
            get
            {
                return lazy.Value;
            }
        }

        /// <summary>
        /// Returns employee list
        /// </summary>
        public List<string> EmployeeList = new List<string>();
    }
}
