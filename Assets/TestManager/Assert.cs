using System;

public class AssertFailedException : Exception
{
    public AssertFailedException(string message) : base(message)
    {
    }
}

public static class Assert
{
    public static void IsTrue(bool condition)
    {
        if (!condition)
        {
            throw new AssertFailedException("Assert.IsTrue failed. Condition is False.");
        }
    }

    public static void AreEqual(object expected, object actual)
    {
        if (!actual.Equals(expected))
        {
            throw new AssertFailedException(
                string.Format("Assert.AreEqual failed. Expected value is {0} but the Actual value is {1}.", expected, actual));
        }
    }
    
    public static void IsNotNull(object obj)
    {
        if (obj == null)
        {
            throw new AssertFailedException("Assert.IsNotNull failed.");
        }
    }
}

