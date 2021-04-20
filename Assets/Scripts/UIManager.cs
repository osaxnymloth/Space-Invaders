using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text gameOver_Text;
    [SerializeField]
    private Text winText;
    [SerializeField]
    private Text restartText;
    private GameManager gameManager;
    private Player player;

    void Start()
    {
        // make sure the UI elements are set to correct values & deactivate game over objects
        scoreText.text = "Score: ";
        winText.gameObject.SetActive(false);
        gameOver_Text.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        livesText.text = "Lives: " + player.playerLives;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gameManager == null)
            Debug.Log("UIManager: GameManager is null!");
        if (player == null)
            Debug.Log("UIManager: Player is null!");
    }

    //update score function
    public void UpdateScore(int playerScore) => scoreText.text = "Score: " + playerScore.ToString();


    //update number of lives left for the player
    public void UpdateLives(int currentLives) => livesText.text = "Lives: " + currentLives.ToString();


    // if game is over, activate the necessary UI elements and start flashing restart text
    public void GameOver()
    {
        gameManager.GameOver();
        gameOver_Text.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        Destroy(player.gameObject); //crude way to make it impossible for player to continue playing/moving after game is over
        StartCoroutine(RestartGameText());
    }

    public void GameWinText()
    {
        winText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(RestartGameText());
    }

    //flicker the restart text to focus attention of the player on it. 
    IEnumerator RestartGameText()
    {
        while (true)
        {
            restartText.text = "Try again? (R)";
            yield return new WaitForSeconds(0.5f);
            restartText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}


