using UnityEngine;

public class GameEvents
{
    public interface INotifier 
    { 
        string message { get; }
        string author { get; }
    }

    public class MessageEvent: INotifier
    {
        public string message { get; set; }
        public string author { get; set; }
        public object data { get; set; }
    }
}
