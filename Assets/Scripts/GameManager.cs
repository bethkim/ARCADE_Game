using System.Collections;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;
    public bool isGameActive = false;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject startMenu;
    public GameObject gameOverMenu;
    
    // NUEVO: Variable para el texto de la puntuación final en la pantalla de Game Over
    public TextMeshProUGUI finalScoreText; 

    [Header("Audio")]
    public AudioSource audioSourceUI;
    public AudioClip sonidoStart;
    public AudioClip sonidoRestart;

    public void StartGame()
    {
        if (audioSourceUI != null && sonidoStart != null)
        {
            audioSourceUI.PlayOneShot(sonidoStart);
        }
        
        isGameActive = true;
        score = 0;
        lives = 3;
        UpdateScore(0);
        UpdateLives(0);
        
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    public void UpdateScore(int points)
    {
        if (!isGameActive) return;
        score += points;
        scoreText.text = "Puntos: " + score;
    }

    public void LoseLife()
    {
        if (!isGameActive) return;
        lives--;
        UpdateLives(0);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateLives(int dummy)
    {
        livesText.text = "Vidas: " + lives;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverMenu.SetActive(true);
        
        // NUEVO: Al perder, actualizamos el texto final con los puntos que lograste
        if (finalScoreText != null)
        {
            finalScoreText.text = "Puntuación Final: " + score;
        }
    }

    public void RestartGame()
    {
        StartCoroutine(ReiniciarConRetraso());
    }

    IEnumerator ReiniciarConRetraso()
    {
        if (audioSourceUI != null && sonidoRestart != null)
        {
            audioSourceUI.PlayOneShot(sonidoRestart);
        }
        
        yield return new WaitForSeconds(0.4f); 
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
