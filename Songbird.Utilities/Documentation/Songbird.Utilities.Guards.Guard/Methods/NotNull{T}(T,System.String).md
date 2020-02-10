# NotNull\<T>(T,System.String)
Checks the given value against NULL and throws an T:Songbird.Utilities.Guards.Exceptions.GuardClauseViolationException if true.
## Type parameters
|Name|Summary|
|-|-|
|T|The type of the given value.|
## Parameters
|Name|Summary|
|-|-|
|value|The value to be checked.|
|message|The custom error message.|
## Exceptions
Songbird.Utilities.Guards.Exceptions.GuardClauseViolationException  
Occurs when the given value is NULL

## Example
```c#  
//param coming from outside source  
public void Foo( response)  
{  
    try  
    {  
        .NotNull(response, "No Response was given.");  
        //response is assured to not be null at this point.  
        //so it can be processed freely  
    }  
    catch( ex)  
    {  
        //Errorhandling here  
    }  
}  
```
