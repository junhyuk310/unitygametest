using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void NextSceneWithNum()
    {
        // �� ��ȣ�� �̿��ؼ� �� �̵�
        SceneManager.LoadScene(1);  // 1 ��° �� �ε�
    }
}