using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    private Transform character;
    private InputAction lookAction;
    private Vector3 cameraAngles, cameraAngles0;
    private Vector3 r;
    private float sensitivityH = 6.0f;
    private float sensitivityV = -4.0f;

    private float minFpvVerticalAngle = -10.0f;
    private float maxFpvVerticalAngle = 40.0f;
    private float minTpvVerticalAngle = 35.0f;
    private float maxTpvVerticalAngle = 75.0f;

    private float minFpvDistance = 0.9f;
    private float maxFpvDistance = 9.0f;

    private bool isPos3;
    private bool isCameraActive;


    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        cameraAngles0 = cameraAngles = this.transform.eulerAngles;
        character = GameObject.Find("Character").transform;
        r = this.transform.position - character.position;
        isPos3 = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCameraActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCameraActive = !isCameraActive;
            Cursor.lockState = isCameraActive ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isCameraActive;
        }

        Vector2 wheel = Input.mouseScrollDelta;
        if (wheel.y != 0)
        {
            if (r.magnitude >= maxFpvDistance)
            {
                isPos3 = true;
                if (wheel.y > 0)
                {
                    r *= (1 - wheel.y / 10);
                }
            }
            else
            {
                isPos3 = false;
                if (r.magnitude >= minFpvDistance)
                {
                    float rr = r.magnitude * (1 - wheel.y / 10);
                    if (rr <= minFpvDistance)
                    {
                        r *= 0.01f;
                        GameState.isFpv = true;
                    }
                    else
                    {
                        r *= (1 - wheel.y / 10);
                    }
                }
                else
                {
                    if (wheel.y < 0)
                    {
                        r *= 100f;
                        GameState.isFpv = false;
                    }
                }
            }
        }

        if (!isPos3)
        {
            Vector2 lookValue = lookAction.ReadValue<Vector2>();
            if (lookValue != Vector2.zero)
            {
                float minVerticalAngle = r.magnitude < minFpvDistance ? minFpvVerticalAngle : minTpvVerticalAngle;
                float maxVerticalAngle = r.magnitude < minFpvDistance ? maxFpvVerticalAngle : maxTpvVerticalAngle;

                cameraAngles.x += lookValue.y * Time.deltaTime * sensitivityV;
                cameraAngles.y += lookValue.x * Time.deltaTime * sensitivityH;

                cameraAngles.x = Mathf.Clamp(cameraAngles.x, minVerticalAngle, maxVerticalAngle);

                this.transform.eulerAngles = cameraAngles;
            }
            this.transform.position = character.position +
                Quaternion.Euler(
                    cameraAngles.x - cameraAngles0.x,
                    cameraAngles.y - cameraAngles0.y,
                    0
                ) * r;
        }
    }
}