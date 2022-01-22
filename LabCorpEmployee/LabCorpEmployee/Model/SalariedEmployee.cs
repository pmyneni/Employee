using System;
using System.Collections.Generic;
using System.Text;

namespace LabCorpEmployee.Model
{
    public class SalariedEmployee :Employee
    {
        public SalariedEmployee(int VacationDays, int workingDays) : base(VacationDays, workingDays) { }
    }
}
