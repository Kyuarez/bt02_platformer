using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [Header("Camera Limit")]
    [SerializeField] private float limit_left;
    [SerializeField] private float limit_right;
    [SerializeField] private float limit_top;
    [SerializeField] private float limit_bottom;

    [SerializeField] private GameObject subScreen;

    [Header("Forced")]
    public bool isForceScrollX;
    public bool isForceScrollY;
    public float forceScrollSpeedX = 0.5f;
    public float forceScrollSpeedY = 0.5f;

    private Camera cam;
    private PlayerController target;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        CameraMoveToTarget();
    }

    private void CameraMoveToTarget()
    {
        if(target == null)
        {
            target = Object.FindFirstObjectByType<PlayerController>();
            return;
        }

        Vector3 targetPos = target.transform.position;
        float adjustX = targetPos.x;
        float adjustY = targetPos.y;

        //@tk : 쿠키런 로직, 알아서 카메라 가서 벽에 부딪히게 함. 강제로
        if (isForceScrollX)
        {
            adjustX = transform.position.x + (forceScrollSpeedX * Time.deltaTime);
        }
        if (isForceScrollY)
        {
            adjustX = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
        }

        if (adjustX < limit_left)
        {
            adjustX = limit_left;
        }
        else if (adjustX > limit_right)
        {
            adjustX = limit_right;
        }

        if (adjustY < limit_bottom)
        {
            adjustY = limit_bottom;
        }
        else if (adjustY > limit_top)
        {
            adjustY = limit_top;
        }

        cam.transform.position = new Vector3(adjustX, adjustY, cam.transform.position.z);

        OnSubScreen(adjustX);
    }

    private void OnSubScreen(float x)
    {
        if(subScreen == null)
        {
            return;
        }

        float y = subScreen.transform.position.y;
        float z = subScreen.transform.position.z;
        Vector3 vec = new Vector3(x * 0.5f, y, z);
        subScreen.transform.position = vec;
    }
}
