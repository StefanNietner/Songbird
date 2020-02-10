# NotNull\<T>(T)
Checks the given value against NULL, throwing an Exception if the given value is NULL.
## Remarks
  
Some remarkable information here  
maybe somethings in a second line?  

## Type parameters
|Name|Summary|
|-|-|
|T|The type of the given value.|
## Parameters
|Name|Summary|
|-|-|
|value|The value to be checked.|
## Exceptions
Songbird.Utilities.Guards.Exceptions.GuardClauseViolationException  
value is NULL

## Example
```c#  
//param coming from outside source  
public void Foo(HttpResponseMessage response)  
{  
    try  
    {  
        Guard.NotNull(response);  
        //response is assured to not be null at this point.  
        //so it can be processed freely  
    }  
    catch(GuardClauseViolationException ex)  
    {  
        //Errorhandling here  
    }  
}  
```
