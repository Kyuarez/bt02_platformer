using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Title,
    InGame,
}

public class SceneTransitionManager : MonoSingleton<SceneTransitionManager>
{

    public void LoadScene(SceneType sceneType)
    {
        SceneManager.LoadScene(sceneType.ToString());
    }

    public void LoadSceneAsync(SceneType sceneType)
    {
        StartCoroutine(CoLoadScene(sceneType));
    }

    IEnumerator CoLoadScene(SceneType sceneType)
    {
        yield return SceneManager.LoadSceneAsync(sceneType.ToString());
        GameManager.Instance.OnLoadStage();
    }
}
