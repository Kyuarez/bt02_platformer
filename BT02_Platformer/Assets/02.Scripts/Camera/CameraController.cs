using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private PlayerController target;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        if(target == null)
        {
            FindTarget();
        }
    }

    private void OnDisable()
    {
        target = null;
    }

    private void LateUpdate()
    {
        if (target == null) 
        {
            FindTarget();
        }


    }

    public void FindTarget()
    {
        PlayerController obj = Object.FindFirstObjectByType<PlayerController>();
        if (obj == null) 
        {
            Debug.LogAssertion("player is null");
            return;
        }

        target = Object.FindFirstObjectByType<PlayerController>();
    }
}
