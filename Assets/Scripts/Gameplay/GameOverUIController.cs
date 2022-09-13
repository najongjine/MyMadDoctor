using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    public static GameOverUIController instance;

    [SerializeField]
    private Canvas gameOverCanvas;

    [SerializeField]
    private Text finalScoreTxt;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void GameOver()
    {
        gameOverCanvas.enabled = true;
        finalScoreTxt.text = "Score: " + GameplayUIController.instance.GetKillsCount();
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.
            SceneManager.LoadScene(UnityEngine.SceneManagement.
            SceneManager.GetActiveScene().name);
    }

}
