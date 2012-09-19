// Copied from http://www.altdevblogaday.com/2012/03/05/unit-testing-in-unity/

//using System;
//
//public class TestCode : TestBase
//{
//	[Test]
//	public void MovingEntitiesUpdatesConnector()
//	{
//	    var source = ScriptInstantiator.InstantiateScript<Entity>();
//	    var target = ScriptInstantiator.InstantiateScript<Entity>();
//	    var connector = ScriptInstantiator.InstantiateScript<Connector>();
//	 
//	    connector.SetSourceEntity(source);
//	    connector.SetTargetEntity(target, true);
//	 
//	    source.transform.position = new Vector3(-10.0f, 0.0f, 0.0f);
//	    target.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
//	 
//	    connector.Update();
//	 
//	    Assert.IsTrue(Vector3.Distance(connector.transform.position, source.transform.position) < 0.01f);
//	    Assert.IsTrue(Vector3.Distance(connector.EndPoint, target.transform.position) < 0.01f);
//	}
//}
