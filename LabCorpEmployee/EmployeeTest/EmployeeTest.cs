using LabCorpEmployee.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeTest
{
	[TestClass]
	public class EmployeeTest
	{
		//max working days - Mock
		int _maxWorkingDays = 260;
		int _hourlyMaxVacation = 10;
		int _salariedMaxVacation = 15;
		int _mgrMaxVacation = 30;

		[TestMethod]
		public void WorkTest()
		{
			HourlyEmployee hourly = new HourlyEmployee(_hourlyMaxVacation, _maxWorkingDays);
			var vacationdays = hourly.Work(200);
			Assert.IsTrue(vacationdays > 0);

			vacationdays = hourly.Work(-100);
			Assert.IsTrue(vacationdays > 0);

			SalariedEmployee salaried = new SalariedEmployee(_salariedMaxVacation, _maxWorkingDays);
			vacationdays = salaried.Work(200);
			Assert.IsTrue(vacationdays > 0);

			vacationdays = salaried.Work(-100);
			Assert.IsTrue(vacationdays > 0);

			Manager manager = new Manager(_mgrMaxVacation, _maxWorkingDays);
			vacationdays = manager.Work(200);
			Assert.IsTrue(vacationdays > 0);

			vacationdays = manager.Work(-100);
			Assert.IsTrue(vacationdays > 0);
		}

		[TestMethod]
		public void InitialWorkTest()
		{			
			HourlyEmployee hourly = new HourlyEmployee(_hourlyMaxVacation, _maxWorkingDays);
			var vacationdays = hourly.Work(-100);
			Assert.IsTrue(vacationdays == 0);
			vacationdays = hourly.Work(261);
			Assert.IsTrue(vacationdays == _hourlyMaxVacation);
			Assert.IsTrue(hourly.employeeWorkingDays == _maxWorkingDays);

			SalariedEmployee salaried = new SalariedEmployee(_salariedMaxVacation, _maxWorkingDays);
			vacationdays = salaried.Work(-100);
			Assert.IsTrue(vacationdays == 0);
			vacationdays = salaried.Work(261);
			Assert.IsTrue(vacationdays == _salariedMaxVacation);
			Assert.IsTrue(salaried.employeeWorkingDays == _maxWorkingDays);

			Manager mgr = new Manager(_mgrMaxVacation, _maxWorkingDays);
			vacationdays = mgr.Work(-100);
			Assert.IsTrue(vacationdays == 0);
			vacationdays = mgr.Work(261);
			Assert.IsTrue(vacationdays == _mgrMaxVacation);
			Assert.IsTrue(mgr.employeeWorkingDays == _maxWorkingDays);
		}

		[TestMethod]
		public void TakeVacationTest()
		{
			HourlyEmployee hourly = new HourlyEmployee(_hourlyMaxVacation, _maxWorkingDays);
			var vacationdays = hourly.TakeVacation(-100);
			Assert.IsTrue(vacationdays == 0);
			vacationdays = hourly.TakeVacation(261);
			Assert.IsTrue(vacationdays == 0);
			vacationdays = hourly.Work(100);
			Assert.IsTrue(vacationdays >= hourly.TakeVacation(2));
			Assert.IsTrue(hourly.TakeVacation(2) < _hourlyMaxVacation);
			Assert.IsTrue(hourly.TakeVacation(20) < _hourlyMaxVacation && hourly.TakeVacation(200) == hourly.totalVacationDaysAccumulated);

			SalariedEmployee salaried = new SalariedEmployee(_salariedMaxVacation, _maxWorkingDays);
			vacationdays = salaried.TakeVacation(-100);
			Assert.IsTrue(vacationdays == 0);
			vacationdays = salaried.TakeVacation(261);
			Assert.IsTrue(vacationdays == 0);
			vacationdays = salaried.Work(100);
			Assert.IsTrue(vacationdays >= salaried.TakeVacation(2));
			Assert.IsTrue(salaried.TakeVacation(2)< _salariedMaxVacation);
			Assert.IsTrue(salaried.TakeVacation(20) < _salariedMaxVacation && salaried.TakeVacation(200) == salaried.totalVacationDaysAccumulated);

			Manager mgr = new Manager(_mgrMaxVacation, _maxWorkingDays);
			vacationdays = mgr.TakeVacation(-100);
			Assert.IsTrue(vacationdays == 0);
			vacationdays = mgr.TakeVacation(261);
			Assert.IsTrue(vacationdays == 0);
			vacationdays = mgr.Work(100);
			Assert.IsTrue(vacationdays >= mgr.TakeVacation(2));
			Assert.IsTrue(mgr.TakeVacation(2) < _mgrMaxVacation);
			Assert.IsTrue(mgr.TakeVacation(20) < _mgrMaxVacation && mgr.TakeVacation(200) == mgr.totalVacationDaysAccumulated);
		}
	}
}
