using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    //public GameObject nextLevelUI;
    public GameObject gameOverUI;
    bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        //Infected.PlayerSpotted +=
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void showGameOverUI()
    {
        this.gameObject.SetActive(true);
        gameOverUI.SetActive(true);
        isGameOver = true;
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(0);
    
    }

    //void 

}
