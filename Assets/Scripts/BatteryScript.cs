using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    [SerializeField]
    private float capacity = 1.0f;
    [SerializeField]
    private bool isRandomCharge = false;
    private AudioSource collectSound;
    private float destroyTimeout;

    void Start()
    {
        collectSound = GetComponent<AudioSource>();
        destroyTimeout = 0f;
    }

    void Update()
    {
        if (destroyTimeout > 0)
        {
            destroyTimeout -= Time.deltaTime;
            if (destroyTimeout <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isRandomCharge) capacity = Random.Range(0.3f, 1.0f);

        if (other.CompareTag("Character"))
        {
            collectSound.Play();
            GameState.TriggerGameEvent("Battery", new GameEvents.MessageEvent
            {
                message = $"Знайдено батарейку з зарядом {capacity:F1}",
                data = capacity
            });
            //Destroy(gameObject);
            destroyTimeout = 0.6f;
        }
    }
}
