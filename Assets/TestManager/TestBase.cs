using System;

public class TestBase
{
    protected ScriptInstantiator ScriptInstantiator { get; private set; }
    
    [SetUp]
    public virtual void SetUp()
    {
        if (ScriptInstantiator == null)
        {
            ScriptInstantiator = new ScriptInstantiator();
        }
    }
    
    [TearDown]
    public virtual void TearDown()
    {
        ScriptInstantiator.CleanUp();
    }
}

