using System;
using System.Collections.Generic;
using System.Text;

namespace LabCorpEmployee.Model
{
    public class Manager : Employee
    {
        public Manager(int _maxVacationDays, int workingDays) : base(_maxVacationDays, workingDays) { }
    }
}