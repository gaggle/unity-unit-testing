using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// This is a helper class which for a given script, creates the appropriate prefab and attaches the script as a component.
// This ensures that the script has access to the right GameObjects and Unity doesn't complain!
// Additionall the Cleanup methods destroys the game objects so they don't stay around in edit mode
//
public class ScriptInstantiator
{
    private List<GameObject> GameObjects { get; set; }
    
    public ScriptInstantiator()
    {
        GameObjects = new List<GameObject>();
    }
    
    public T InstantiateScript<T>() where T : MonoBehaviour
    {
        GameObject gameObject;
        object prefab = Resources.Load("Prefabs/" + typeof(T).Name);
        
        if (prefab == null)
        {
            gameObject = new GameObject();
        }
        else
        {
            gameObject = GameObject.Instantiate(Resources.Load("Prefabs/" + typeof(T).Name)) as GameObject;
        }
        
        gameObject.name = typeof(T).Name + " (Test)";
        
        // Prefabs should already have the component
        T inst = gameObject.GetComponent<T>();
        if (inst == null)
        {
            inst = gameObject.AddComponent<T>();
        }
        
        MethodInfo startMethod = typeof(T).GetMethod("Start");
        if (startMethod != null)
        {
            startMethod.Invoke(inst, null);
        }
        
        GameObjects.Add(gameObject);
        return inst;
    }
    
    public void CleanUp()
    {
        foreach (GameObject gameObject in GameObjects)
        {
            GameObject.DestroyImmediate(gameObject);
        }
        
        GameObjects.Clear();
    }    
}

