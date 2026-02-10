using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;      // UI-ul de Game Over (ex: "YOU GOT CAUGHT")
    public AudioSource alarmAudio;     // Referință către componenta AudioSource cu sunetul de alarmă

    void Start()
    {
        // La început ascunde UI-ul de Game Over
        gameOverUI.SetActive(false);

        // Oprește sunetul de alarmă în caz că este pornit
        if (alarmAudio != null)
            alarmAudio.Stop();

        // Asigură că timpul jocului e normal (nu oprit)
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        // Arată UI-ul de Game Over
        gameOverUI.SetActive(true);

        // Pornește sunetul de alarmă dacă nu e deja pornit
        if (alarmAudio != null && !alarmAudio.isPlaying)
            alarmAudio.Play();

        // Oprește timpul jocului (pauză)
        Time.timeScale = 0f;
    }
}
