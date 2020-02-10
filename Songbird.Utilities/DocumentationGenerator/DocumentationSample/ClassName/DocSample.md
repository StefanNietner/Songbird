# ClassName
Class Summary here

## Properties
|Name|Summary|
|-|-|
|Name1|Sum1|
|Name2|Name3|

## Methods
|Name|Summary|
|-|-|
|[Name1](Methods/SecondDoc)|Sum1|

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

Inline code is done `like` so
