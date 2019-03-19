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

    private bool ShowDistance = true;
    private int IntCurrentDistance = 0;
    private float CurrentDistance = 0;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //public void IncrementScore(int Increment)
    //{
    //    CurrentScore += Increment;
    //    ScoreLabel.text = "Score: " + CurrentScore;
    //}

    public void ShowGameOver()
    {
        GameOverLabel.rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
        RestartGameButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -50, 0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void DisplayDistance()
    {
        Vector3 StageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if (Enemy.transform.position.x > StageDimensions.x)
        {
            DistanceLabel.enabled = true;
            CurrentDistance = Enemy.transform.position.x - Player.transform.position.x;
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
        DisplayDistance();
    }
}
