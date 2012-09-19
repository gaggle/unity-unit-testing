using System;

public class TestFixtureAttribute : Attribute
{
}

public class TestAttribute : Attribute
{
}

public class SetUpAttribute : Attribute
{
}

public class TearDownAttribute : Attribute
{
}

public class ExpectedException : Attribute
{
    public ExpectedException(Type exceptionType)
    {
        ExceptionType = exceptionType;
    }
    
    public Type ExceptionType { get; set; }
}