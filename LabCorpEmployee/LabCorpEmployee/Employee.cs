using System;
using System.Collections.Generic;
using System.Text;

namespace LabCorpEmployee
{
	public abstract class Employee
	{
		#region private
		private int _maxVacationDays;
        private int _workingDays;
        private int _empWorkingdays=0;
        public Employee(int maxVacationDays, int workingDays)
        {
            _maxVacationDays = maxVacationDays;
            _workingDays = workingDays;
        }

        private float _totalVacationAccumulated = 0;
        private float vacationdays { get { return _totalVacationAccumulated; } set { _totalVacationAccumulated = value; } }
		#endregion

		#region ClassVariables
		public float totalVacationDaysAccumulated { get { return vacationdays; } }
        public int employeeWorkingDays { get { return _empWorkingdays; } }
		#endregion

		public float TakeVacation(float vacationUsed)
        {
            vacationdays = (vacationdays > vacationUsed && vacationUsed>0)?(vacationdays - vacationUsed):vacationdays;
            if (vacationdays < vacationUsed)
                Console.WriteLine("Max vacation days accrued: " + vacationdays);
            return vacationdays;
        }
        public float Work(int daysWorked)
        {
            if (daysWorked > 0 && daysWorked <= _workingDays && _empWorkingdays < _workingDays)
                vacationdays += ((float)daysWorked * (_maxVacationDays / (float)_workingDays));
            else if (daysWorked >= _workingDays)
            {
                Console.WriteLine("Max Vacation days for this employee is " + _maxVacationDays);
                vacationdays = _maxVacationDays;
            }
            else if (daysWorked < 0)
                Console.WriteLine("Working days cannot be negitive.");		

            _empWorkingdays += daysWorked<0?0:daysWorked;
            if (_empWorkingdays > _workingDays)
            {
                Console.WriteLine("Max Working Days of an employee considered is " + _workingDays);
                _empWorkingdays = _workingDays;
            }
            
            return vacationdays;
        }
    }
}
