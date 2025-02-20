using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum GAME_STATE
{
    PLAYING,
    GAMEOVER,
    GAMECLEAR,
    END,
}

public class GameManager : MonoSingleton<GameManager>
{
    private static GAME_STATE state;
    public static GAME_STATE STATE
    {
        get
        {
            return state;
        }
        set
        {
            UIManager.InGameUI.SetActiveInGameUI(value);
            state = value;
        }
    }

    private Transform loadObjectParent;
    public Transform LoadObjectParent
    {
        get 
        {
            if (loadObjectParent == null) 
            {
                loadObjectParent = GameObject.Find("LoadObjectParent")?.transform;
            }
            return loadObjectParent;
        }
    }

    private int currentStage;
    public int CurrentStage
    {
        get
        {
            return currentStage;
        }
        set
        {
            //기존 씬 제거
            if(currentStage != value)
            {
                OnLoadStage(value);
            }
        }
    }

    protected float stageTime;
    protected float displayTime;
    protected bool isCountDown;

    float times;


    protected override void Awake()
    {
        base.Awake();
        stageList = Resources.Load<StageListSO>("Stages/StageList").stageList;
    }

    private void Update()
    {
        //timer
    }

    public void OnLoadStage()
    {
        currentStage = 1;
        RemoveStage();
        OnLoadMap();

        STATE = GAME_STATE.PLAYING;
        isCountDown = true;
    }

    public void OnLoadStage(int stageID)
    {
        currentStage = stageID;
        RemoveStage();
        OnLoadMap();

        STATE = GAME_STATE.PLAYING;
        isCountDown = true;
    }

    private void OnLoadMap()
    {
        StageDataSO stageData = stageList[currentStage - 1];

        //instantiate map
        Instantiate(stageData.MapPrefab, LoadObjectParent);
        //instantiate player
        GameObject playerObj = Resources.Load<GameObject>("Prefabs/Player");
        Instantiate(playerObj, stageData.playerInitPos, Quaternion.identity, LoadObjectParent);
        //@tk 게임매니저에서 타이머 체크
        stageTime = stageData.timeCount;

    }

    public void ResetStage()
    {
        StageDataSO stageData = stageList[currentStage - 1];

        //timer & player reset
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.InitPlayerPos(stageData.playerInitPos);
        STATE = GAME_STATE.PLAYING;
        stageTime = stageData.timeCount;
        isCountDown = true;
    }

    public void RemoveStage()
    {
        stageTime = 0;
        if (LoadObjectParent.childCount <= 0)
        {
            return;
        }

        foreach (Transform child in LoadObjectParent) 
        {
            Destroy(child.gameObject);
        }
    }

    private StageDataSO[] stageList;

}
