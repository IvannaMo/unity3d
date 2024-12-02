using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    private float capacity = 1.0f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Character"))
        {
            FlashlightScript flashlight = collider.transform.Find("Flashlight")?.GetComponent<FlashlightScript>();
            if (flashlight != null)
            {
                flashlight.Charge(capacity);
                Destroy(gameObject);
            }
        }
    }
}
