using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public void OnClickStartGame()
    {
        SceneTransitionManager.LoadScene(SceneType.InGame);
    }

    public void OnClickLoadGame()
    {
        //���߿� �� �����Ѱ� �����ͼ�
    }

    public void OnClickEndGame()
    {
        //���� ����
        Application.Quit();
    }
}
