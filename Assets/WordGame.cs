using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WordGame : MonoBehaviour
{
    public Text wordText;
    public InputField inputField;
    public Text feedbackText;

    private Dictionary<string, List<string>> wordDictionary;
    private int score;
    private int sum;

    public int rewardAmount = 10;

    private void Start()
    {
        wordDictionary = new Dictionary<string, List<string>>
        {
            { "apple", new List<string> { "사과" } },
            { "banana", new List<string> { "바나나" } },
            { "cherry", new List<string> { "체리", "산딸기" } },
            // 추가적인 단어 및 뜻
        };

        score = PlayerPrefs.GetInt("Score", 0);
        sum = PlayerPrefs.GetInt("Sum", 0);
        UpdateMoneyUI();
        UpdateSumUI();
        GenerateRandomWord();
    }

    private void GenerateRandomWord()
    {
        string word = wordDictionary.Keys.ElementAt(Random.Range(0, wordDictionary.Count));
        List<string> meanings = wordDictionary[word];

        wordText.text = string.Join(", ", meanings.ToArray());
        Debug.Log(string.Join(", ", meanings.ToArray()));
    }

    public void CheckWord()
    {
        string input = inputField.text.ToLower();

        var query = wordDictionary.Where(pair => pair.Key == input);

        if (query.Any())
        {
            feedbackText.text = "맞았습니다! +" + rewardAmount + "점";
            score += rewardAmount;
            sum += 1;
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetInt("Sum", sum);
            UpdateSumUI();
            UpdateMoneyUI();
            GenerateRandomWord();
        }
        else
        {
            feedbackText.text = "틀렸습니다. 다시 시도해보세요.";
        }

        inputField.text = "";
    }

    private void UpdateMoneyUI()
    {
        GameObject.Find("ScoreText").GetComponent<Text>().text = score.ToString();
    }
    private void UpdateSumUI()
    {
        GameObject.Find("SumText").GetComponent<Text>().text = sum.ToString();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Sum", sum);
    }
}