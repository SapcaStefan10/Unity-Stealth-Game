using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound; // opțional, sunet la colectare
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && collectSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = collectSound;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            // Aici poți crește scorul sau orice altă logică
            Debug.Log("Colectabil luat!");

            // Distruge obiectul după un mic delay ca să se audă sunetul
            Destroy(gameObject, collectSound != null ? collectSound.length : 0f);
        }
    }
}
