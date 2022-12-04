using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class ColumnsModel
    {
        public string Name { get; set; }

        public ColumnsModel(string name)
        {
            this.Name = name;
        }
    }
}