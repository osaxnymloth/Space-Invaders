using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;
    [SerializeField]
    private GameObject enemyContainer;
    private UIManager uIManager;

    private void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (uIManager == null)
            Debug.Log("GameManager: uIManager is empty!");
    }

    void Update()
    {
        CheckForChildren();
        // if player hits R key, restart game (at any point)
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("Main");

        // exit game if ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        // if gameover, tell UIManager to display proper interface
        if (isGameOver == true)
        {
            Debug.Log("game is over!");
            uIManager.GameOver();
        }
    }

    public void GameOver() => isGameOver = true;

    // check for children in enemyContainer. No children means no enemies thus player wins. Need to check outside of Enemy script. 
    public void CheckForChildren()
    {
        //Debug.Log(enemyContainer.name + " has " + enemyContainer.transform.childCount + " children");
        if (enemyContainer.transform.childCount == 0)
        {
            uIManager.GameWinText();
        }

    }
}
