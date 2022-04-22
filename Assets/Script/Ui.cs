using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    //public GameObject nextLevelUI;
    public GameObject gameOverUI;
    public GameObject nextLevelUi;
    public GameObject finishedUi;


    public static int currentLevel = 0;
    const int maxs = 6;

    public string[] levels;
    // Start is called before the first frame update

    public void Start()
    {
        int sceneID;
        sceneID = SceneManager.GetActiveScene().buildIndex;
        currentLevel = sceneID;
        Debug.Log(sceneID);
        levels = new string[maxs] { "Menu", "Level1", "Level2", "Level3", "Level4", "Level5" };
    }

    public void showGameOverUI()
    {
        this.gameObject.SetActive(true);
        gameOverUI.SetActive(true);
    }

    public void restartLevel()
    {
        Debug.Log("Key clicked that calls method restartLevel()");
        SceneManager.LoadScene(levels[currentLevel]);
    }

    public void toMenu()
    {
        currentLevel = 0;
        SceneManager.LoadScene(0);
    }

    public void showNextLevelUi()
    {

            nextLevelUi.SetActive(true);

    }
    public void nextLevel()
    {
        Debug.Log("Key clicked that calls method nextLevel()");
        currentLevel++;
        if (currentLevel != 6)
        {
            SceneManager.LoadScene(levels[currentLevel]);
        }
        else 
        {
            finishedUi.SetActive(true);
        }
     
        Debug.Log("Current Level is: " + currentLevel);
    }

    public void quitGame()
    {
        Application.Quit();

    }



}
