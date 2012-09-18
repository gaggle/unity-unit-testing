using System;
using UnityEngine;
using UnityEditor;

public class UnitTestWindow : EditorWindow 
{ 
    private UnitTestManager unitTestManager;
    
    [MenuItem ("Window/Unit Tests")]
    private static void Init()
    {
        // Get existing open window or if none, make a new one:
        EditorWindow.GetWindow(typeof(UnitTestWindow));
    }
    
    private void OnEnable()
    {
        unitTestManager = new UnitTestManager();
    }

    private void OnGUI()
    {
        GUI.skin = Resources.Load("GUISkins/Tests") as GUISkin;
        
        if (GUILayout.Button("Run Tests", GUILayout.Width(150), GUILayout.Height(40)))
        {
            unitTestManager.RunTests();
        }
        
        GUILayout.BeginVertical();
        
            foreach (TestItem testItem in unitTestManager.TestItems)
            {
                string style = testItem.Success == null ? "Label" : (testItem.Success.Value ? "Success" : "Failure");
                
                string text = testItem.Name + "".PadLeft(70 - Math.Min(testItem.Name.Length, 68), '.');
                if (testItem.Success != null)
                {
                    text += testItem.Success.Value ? "Passed!" : "Failed!";
                }
                
                GUILayout.Label(text, style);
                
                if (!string.IsNullOrEmpty(testItem.Message))
                {
                    GUILayout.Label(testItem.Message, style);
                }
            }
        
        GUILayout.EndVertical();
    }
}
