using UnityEngine;

[CreateAssetMenu(menuName ="Stage/StageData", fileName ="Stage_")]
public class StageDataSO : ScriptableObject
{
    public int stageID; //stage ID
    public Vector3 playerInitPos; //플레이어 첫 위치
    public GameObject MapPrefab; //맵 프리팹
    public int timeCount; //카운트 다운
}
