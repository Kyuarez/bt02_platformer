using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Title,
    InGame,
}

public class SceneTransitionManager : MonoBehaviour
{


    public static void LoadScene(SceneType sceneType)
    {
        SceneManager.LoadScene(sceneType.ToString());
    }        

}
