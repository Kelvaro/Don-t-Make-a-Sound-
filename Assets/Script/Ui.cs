using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    //public GameObject nextLevelUI;
    public GameObject gameOverUI;
    public GameObject nextLevelUi;
    

    public static int currentLevel = 0;

    public string[] levels;
    // Start is called before the first frame update

    public void Start()
    {
        int sceneID;
        sceneID = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneID);
        SceneManager.GetSceneByName("Testing");
    }

    public void showGameOverUI()
    {
        //this.gameObject.SetActive(true);
        gameOverUI.SetActive(true);
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(currentLevel);  
    }

    public void toMenu()
    {
        currentLevel = 0;  
    }

    public void showNextLevelUi()
    {
        nextLevelUi.SetActive(true);
    }
    public void nextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }

}
