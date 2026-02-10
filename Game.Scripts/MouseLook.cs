using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Sensitivity")]
    public float mouseSensitivity = 100f;

    [Header("References")]
    public Transform playerBody;  // Obiectul care se rotește pe axa Y (de ex. PlayerBody)

    private float xRotation = 0f;

    void Start()
    {
        // Blocăm cursorul în mijlocul ecranului și îl ascundem
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Preluăm mișcarea mouse-ului
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotirea pe axa X (sus-jos), limitată la ±90 grade
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplicăm rotația doar pe cameră (sus-jos)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotim playerBody pe axa Y (stânga-dreapta)
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            Debug.LogWarning("playerBody nu este setat în inspector!");
        }
    }
}
