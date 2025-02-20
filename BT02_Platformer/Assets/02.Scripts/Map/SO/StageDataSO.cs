using UnityEngine;

[CreateAssetMenu(menuName ="Stage/StageData", fileName ="Stage_")]
public class StageDataSO : ScriptableObject
{
    public int stageID; //stage ID
    public Vector3 playerInitPos; //�÷��̾� ù ��ġ
    public GameObject MapPrefab; //�� ������
    public int timeCount; //ī��Ʈ �ٿ�
}
