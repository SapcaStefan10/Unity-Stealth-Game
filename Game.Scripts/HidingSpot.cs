using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public bool playerIsInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
            Debug.Log("Player in hiding spot!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            Debug.Log("Player left hiding spot!");
        }
    }
}
