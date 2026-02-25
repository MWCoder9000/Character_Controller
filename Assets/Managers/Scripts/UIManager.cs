using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreValue;
    [SerializeField] TextMeshProUGUI timeValue;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject HUD;
    [SerializeField] TextMeshProUGUI endScoreValue;


    void Start()
    {
        UpdateScoreUI(0);
        UpdateTimeUI(0);
    }

    public void NEWSTART()
    {
        UpdateScoreUI(0);
        UpdateTimeUI(0);
    }
    public void UpdateScoreUI(int value)
    {
        // "D5" - minimum of 5 digits, preceding shorter numbers with 0s
        scoreValue.text = value.ToString("D5");
    }

    public void UpdateTimeUI(float time)
    {
        int seconds = (int)time;
        timeValue.text = System.TimeSpan.FromSeconds(seconds).ToString("hh':'mm':'ss");
    }

    public void ActivateEndGame(int score)
    {
        GameManager.Instance.Dead = true;
        Time.timeScale = 0f;
        endScoreValue.text = "Scored: " + score.ToString();
        HUD.SetActive(false);
        gameOverPanel.SetActive(true);
        Cursor.visible = true;
    }
}