using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastScript : MonoBehaviour
{
    private static ToastScript instance;
    private TMPro.TextMeshProUGUI toastTMP;
    private float timeout = 5.0f;
    private float timeLeft;
    private GameObject content;
    private readonly Queue<ToastMessage> messages = new Queue<ToastMessage>();

    public static void ShowToast(string message, string author = null, float? timeout = null)
    {
        foreach (ToastMessage m in instance.messages)
        {
            if (m.message == message && m.author == author)
            {
                return;
            }
        }

        instance.messages.Enqueue(new ToastMessage
        {
            message = message,
            author = author,
            timeout = timeout ?? instance.timeout
        });
    }

    private void OnGameEvent(string eventName, object data)
    {
        if (data is GameEvents.INotifier n)
        {
            ShowToast(n.message, n.author);
        }
    }

    void Start()
    {
        instance = this;
        content = transform.Find("Content").gameObject;
        toastTMP = transform.Find("Content/TextBox/Background/ToastTMP").GetComponent<TMPro.TextMeshProUGUI>();
        content.SetActive(false);
        GameState.Subscribe(OnGameEvent);
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                messages.Dequeue();
                content.SetActive(false);
            }
        }
        else
        {
            if (messages.Count > 0)
            {
                var m = messages.Peek();

                string displayMessage = m.message;
                if (!string.IsNullOrEmpty(m.author)) 
                {
                    displayMessage = $"{m.author}: {displayMessage}";
                }

                toastTMP.text = displayMessage;
                timeLeft = m.timeout;
                content.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameEvent);
    }

    private class ToastMessage
    {
        public string message { get; set; }
        public float timeout { get; set; }
        public string author { get; set; }
    }
}
