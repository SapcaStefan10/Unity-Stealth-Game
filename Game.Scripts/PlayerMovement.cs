using UnityEngine;
using TMPro;  // Folosește TextMeshPro

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sneakSpeed = 2f;
    public Transform playerCamera;

    private CharacterController controller;
    private Vector3 velocity;
    public float gravity = -9.81f;

    public bool isSneaking { get; private set; }
    public bool isMakingNoise { get; private set; }  // 🔊 ADĂUGAT

    private bool canHide = false;
    private bool isHidden = false;
    private HidingSpot currentHidingSpot = null;

    public TMP_Text hidingStatusText;  // TMP_Text în loc de Text

    private int defaultLayer;
    private int hiddenLayer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        UpdateHUD();

        defaultLayer = gameObject.layer;

        hiddenLayer = LayerMask.NameToLayer("HiddenPlayer");
        if (hiddenLayer == -1)
        {
            Debug.LogError("Layer-ul 'HiddenPlayer' nu există! Creează-l în Project Settings -> Tags and Layers.");
            hiddenLayer = defaultLayer; // fallback
        }
    }

    void Update()
    {
        isSneaking = Input.GetKey(KeyCode.LeftShift);
        isMakingNoise = !isSneaking;  // 🔊 ADĂUGAT – dacă NU e sneaking, face zgomot

        float speed = isSneaking ? sneakSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = playerCamera.right;
        right.y = 0;
        right.Normalize();

        Vector3 move = right * x + forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        bool wasHidden = isHidden;
        isHidden = canHide && isSneaking;

        if (isHidden != wasHidden)
        {
            if (isHidden)
            {
                Debug.Log("Player is now hidden! Setting layer to HiddenPlayer");
                gameObject.layer = hiddenLayer;
            }
            else
            {
                Debug.Log("Player is now visible! Setting layer to Default");
                gameObject.layer = defaultLayer;
            }
            UpdateHUD();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HidingSpot spot = other.GetComponent<HidingSpot>();
        if (spot != null)
        {
            canHide = true;
            currentHidingSpot = spot;
            Debug.Log("Entered hiding spot area: " + other.gameObject.name);
            UpdateHUD();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HidingSpot spot = other.GetComponent<HidingSpot>();
        if (spot != null && spot == currentHidingSpot)
        {
            canHide = false;

            if (isHidden)
            {
                isHidden = false;
                gameObject.layer = defaultLayer;
                Debug.Log("Left hiding spot, player now visible");
            }

            currentHidingSpot = null;
            UpdateHUD();
        }
    }

    private void UpdateHUD()
    {
        if (hidingStatusText == null) return;

        if (isHidden)
            hidingStatusText.text = "Ești ascuns";
        else if (canHide)
            hidingStatusText.text = "Ține Shift pentru a te ascunde";
        else
            hidingStatusText.text = "";
    }
}
