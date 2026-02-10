using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform player;
    public float viewDistance = 10f;
    public float viewAngle = 45f;

    public float hearingDistance = 5f;  // distanța la care inamicul te poate auzi

    public GameOverManager gameOverManager;
    public LayerMask obstacleMask;

    private PlayerMovement playerMovement;

    void Start()
    {
        // Obținem componenta PlayerMovement de pe jucător
        playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement nu a fost găsit pe obiectul player!");
        }
    }

    void Update()
    {
        if (PlayerInSight() || PlayerHeard())
        {
            gameOverManager.GameOver();
        }
    }

    bool PlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Verificăm dacă playerul e în distanța de vizibilitate
        if (distanceToPlayer > viewDistance)
            return false;

        // Verificăm dacă playerul e în unghiul de vizibilitate
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle > viewAngle)
            return false;

        // Raycast ca să vedem dacă ceva blochează vederea
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, viewDistance))
        {
            if (hit.transform == player)
            {
                return true; // te-a văzut
            }
        }

        return false;
    }

    bool PlayerHeard()
    {
        if (!playerMovement.isMakingNoise)
            return false;

        float distance = Vector3.Distance(transform.position, player.position);

        // dacă face zgomot și e aproape
        if (distance <= hearingDistance)
        {
            // Opțional: adaugă Raycast dacă vrei ca pereții să blocheze sunetul
            return true; // te-a auzit
        }

        return false;
    }
}
