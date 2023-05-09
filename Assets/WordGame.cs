using System.Collections.Generic;
using System.Linq;  // LINQ�� ����ϱ� ���� �߰�
using UnityEngine;
using UnityEngine.UI;

public class WordGame : MonoBehaviour
{
    public Text wordText;   // �ܾ� ���� ǥ���� UI �ؽ�Ʈ
    public InputField inputField;   // ����� �Է��� ���� �Է� �ʵ�
    public Text feedbackText;   // ���� ���θ� ǥ���� UI �ؽ�Ʈ

    private Dictionary<string, List<string>> wordDictionary;
    private int score;  //�� ����
    private int sum;    //�����ؼ� ���� �ܾ��� ����
    public float rewardAmount;  //������ ��� ��

    private void Start()
    {
        wordDictionary = new Dictionary<string, List<string>>   //�ܾ����
        {
            { "apple", new List<string> { "���" } },
            { "banana", new List<string> { "�ٳ���" } },
            { "cherry", new List<string> { "ü��", "�����" } },
            { "dog", new List<string> { "��" } },
            { "cat", new List<string> { "�����" } },
            { "orange", new List<string> { "������" } },
            { "car", new List<string> { "�ڵ���" } },
            { "book", new List<string> { "å" } },
            { "computer", new List<string> { "��ǻ��" } },
            { "flower", new List<string> { "��" } },
            { "tree", new List<string> { "����" } },
            { "chaos", new List<string> { "ȥ��" } },
            { "shrimp", new List<string> { "����" } },
            { "fish", new List<string> { "�����" } },
            { "water", new List<string> { "��" } },
            { "rice", new List<string> { "��" } },
            { "game", new List<string> { "����" } },
            { "unity", new List<string> { "����Ƽ" } },
            { "teacher", new List<string> { "������" } },
            { "doctor", new List<string> { "�ǻ�" } },
            { "sky", new List<string> { "�ϴ�" } },
            { "soil", new List<string> { "��" } },
            { "pen", new List<string> { "��" } },
            { "eye", new List<string> { "��" } },
            { "family", new List<string> { "����" } },
            { "hand", new List<string> { "��" } },
            { "money", new List<string> { "��" } },
            { "people", new List<string> { "�����" } },
            { "school", new List<string> { "�б�" } },
            { "child", new List<string> { "����" } },
            { "dawn", new List<string> { "����" } },
            // �߰����� �ܾ� �� ��
        };

        score = PlayerPrefs.GetInt("Score", 0); //�����ߴ� ���� �ҷ�����
        sum = PlayerPrefs.GetInt("Sum", 0); //�����ߴ� �޺� �ҷ�����
        UpdateScoreUI(); 
        UpdateSumUI();  
        GenerateRandomWord();   
    }

    private void GenerateRandomWord()
    {
        string word = wordDictionary.Keys.ElementAt(Random.Range(0, wordDictionary.Count)); //�ܾ �����ϰ� ����
        List<string> meanings = wordDictionary[word];

        wordText.text = string.Join(", ", meanings.ToArray());  // ���õ� �ܾ��� ���� Text UI�� ǥ��
        Debug.Log(string.Join(", ", meanings.ToArray()));   // ���õ� �ܾ��� ����� �α׷� ���
    }

    public void CheckWord()
    {
        rewardAmount = (((Mathf.Pow(1.06f, 10) - Mathf.Pow(1.06f, 10 + sum)) / (1 - 1.06f)));   //�޺��� ���� ������ ������ �����ϴ� ����
        string input = inputField.text.ToLower();   //�Է¹��� ��� �ܾ �ҹ��ڷ� �ٲ㼭 ����

        var query = wordDictionary.Where(pair => pair.Key == input);     // Dictionary���� ����� �Է°� ��ġ�ϴ� Ű(�ܾ�)�� �˻�

        if (query.Any())    //�ܾ ��ġ�ϴ� ���
        {
            feedbackText.text = "�¾ҽ��ϴ�! +" + (int)rewardAmount + "��";   //�¾Ҵٰ� ���
            score += (int)rewardAmount; //���� ����
            sum += 1;   //�޺� ����
            PlayerPrefs.SetInt("Score", score); //���� ����
            PlayerPrefs.SetInt("Sum", sum); //�޺� ����
            UpdateSumUI();
            UpdateScoreUI();
            GenerateRandomWord();
        }
        else
        {
            feedbackText.text = "Ʋ�Ƚ��ϴ�. �ٽ� �õ��غ�����.";
            sum = 0;
        }

        inputField.text = "";   //�Է��ʵ� �ʱ�ȭ
    }

    private void UpdateScoreUI()
    {
        GameObject.Find("ScoreText").GetComponent<Text>().text = score.ToString();  //UI�� ���� ǥ��
    }
    private void UpdateSumUI()
    {
        GameObject.Find("SumText").GetComponent<Text>().text = sum.ToString();  //UI�� �޺� ǥ��
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Sum", sum);
    }
}