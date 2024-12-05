using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    private Transform parentTransform;
    private Light flashlight;
    private float charge;
    private float workTime = 2.5f;

    public float chargeLevel => Mathf.Clamp01(charge);

    void Start()
    {
        parentTransform = transform.parent;
        if (parentTransform == null )
        {
            Debug.LogError("FlashlightScript: parentTransform not found");
        }
        flashlight = GetComponent<Light>();
        charge = 1.0f;
        GameState.Subscribe(OnBatteryEvent, "Battery");
    }

    void Update()
    {
        if (parentTransform == null) return;

        if (charge > 0 && !GameState.isDay)
        {
            flashlight.intensity = chargeLevel;
            charge -= Time.deltaTime / workTime;
        }

        if (GameState.isFpv)
        {
            transform.forward = Camera.main.transform.forward;
        }
        else
        {
            Vector3 f = Camera.main.transform.forward;
            f.y = 0.0f;
            if (f == Vector3.zero) f = Camera.main.transform.up;
            transform.forward = f.normalized;
        }
    }

    private void OnBatteryEvent(string eventName, object data)
    {
        if (data is GameEvents.MessageEvent e)
        {
            charge += (float)e.data;
        }
    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnBatteryEvent, "Battery");
    }

    //public void Charge(float amount)
    //{
    //    charge = Mathf.Clamp(charge + amount, 0.0f, 1.0f);
    //}
}
