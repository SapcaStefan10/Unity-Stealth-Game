using UnityEngine;

public class SimpleSecurityCamera : MonoBehaviour
{
    public Transform viewPoint;          // Punctul din fața camerei
    public Transform player;             // Referința către player
    public float rotationSpeed = 30f;    // Viteza de rotație în grade/secundă
    public float maxRotationAngle = 45f; // Unghiul maxim în ambele părți
    public float viewAngle = 60f;        // Câmpul vizual al camerei
    public float viewDistance = 10f;     // Distanța maximă de detectare
    public LayerMask obstacleMask;       // Layer-ele care blochează vederea

    private float currentAngle = 0f;
    private int rotationDirection = 1;

    void Update()
    {
        RotateCamera();
        DetectPlayer();
    }

    void RotateCamera()
    {
        currentAngle += rotationSpeed * rotationDirection * Time.deltaTime;

        if (currentAngle > maxRotationAngle)
        {
            currentAngle = maxRotationAngle;
            rotationDirection = -1;
        }
        else if (currentAngle < -maxRotationAngle)
        {
            currentAngle = -maxRotationAngle;
            rotationDirection = 1;
        }

        transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - viewPoint.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > viewDistance)
            return;

        float angleToPlayer = Vector3.Angle(viewPoint.forward, directionToPlayer);
        if (angleToPlayer > viewAngle / 2f)
            return;

        if (Physics.Raycast(viewPoint.position, directionToPlayer.normalized, distanceToPlayer, obstacleMask))
            return;

        // Dacă ajunge aici, playerul este detectat
        GameOverManager gm = GameObject.FindObjectOfType<GameOverManager>();
        if (gm != null)
            gm.GameOver();
    }
}
