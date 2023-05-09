using System.Collections.Generic;
using System.Linq;  // LINQ를 사용하기 위해 추가
using UnityEngine;
using UnityEngine.UI;

public class WordGame : MonoBehaviour
{
    public Text wordText;   // 단어 뜻을 표시할 UI 텍스트
    public InputField inputField;   // 사용자 입력을 받을 입력 필드
    public Text feedbackText;   // 정답 여부를 표시할 UI 텍스트

    private Dictionary<string, List<string>> wordDictionary;
    private int score;  //총 점수
    private int sum;    //연속해서 맞춘 단어의 개수
    public float rewardAmount;  //점수를 얻는 양

    private void Start()
    {
        wordDictionary = new Dictionary<string, List<string>>   //단어사전
        {
            { "apple", new List<string> { "사과" } },
            { "banana", new List<string> { "바나나" } },
            { "cherry", new List<string> { "체리", "산딸기" } },
            { "dog", new List<string> { "개" } },
            { "cat", new List<string> { "고양이" } },
            { "orange", new List<string> { "오렌지" } },
            { "car", new List<string> { "자동차" } },
            { "book", new List<string> { "책" } },
            { "computer", new List<string> { "컴퓨터" } },
            { "flower", new List<string> { "꽃" } },
            { "tree", new List<string> { "나무" } },
            { "chaos", new List<string> { "혼돈" } },
            { "shrimp", new List<string> { "새우" } },
            { "fish", new List<string> { "물고기" } },
            { "water", new List<string> { "물" } },
            { "rice", new List<string> { "밥" } },
            { "game", new List<string> { "게임" } },
            { "unity", new List<string> { "유니티" } },
            { "teacher", new List<string> { "선생님" } },
            { "doctor", new List<string> { "의사" } },
            { "sky", new List<string> { "하늘" } },
            { "soil", new List<string> { "흙" } },
            { "pen", new List<string> { "펜" } },
            { "eye", new List<string> { "눈" } },
            { "family", new List<string> { "가족" } },
            { "hand", new List<string> { "손" } },
            { "money", new List<string> { "돈" } },
            { "people", new List<string> { "사람들" } },
            { "school", new List<string> { "학교" } },
            { "child", new List<string> { "아이" } },
            { "dawn", new List<string> { "새벽" } },
            // 추가적인 단어 및 뜻
        };

        score = PlayerPrefs.GetInt("Score", 0); //저장했던 점수 불러오기
        sum = PlayerPrefs.GetInt("Sum", 0); //저장했던 콤보 불러오기
        UpdateScoreUI(); 
        UpdateSumUI();  
        GenerateRandomWord();   
    }

    private void GenerateRandomWord()
    {
        string word = wordDictionary.Keys.ElementAt(Random.Range(0, wordDictionary.Count)); //단어를 랜덤하게 선택
        List<string> meanings = wordDictionary[word];

        wordText.text = string.Join(", ", meanings.ToArray());  // 선택된 단어의 뜻을 Text UI에 표시
        Debug.Log(string.Join(", ", meanings.ToArray()));   // 선택된 단어의 뜻들을 로그로 출력
    }

    public void CheckWord()
    {
        rewardAmount = (((Mathf.Pow(1.06f, 10) - Mathf.Pow(1.06f, 10 + sum)) / (1 - 1.06f)));   //콤보에 따라 오르는 점수가 증가하는 수식
        string input = inputField.text.ToLower();   //입력받은 모든 단어를 소문자로 바꿔서 저장

        var query = wordDictionary.Where(pair => pair.Key == input);     // Dictionary에서 사용자 입력과 일치하는 키(단어)를 검색

        if (query.Any())    //단어가 일치하는 경우
        {
            feedbackText.text = "맞았습니다! +" + (int)rewardAmount + "점";   //맞았다고 출력
            score += (int)rewardAmount; //점수 증가
            sum += 1;   //콤보 증가
            PlayerPrefs.SetInt("Score", score); //점수 저장
            PlayerPrefs.SetInt("Sum", sum); //콤보 저장
            UpdateSumUI();
            UpdateScoreUI();
            GenerateRandomWord();
        }
        else
        {
            feedbackText.text = "틀렸습니다. 다시 시도해보세요.";
            sum = 0;
        }

        inputField.text = "";   //입력필드 초기화
    }

    private void UpdateScoreUI()
    {
        GameObject.Find("ScoreText").GetComponent<Text>().text = score.ToString();  //UI에 점수 표시
    }
    private void UpdateSumUI()
    {
        GameObject.Find("SumText").GetComponent<Text>().text = sum.ToString();  //UI에 콤보 표시
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Sum", sum);
    }
}