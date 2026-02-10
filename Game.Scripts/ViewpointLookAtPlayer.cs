using UnityEngine;

public class ViewpointLookAtPlayer : MonoBehaviour
{
    public Transform player; // setează în inspector playerul aici

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // pentru a nu inclina în sus sau jos
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
