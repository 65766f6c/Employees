# Employees  
  
This solution includes Core and Tests propjects. The Tests project contains the sample data and common methods for testing the solution. The Core project contains the employee management logic.  
  

## Core  
  
The entry point is the `EmployeesManagerBuilder` type, that is used for initialization `Domain.EmployeesManager` type instances. The `EmployeesManager` type allows to access a collection of domain employees and get total employees rate methods. First of all you need to configure `EmployeesManagerBuilder`. To do this pass an `Model.EmployeesConfiguration` instance to the `EmployeesManagerBuilder` constructor, or use the `EmployeesManagerBuilder.ConfigureEmployees` method to let the builder initialize configuration for you. Also you need to set up initial employees collection passing to the `EmployeesManagerBuilder.AddEmployees` method a collection of `Model.EmployeeFlat` types.  
  
To build an `EmployeesManager` it is also used the `EmployeesFactory` to map input flat employees to domain objects (`Employee`/`ChiefEmployee`) and inject preloaded (and configured) surcharge calculators to employees. `Employee` instances are decorated by `ChiefEmployee` at perforce (in case of it is not the base employee type and able to contain subordinates) to extend surcharge logic (base employees use employment duration surcharge calculators, chief employees use subordinates surcharge calculators). The `SubordinatesSurchargeCalculator` type use recursion methods to increase the execution speed. It can be switched to iterative realization to reduse memory usage. Also the `EmployeesFactory` validates the input for logical errors on the go (like multiple chiefs (restricted by the task) and subordinates closures). This logic didn't moved to separate type to increase execution speed, optionaly can be moved at the further.
