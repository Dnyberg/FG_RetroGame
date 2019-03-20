using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController SharedInstance;

    public Text DistanceLabel;
    public Text GameOverLabel;
    public Button RestartGameButton;

    public GameObject Player;
    public GameObject Enemy;

    public float LoseDistance = 200;
    public string LevelToLoad;


    private int IntCurrentDistance = 0;
    private float CurrentDistance = 0;


    void Awake()
    {
        SharedInstance = this;
    }

    //public void IncrementScore(int Increment)
    //{
    //    CurrentScore += Increment;
    //    ScoreLabel.text = "Score: " + CurrentScore;
    //}

    public void ShowGameOver()
    {
        Time.timeScale = 0;
        GameOverLabel.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
        RestartGameButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -50, 0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

    void UpdateDistance()
    {
        Vector3 StageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        CurrentDistance = Enemy.transform.position.x - Player.transform.position.x;

        if (Enemy.transform.position.x > StageDimensions.x)
        {
            DistanceLabel.enabled = true;        
            IntCurrentDistance = Mathf.RoundToInt(CurrentDistance);
            DistanceLabel.text = "Distance: " + IntCurrentDistance;
        }   

        else
        {
            DistanceLabel.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance();

        if (Input.GetKeyDown(KeyCode.Escape))
            ShowGameOver();
    }
}
