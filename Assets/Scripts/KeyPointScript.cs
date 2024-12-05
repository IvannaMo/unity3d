using UnityEngine;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyPointName = "1";
    [SerializeField]
    private float timeout = 2.0f;
    private float timeLeft;

    public float part;

    void Start()
    {
        timeLeft = timeout;
        part = 1.0f;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
            part = timeLeft / timeout;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            GameState.collectedItems.Add("Key" + keyPointName, part);
            GameState.TriggerGameEvent("KeyPoint", new GameEvents.MessageEvent
            {
                message = "Знайдено ключ " + keyPointName,
                data = part
            });
            Destroy(gameObject);
        }
    }
}
