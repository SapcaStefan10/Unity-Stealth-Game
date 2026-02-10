using UnityEngine;
using TMPro;

public class CollectibleInteractUI : MonoBehaviour
{
    public TMP_Text collectPromptText;
    private bool playerInRange = false;

    void Start()
    {
        if (collectPromptText != null)
            collectPromptText.text = "";
    }

    void Update()
    {
        if (playerInRange)
        {
            if (collectPromptText != null)
                collectPromptText.text = "Apasă E pentru a lua intelul";

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E apăsat. Colectez obiectul.");
                Collect();
            }
        }
        else
        {
            if (collectPromptText != null)
                collectPromptText.text = "";
        }
    }

    void Collect()
    {
        Debug.Log("Intel colectat!");
        if (collectPromptText != null)
            collectPromptText.text = "";  // Șterge textul când iei obiectul
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player a intrat în trigger.");
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player a ieșit din trigger.");
            playerInRange = false;
        }
    }
}
