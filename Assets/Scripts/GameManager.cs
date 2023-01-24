using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 2f;

    public bool isGameActive;
    public int score = 0;
    public int lives = 3;
    public bool isClickedOnBad = false;
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI pausedText;
    public GameObject restartButton;
    public GameObject titleScreen;
    public GameObject pauseCanvas;
    public Slider slider;
    public Button resumeButton;
    public Button pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;

        titleScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
        UpdateLives();
    }

    IEnumerator SpawnTargets()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    void UpdateLives()
    {
        if(lives >= 0)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    public void GameOver()
    {
        if (lives < 1 || isClickedOnBad)
        {
            isGameActive = false;
            gameOverText.gameObject.SetActive(true);
            restartButton.SetActive(true);
            pauseCanvas.SetActive(false);

        }
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        slider.gameObject.SetActive(false);
        pauseCanvas.SetActive(true);
        spawnRate /= difficulty;

        StartCoroutine(SpawnTargets());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausedText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausedText.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        isGameActive = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
