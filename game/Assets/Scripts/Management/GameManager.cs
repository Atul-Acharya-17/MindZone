using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EmotivUnityPlugin;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Score of the player
    /// </summary>
    private int score = 0;

    /// <summary>
    /// Text UI of the score
    /// </summary>
    public Text scoreText;

    /// <summary>
    /// Player health
    /// </summary>
    private float health = 100.0f;

    /// <summary>
    /// Maximum health of the player
    /// </summary>
    private const float maxHealth = 100.0f;

    /// <summary>
    /// Health Bar
    /// </summary>
    public Image healthBar;

    public bool locker;

    private float frames = 0;
    private float attentionFrames = 0;

    public Button attentionButton;
    public Button neutralButton;


    /// <summary> 
    /// Start is called before the first frame update
    /// Initialises the text of the Text UI
    /// </summary>
    void Start()
    {
        locker = false;
        //List<string> dataStreamList = new List<string>();
        //dataStreamList.Add(DataStreamName.MentalCommands);
        //dataStreamList.Add(DataStreamName.SysEvents);

        //CortexFacade.StartDataStream(dataStreamList);
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Update is called once per frame
    /// Calls the GameOver method if the game is over
    /// </summary
    void Update()
    {

        if (IsGameOver())
        {
            GameOver();
        }

        else
        {
            frames += 1;
            string action = CortexFacade.GetAction();
            if (action != "neutral")
            {
                attentionFrames += 1;
                IncreaseHealth(0.1f);
                attentionButton.GetComponent<Image>().color = new Color(0f, 200f / 255f, 0f, 175/255f);
                neutralButton.GetComponent<Image>().color = new Color(106f / 255f, 106f / 255f, 106f / 255f, 175 / 255f);
                // Update button color
            }

            else
            {
                attentionButton.GetComponent<Image>().color = new Color(106f / 255f, 106f / 255f, 106f / 255f, 175 / 255f);
                neutralButton.GetComponent<Image>().color = new Color(200f / 255f, 0f, 0f, 175 / 255f);
                // Update button color
            }
        }
    }
    /// <summary>
    /// Increases the score an updates the text UI
    /// </summary>
    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Decreases the health of the player and modifies the health bar
    /// </summary>
    /// <param name="damage"> Damage received by the player.</param>
    public void DecreaseHealth(float damage)
    {
        health -= damage;
        float ratio = health/ maxHealth;
        healthBar.fillAmount = ratio;
        healthBar.color = new Color((1-ratio), (ratio), 0);
        if (health <= 0.0f)
        {
            GameOver();
        }
    }

    public void IncreaseHealth(float increment)
    {

    }

    /// <summary>
    /// Invokes LoadMenu in 5 seconds
    /// </summary>
    public void GameOver()
    {
        if (!locker)
        {
            string url = "http://localhost:8000/players";
            Debug.Log(attentionFrames / frames);
            Player player = new Player(CortexFacade.User, score, (attentionFrames / frames) * 100 );
            string playerJSON = player.Stringify();
            StartCoroutine(ServerController.Post(url, playerJSON,
                result => {
                    Debug.Log(result);
                }));
            CortexFacade.Stop();
            scoreText.text = "Final Score: " + score.ToString();
            Invoke("LoadMenu", 5);
            locker = true;
        }
        
    }

    /// <summary>
    /// Checks if the game is over
    /// </summary>
    ///<returns>
    /// Returns true if the game is over. Otherwise returns false
    ///</returns>
    public bool IsGameOver()
    {
        return health <= 0.0f;
    }

    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
