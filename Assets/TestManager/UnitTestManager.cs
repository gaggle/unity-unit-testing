using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

public class TestItem
{
    public string Name;
    public string Message;
    public bool? Success;
}

public class UnitTestManager
{
    public List<TestItem> TestItems { get; private set; }
    
    public bool IsRunning { get; set; }

    public UnitTestManager()
    {
        TestItems = new List<TestItem>();
    }
    
    public void RunTests()
    {
        if (IsRunning)
        {
            return;
        }
            
        TestItems.Clear();
       
        IsRunning = true;

        var assembly = Assembly.GetCallingAssembly();
        Type[] types = assembly.GetTypes();

        // Find all the test classes
        //
        foreach (Type type in types)
        {
            if (type.IsDefined(typeof(TestFixtureAttribute), false))
            {
                object fixtureInstance = Activator.CreateInstance(type); 

                MethodInfo[] methods = type.GetMethods();
                MethodInfo setUpMethod = methods.FirstOrDefault(x => x.IsDefined(typeof(SetUpAttribute), false));
                MethodInfo tearDownMethod = methods.FirstOrDefault(x => x.IsDefined(typeof(TearDownAttribute), false));
                foreach (MethodInfo testMethod in methods)
                {
                    if (testMethod.IsDefined(typeof(TestAttribute), false))
                    {
                        // Add the item to the list for UI purposes
                        TestItems.Add(new TestItem { Name = type.Name + "::" + testMethod.Name });
      
                        TestItems.Last().Success = true;

                        try
                        {
                            // First run setup for the test if available
                            if (setUpMethod != null)
                            {
                                setUpMethod.Invoke(fixtureInstance, null);
                            }
                        }
                        catch (Exception e)
                        {
                            TestItems.Last().Success = false;
                            TestItems.Last().Message = e.ToString();
                        }
      
                        
                        // If the setup was successful, run the test method, ignoring expected exceptions
                        //
                        if (TestItems.Last().Success.Value)
                        {
                            try
                            {
                                testMethod.Invoke(fixtureInstance, null);
                            }
                            catch (Exception e)
                            {
                                if (testMethod.IsDefined(typeof(ExpectedException), false))
                                {
                                    var expectedEx = testMethod.GetCustomAttributes(typeof(ExpectedException), false)[0] as ExpectedException;
                                    if (e.InnerException == null || expectedEx.ExceptionType != e.InnerException.GetType())
                                    {
                                        TestItems.Last().Success = false;
                                        TestItems.Last().Message = e.ToString();
                                    }
                                }
                                else
                                {
                                    TestItems.Last().Success = false;
                                    TestItems.Last().Message = e.ToString();
                                }
                            }
                        }
      
                        try
                        {
                            // Finally clean up regardless of previous results
                            //
                            if (tearDownMethod != null)
                            {
                                tearDownMethod.Invoke(fixtureInstance, null);
                            }
                        }
                        catch (Exception e)
                        {
                            TestItems.Last().Success = false;
                            TestItems.Last().Message = e.ToString();
                        }
                    }
                }
            }
        }
        
        IsRunning = false;
    }
}
