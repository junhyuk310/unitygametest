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
            { "apple", new List<string> { "���" } },
            { "banana", new List<string> { "�ٳ���" } },
            { "cherry", new List<string> { "ü��", "�����" } },
            // �߰����� �ܾ� �� ��
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
            feedbackText.text = "�¾ҽ��ϴ�! +" + rewardAmount + "��";
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
            feedbackText.text = "Ʋ�Ƚ��ϴ�. �ٽ� �õ��غ�����.";
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