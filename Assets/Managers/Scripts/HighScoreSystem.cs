using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreSystem : MonoBehaviour
{
    List<PlayerScore> highScoreList = new List<PlayerScore>();

    [SerializeField] List<TextMeshProUGUI> highPlayersTMP = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> highScoresTMP = new List<TextMeshProUGUI>();

    public bool CheckHighScore(string name, int score)
    {
        bool newHighScore;
        //PlayerPrefs.DeleteAll();
        DebugLogHighScores();
        LoadHighScores();
        newHighScore = AddHighScore(name, score);
        SortHighScores();
        SaveHighScores();
        DebugLogHighScores();
        return newHighScore;
    }

    void LoadHighScores()
    {
        highScoreList.Clear();

        for (int i = 0; i < 5; i++)
        {
            highScoreList.Add(new PlayerScore(
            PlayerPrefs.GetString("HighPlayer" + i, ""),
            PlayerPrefs.GetInt("HighScore" + i, 0)));

            //Debug.Log("LOAD: [" + i + "] " + highScoreList[i].Name + " | " + highScoreList[i].Score);
        }
    }

    void SortHighScores()
    {
        //highScoreList = highScoreList.OrderByDescending(x => x.Score).ToList();
        highScoreList.Sort((x, y) => y.Score.CompareTo(x.Score));
        //Debug.Log("HIGHEST SCORE: [0!] " + highScoreList[0].Name + " | " + highScoreList[0].Score);
    }

    void SaveHighScores()
    {
        int count = highScoreList.Count;
        if (count > 5)
        {
            count = 5;
        }

        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                if (highScoreList[i].Score != 0)
                {
                    if (highScoreList[i].Name == "")
                    {
                        highScoreList[i].Name = "Anonymous";
                    }
                    PlayerPrefs.SetString("HighPlayer" + i, highScoreList[i].Name);
                    PlayerPrefs.SetInt("HighScore" + i, highScoreList[i].Score);
                    //Debug.Log("SAVE: [" + i + "] " + PlayerPrefs.GetString("HighPlayer" + i, "???") + " | " + PlayerPrefs.GetInt("HighScore" + i, 999));
                }
            }
        }
    }

    bool AddHighScore(string newName, int newScore)
    {
        if (highScoreList.Count >= 5 && newScore <= highScoreList[4].Score)
        {
            return false;
        }

        highScoreList.Add(new PlayerScore(newName, newScore));
        return true;
    }

    void DebugLogHighScores()
    {
        Debug.Log("PLAYERPREFS HIGH SCORES...");
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i + ": " + PlayerPrefs.GetString("HighPlayer" + i, "???") + " | " + PlayerPrefs.GetInt("HighScore" + i, 999));
        }
    }

    private class PlayerScore
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public PlayerScore(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }

    public void UpdateHighScoreUI()
    {
        LoadHighScores();
        for (int i = 0; i < highPlayersTMP.Count; i++)
        {
            if (highScoreList[i] == null || highScoreList[i].Score == 0)
            {

                highPlayersTMP[i].text = "";
                highScoresTMP[i].text = "";
            }
            else
            {
                highPlayersTMP[i].text = highScoreList[i].Name;
                highScoresTMP[i].text = highScoreList[i].Score.ToString();
            }
        }
    }
}