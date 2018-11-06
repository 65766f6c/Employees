# Employees
Employees management library
```
namespace Employees.Core
```

Easy start example:
```
var manager = new EmployeesManagerBuilder(configuration)
	.AddEmployees(employees)
	.Build();
		
var totalRate = manager.GetTotalRate(DateTime.UtcNow);
```

A little more fluent example:
```
var employees = new EmployeesManagerBuilder()
	.AddEmployees(employees)
	.ConfigureEmployees(configuration =>
	{
		configuration.Details.Add(EmployeeType.Employee, new EmployeeDetails()
		{
			BaseSurchargePercentage = 0.03m,
			MaxBaseSurchargePercentage = 0.3m,
		});
		configuration.Details.Add(EmployeeType.Manager, new EmployeeDetails()
		{
			BaseSurchargePercentage = 0.05m,
			MaxBaseSurchargePercentage = 0.4m,
			SubordinatesSurchargePercentage = 0.005m,
			SubordinatesSurchargeLevel = 1
		});
		//etc
	})
	.Build()
	.Employees;
```
