# Vesting Problem

Vesting Problem is a command-line Application for vesting events which it will receive as inputs via a .csv file in terminal.

## Summary

#### Framework

The solution was developed using asp.net core, as it is an open source, cross-platform framework in which I have greater knowledge and development experience.

#### Tests

For the tests we used the xUnit library, which was developed by the same developers as Nunit.
In conjunction with xUnit, the Moq framework was used, for Mock data under test, and the fluentAssertion library, to assist in the elaboration of asserts.

## Run Application

- Unzip CAPS-CARTA.zip into a directory of your choice.

 ### Option 1
 - Open the solution VestingProblem.sln in visualStudio or any ide you like that supports the technology.
 - Run the Terminal project.
 - Add the inputs to the console as shown below.
   ex: example3.csv 2021-01-01 1

 ### Option 2
 - Open the build folder on any terminal of your choice that supports the technology.
 - Run the command **dotnet Terminal.dll**,  in your console.
 - Add the inputs to the console as shown below.
   ex: example3.csv 2021-01-01 1

```csv exemple
> echo example.csv
VEST,E001,Alice Smith,ISO-001,2020-01-01,1000 
VEST,E001,Alice Smith,ISO-001,2021-01-01,1000 
VEST,E001,Alice Smith,ISO-002,2020-03-01,300 
VEST,E001,Alice Smith,ISO-002,2020-04-01,500 
VEST,E002,Bobby Jones,NSO-001,2020-01-02,100 
VEST,E002,Bobby Jones,NSO-001,2020-02-02,200 
VEST,E002,Bobby Jones,NSO-001,2020-03-02,300 
VEST,E003,Cat Helms,NSO-002,2024-01-01,100
VEST,E001,Alice Smith,ISO-001,2020-01-01,1000.5 
CANCEL,E001,Alice Smith,ISO-001,2021-01-01,700.75 

```