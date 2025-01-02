using System.Collections;
using System.Collections.Generic;
using System.IO; 
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

namespace RainbowJump.Scripts
{
    public class Manager : MonoBehaviour
    {
        public Player playerMovement;
        public TrailRenderer playerTrail;
        public Spawner spawner;
        public GameObject mainCamera;

        public Transform playerTransform;
        public Text scoreText;
        public Text highScoreText;
        public TextMeshProUGUI currentScoreText; // Reference for the current game score display
        public TextMeshProUGUI clickCountText; // Reference for the click count display

        public ScoreboardManager scoreboardManager;

        public float score = 0f;
        private float highScore = 0f;

        public bool gameOver = false;

        public SpriteRenderer playerSprite;

        public GameObject playerScore;
        public GameObject deathParticle;
        public GameObject gameOverUI;
        public GameObject tapToPlayUI;
        public GameObject tapToStartBtn;

        public AudioClip tapSound;
        public AudioClip deathSound;
        public AudioClip buttonSound;
        private AudioSource audioSource;

        void Start()
        {
            Application.targetFrameRate = 144;

            playerMovement.enabled = false;
            playerMovement.rb.simulated = false;

            audioSource = GetComponent<AudioSource>();

            highScore = PlayerPrefs.GetFloat("HighScore", 0f);
            highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore).ToString();

            if (scoreboardManager != null)
            {
                scoreboardManager.LoadScoreboard();
            }
        }

        void Update()
        {
            if (gameOver)
            {
                HandleGameOver();
            }

            if (playerTransform.position.y > score)
            {
                score = playerTransform.position.y;
            }

            scoreText.text = Mathf.FloorToInt(score).ToString();
        }

        void HandleGameOver()
        {
            playerMovement.enabled = false;
            playerMovement.rb.simulated = false;
            playerScore.SetActive(false);
            playerSprite.enabled = false;
            tapToStartBtn.SetActive(false);
            gameOverUI.SetActive(true);
            deathParticle.SetActive(true);
            gameOver = false;

            int clickCount = 0;

            if (FindObjectOfType<ClickTracker>() != null)
            {
                clickCount = FindObjectOfType<ClickTracker>().GetLeftClickCount();
                clickCountText.text = "Clicks This Game: " + clickCount.ToString();
                FindObjectOfType<ClickTracker>().EndGame();
            }

            // Display the player's score for the game
            currentScoreText.text = "Score This Game: " + Mathf.FloorToInt(score).ToString();

            SaveGameData(score, clickCount);

            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetFloat("HighScore", highScore);
                highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore).ToString();
            }
        }

        void SaveGameData(float score, int clickCount)
        {
            string playerName = "Player";

            GameData gameData = new GameData(playerName, Mathf.FloorToInt(score), clickCount);

            string fileName = "GameData_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            string jsonData = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(filePath, jsonData);
            Debug.Log("Game data saved to: " + filePath);

            if (scoreboardManager != null)
            {
                scoreboardManager.AddGameData(gameData);

                ScoreboardDisplay scoreboardDisplay = FindObjectOfType<ScoreboardDisplay>();
                if (scoreboardDisplay != null)
                {
                    scoreboardDisplay.UpdateScoreboardUI();
                }
            }
        }

        public void TapToStart()
        {
            PlayTapSound();
            playerMovement.rb.simulated = true;
            playerMovement.rb.velocity = Vector2.up * playerMovement.jumpForce;
            playerScore.SetActive(true);
            tapToPlayUI.SetActive(false);
            tapToStartBtn.SetActive(false);
            playerMovement.enabled = true;

            if (FindObjectOfType<ClickTracker>() != null)
            {
                FindObjectOfType<ClickTracker>().ResetClickCount();
            }
        }

        public void RestartGame()
        {
            tapToPlayUI.SetActive(true);
            tapToStartBtn.SetActive(true);
            playerSprite.enabled = true;
            deathParticle.SetActive(false);
            gameOverUI.SetActive(false);

            playerMovement.rb.simulated = false;
            playerMovement.transform.position = new Vector3(0f, -3f, 0f);
            mainCamera.transform.position = new Vector3(0f, 0f, -10f);

            spawner.DestroyAllObstacles();
            spawner.InitializeObstacles();
            score = 0f;

            playerTrail.Clear();

            if (FindObjectOfType<ClickTracker>() != null)
            {
                FindObjectOfType<ClickTracker>().ResetClickCount();
            }
        }

        public void PlayTapSound()
        {
            audioSource.PlayOneShot(tapSound);
        }

        public void PlayDeathSound()
        {
            audioSource.PlayOneShot(deathSound);
        }

        public void PlayButtonSound()
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }
}
