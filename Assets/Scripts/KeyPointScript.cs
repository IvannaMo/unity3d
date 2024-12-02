using UnityEngine;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyPointName = "1";
    [SerializeField]
    private float timeout = 5.0f;
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
}
