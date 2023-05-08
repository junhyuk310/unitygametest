using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WordGame : MonoBehaviour
{
    public Text wordText;
    public InputField inputField;
    public Text feedbackText;
    public Button checkButton;
    public int rewardAmount = 10;

    private Dictionary<string, List<string>> wordDictionary;
    private int money;

    private void Start()
    {
        // ���� �ܾ�� ���� Dictionary�� ����
        wordDictionary = new Dictionary<string, List<string>>
        {
            { "apple", new List<string> { "���" } },
            { "banana", new List<string> { "�ٳ���" } },
            { "cherry", new List<string> { "ü��", "�����" } },
            // �߰����� �ܾ� �� ��
        };
        checkButton.onClick.AddListener(CheckWord);

        money = PlayerPrefs.GetInt("Money", 0);
        UpdateMoneyUI();
        GenerateRandomWord();
    }

    private void GenerateRandomWord()
    {
        // Dictionary���� �����ϰ� �ܾ ����
        string word = wordDictionary.Keys.ElementAt(Random.Range(0, wordDictionary.Count));

        // ���õ� �ܾ��� ���� Text UI�� ǥ��
        wordText.text = wordDictionary[word][0];

        // ���õ� �ܾ��� ����� �α׷� ���
        Debug.Log(string.Join(", ", wordDictionary[word].ToArray()));
    }

    public void CheckWord()
    {
        // ����ڰ� �Է��� �ܾ�
        string input = inputField.text.ToLower();

        // Dictionary���� �ش� �ܾ ã��
        if (wordDictionary.ContainsKey(input))
        {
            feedbackText.text = "�¾ҽ��ϴ�! +" + rewardAmount + "��";
            money += rewardAmount;
            PlayerPrefs.SetInt("Money", money);
            UpdateMoneyUI();
            GenerateRandomWord();
        }
        else
        {
            feedbackText.text = "Ʋ�Ƚ��ϴ�. �ٽ� �õ��غ�����.";
        }

        // �Է� �ʵ� �ʱ�ȭ
        inputField.text = "";
    }

    private void UpdateMoneyUI()
    {
        // ���� UI�� ǥ��
        // ���� ���, MoneyText��� �̸��� Text UI�� �ִٸ� �Ʒ��� ���� ���
        GameObject.Find("MoneyText").GetComponent<Text>().text = money.ToString();
    }

    private void OnApplicationQuit()
    {
        // ���� ���� �� ���� ����
        PlayerPrefs.SetInt("Money", money);
    }
}