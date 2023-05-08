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
        // 영어 단어와 뜻을 Dictionary에 저장
        wordDictionary = new Dictionary<string, List<string>>
        {
            { "apple", new List<string> { "사과" } },
            { "banana", new List<string> { "바나나" } },
            { "cherry", new List<string> { "체리", "산딸기" } },
            // 추가적인 단어 및 뜻
        };
        checkButton.onClick.AddListener(CheckWord);

        money = PlayerPrefs.GetInt("Money", 0);
        UpdateMoneyUI();
        GenerateRandomWord();
    }

    private void GenerateRandomWord()
    {
        // Dictionary에서 랜덤하게 단어를 선택
        string word = wordDictionary.Keys.ElementAt(Random.Range(0, wordDictionary.Count));

        // 선택된 단어의 뜻을 Text UI에 표시
        wordText.text = wordDictionary[word][0];

        // 선택된 단어의 뜻들을 로그로 출력
        Debug.Log(string.Join(", ", wordDictionary[word].ToArray()));
    }

    public void CheckWord()
    {
        // 사용자가 입력한 단어
        string input = inputField.text.ToLower();

        // Dictionary에서 해당 단어를 찾음
        if (wordDictionary.ContainsKey(input))
        {
            feedbackText.text = "맞았습니다! +" + rewardAmount + "원";
            money += rewardAmount;
            PlayerPrefs.SetInt("Money", money);
            UpdateMoneyUI();
            GenerateRandomWord();
        }
        else
        {
            feedbackText.text = "틀렸습니다. 다시 시도해보세요.";
        }

        // 입력 필드 초기화
        inputField.text = "";
    }

    private void UpdateMoneyUI()
    {
        // 돈을 UI에 표시
        // 예를 들어, MoneyText라는 이름의 Text UI가 있다면 아래와 같이 사용
        GameObject.Find("MoneyText").GetComponent<Text>().text = money.ToString();
    }

    private void OnApplicationQuit()
    {
        // 게임 종료 시 돈을 저장
        PlayerPrefs.SetInt("Money", money);
    }
}