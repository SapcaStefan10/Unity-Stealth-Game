using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public TMP_Text messageText;

    private bool triggered = false;

    void Start()
    {
        if (messageText != null)
            messageText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            if (messageText != null)
                messageText.text = "You got through!";

            Time.timeScale = 0f;  // oprește jocul

            Debug.Log("Nivel terminat! Apasă R pentru restart.");
        }
    }

    void Update()
    {
        if (triggered && Time.timeScale == 0f && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
