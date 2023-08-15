using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public PlayerManager playerManager;
    public float cameraHeight = 10.0f; 
    public float cameraAngle = 60.0f; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HandleFollowTarget()
    {
        Vector3 targetPosition = PlayerManager.instance.transform.position;
        Vector3 cameraOffset = Quaternion.Euler(cameraAngle, 0, 0) * Vector3.back * cameraHeight;
        Vector3 cameraPosition = targetPosition + cameraOffset;
        
        transform.position = cameraPosition;
        
        transform.LookAt(targetPosition);
    }

    void Update()
    {
        HandleFollowTarget();
    }
}