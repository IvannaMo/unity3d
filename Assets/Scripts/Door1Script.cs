using System.Linq;
using UnityEngine;

public class Door1Script : MonoBehaviour
{
    [SerializeField]
    private string requiredKey = "1";
    private float openingTime = 3.0f;
    private float timeout;
    private AudioSource closedSound;
    private AudioSource openedSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            if (GameState.collectedItems.ContainsKey("Key1"))
            {
                GameState.TriggerGameEvent("Door1",
                    new GameEvents.MessageEvent
                    {
                        message = "Двері відчиняються",
                        author = "Двері 1",
                        data = requiredKey
                    });
                timeout = openingTime;
                openedSound.Play();
            }
            else
            {
                GameState.TriggerGameEvent("Door1", new GameEvents.MessageEvent
                {
                    message = "Для відкривання двері вам необхідно знайти ключ " + requiredKey,
                    data = requiredKey
                });
                closedSound.Play();
            }
        }
    }

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        closedSound = audioSources[0];
        openedSound = audioSources[1];
    }

    void Update()
    {
        if (timeout > 0f)
        {
            transform.Translate(Time.deltaTime / openingTime, 0, 0);
            timeout -= Time.deltaTime;
        }
    }
}
