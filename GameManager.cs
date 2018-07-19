using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    [SerializeField]
    GameObject gameOverScreen;
    [SerializeField]
    Text killsText;
    [SerializeField]
    Text timeText;

    [SerializeField]
    GameObject gameWonScreen;
    [SerializeField]
    Text winKillsText;
    [SerializeField]
    Text winTimeText;

    public int kills = 0;
    float startTime;

    void Awake()
    {
        startTime = Time.time;
        Time.timeScale = 1f;
    }

	public void GameOver()
	{

        int timePlayed = Mathf.RoundToInt(Time.time - startTime);

        if (timePlayed < 60)
            timeText.text = "Time Played: " + timePlayed + " Seconds";
        else
            timeText.text = "Time Played: " + timePlayed / 60 + " Minutes";

        killsText.text = "Enemies Killed: " + kills;

        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
		print ("Game over");

	}

    public void GameWon()
    {
        int timePlayed = Mathf.RoundToInt(Time.time - startTime);

        if (timePlayed < 60)
            winTimeText.text = "Time Played: " + timePlayed + " Seconds";
        else
            winTimeText.text = "Time Played: " + timePlayed / 60 + " Minutes";

        winKillsText.text = "Enemies Killed: " + kills;

        gameWonScreen.SetActive(true);
        Time.timeScale = 0f;
        print("Game won");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}