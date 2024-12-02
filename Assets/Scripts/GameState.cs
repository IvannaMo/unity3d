using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool isDay { get; set; }
    public static bool isFpv { get; set; }

    #region Game events
    
    public static void TriggerGameEvent()
    {

    }

    private static Dictionary<string, List<Action<String>>> subscribers = new();
    public static void Subscribe(Action action, String eventName)
    {

    }
    public static void UnSubscribe(Action action, String eventName) 
    {
        
    }

    #endregion
}
