using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public void OnClickStartGame()
    {
        SceneTransitionManager.LoadScene(SceneType.InGame);
    }

    public void OnClickLoadGame()
    {
        //나중에 씬 저장한거 가져와서
    }

    public void OnClickEndGame()
    {
        //로컬 저장
        Application.Quit();
    }
}
