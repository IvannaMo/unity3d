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

    public static void ShowToast(string message, float? timeout = null)
    {
        if (instance.messages.Count > 0 && instance.messages.Peek().message == message)
        {
            return;
        }

        instance.messages.Enqueue(new ToastMessage 
        {
            message = message,
            timeout = timeout ?? instance.timeout
        });
    }

    void Start()
    {
        instance = this;
        content = transform.Find("Content").gameObject;
        toastTMP = transform.Find("Content/ToastTMP").GetComponent<TMPro.TextMeshProUGUI>();
        content.SetActive(false);
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
                toastTMP.text = m.message;
                timeLeft = m.timeout;
                content.SetActive(true);
            }
        }
    }

    private class ToastMessage
    {
        public string message { get; set; }
        public float timeout { get; set; }
    }
}
