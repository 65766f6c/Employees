# Employees
### Employees management library  
  
Default namespace: `Employees.Core`  
  
Easy start example:
```cs
var manager = new EmployeesManagerBuilder(configuration)
	.AddEmployees(employees)
	.Build();
		
var totalRate = manager.GetTotalRate(DateTime.UtcNow);
var rate = manager.Employees.First.GetTotalRate(DateTime.UtcNow);
```  
  
The `EmployeesConfiguration` type represents the container for collection of `EmployeeType`/`EmployeeDetails` pairs and directed to configure employees with specified preferences. The `EmployeeDetails` type stores surcharge details for specified type. If you are using chief employee types, you also may need to specify `SubordinatesSurchargePercentage`, and possibly `SubordinatesSurchargeLevel` (default is max value) properties. You can initialize configuration like the code below:
```cs
var configuration = new EmployeesConfiguration()
{
	Details = new Dictionary<EmployeeType, EmployeeDetails>()
	{
		{
			EmployeeType.Employee,
			new EmployeeDetails()
			{
				BaseSurchargePercentage = 0.03m,
				MaxBaseSurchargePercentage = 0.3m
			}
		},
		{
			EmployeeType.Manager,
			new EmployeeDetails()
			{
				BaseSurchargePercentage = 0.05m,
				MaxBaseSurchargePercentage = 0.4m,
				SubordinatesSurchargePercentage = 0.005m,
				SubordinatesSurchargeLevel = 1
			}
		},
		{
			EmployeeType.Sales,
			new EmployeeDetails()
			{
				BaseSurchargePercentage = 0.01m,
				MaxBaseSurchargePercentage = 0.35m,
				SubordinatesSurchargePercentage = 0.003m
			}
		}
	}
};
```
  
The `EmployeesManagerBuilder.AddEmployees` method takes `ICollection<EmployeeFlat>` as parameter. You can initialize your input like that (note that you need to set a unique identity for each instance in case it's a subordinate/chief):
```cs
var subordinateIds = new List<long>() { 1, 10, 11 };
var employeeFlat = new EmployeeFlat()
{
	Id = uniqueId,
	BaseRate = 1000000,
	DateOfEmployment = new DateTime(1999,11,11),
	Name = "John Doe",
	Type = EmployeeType.Manager,
	Subordinates = subordinateIds
}
```
  
A little more fluent example:
```cs
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

For UML diagram see [uml.jpg](https://github.com/65766f6c/Employees/blob/master/uml.jpg)
For additional info see [description.md](https://github.com/65766f6c/Employees/blob/master/description.md)
