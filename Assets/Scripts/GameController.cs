using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Player player;
    public GameObject moleContainer;
    public TextMesh infoText;
    public float spawnDuration = 1.5f;
    public float spawnDecrement = 0.1f;
    public float minimumSpawnDuration = 0.5f;
    public float gameTimer = 30f;
    private float resetTimer = 3f;
    private float startDelay = 3.1f;
    public bool gameStarted = false;
    public GameObject menuPanel;
    public GameObject gameOverPanel;


    bool soundPressed;
    public Sprite soundEnabledSprite, soundDisabledSprite;
    public UnityEngine.UI.Button soundButton;

    private Mole[] moles;
    private float spawnTimer = 0f;

    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    public void StartGame() {
        infoText.gameObject.SetActive(true);
        menuPanel.SetActive(false);
        gameStarted = true;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
        public void soundPress()
    {
        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            soundPressed = false;
            PlayerPrefs.SetInt("Muted", 0);
            soundButton.image.sprite = soundDisabledSprite;


            AudioListener.pause = true;
        }
        else if (PlayerPrefs.GetInt("Muted") == 0)
        {
            soundPressed = true;

            PlayerPrefs.SetInt("Muted", 1);
            soundButton.image.sprite = soundEnabledSprite;
            AudioListener.pause = false;

        }

    }
    void Start()
    {
        gameOverPanel.SetActive(false);

        if (!PlayerPrefs.HasKey("Muted")) {
            PlayerPrefs.SetInt("Muted", 1);
            soundButton.image.sprite = soundEnabledSprite;
            AudioListener.pause = false;

        }

        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            soundButton.image.sprite = soundEnabledSprite;
            AudioListener.pause = false;

        }

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            soundButton.image.sprite = soundDisabledSprite;
            AudioListener.pause = true;

        }

        //highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        soundPressed = false;

        infoText.gameObject.SetActive(false);
        moles = moleContainer.GetComponentsInChildren<Mole>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (gameStarted)
        {
            startDelay -= Time.deltaTime;
            if (startDelay > 0f)
            {
                infoText.text = "Round starting in...\nTime: " + Mathf.Floor(startDelay);
            }
            else if (startDelay <= 0f)
            {
                gameTimer -= Time.deltaTime;
                if (gameTimer > 0f)
                {
                    infoText.text = "Hit all the moles!\nTime: " + Mathf.Floor(gameTimer) + "\nScore " + player.score;

                    spawnTimer -= Time.deltaTime;
                    if (spawnTimer <= 0f)
                    {
                        moles[Random.Range(0, moles.Length)].Rise();

                        spawnDuration -= spawnDecrement;
                        if (spawnDuration < minimumSpawnDuration)
                        {
                            spawnDuration = minimumSpawnDuration;
                        }
                        spawnTimer = spawnDuration;
                    }
                }
                else
                {
                    infoText.text = "Game over! Your score:\n" + Mathf.Floor(player.score);

                    resetTimer -= Time.deltaTime;
                    if (resetTimer <= 0f)
                    {
                        gameOverPanel.SetActive(true);
                    }
                }
            }
        }
    }
}
