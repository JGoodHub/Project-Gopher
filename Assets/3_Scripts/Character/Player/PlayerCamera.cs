using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // public static PlayerCamera instance;
    public PlayerManager playerManager;
    public float cameraHeight = 12.0f; 
    public float cameraAngle = 60.0f; 

    private void Awake()
    {
        
        //TODO: CharacterManager is on the player, only one can be active rn 
        // if (instance == null)
        // {
        //     instance = this;
        // }
        // else
        // {
        //     Destroy(this);
        // }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerManager = GetComponentInParent<PlayerManager>();
    }
    

    public void AttachToPlayerAndFollow()
    {
        if (!playerManager) return;
        Vector3 targetPosition = playerManager.transform.position;
        Vector3 cameraOffset = Quaternion.Euler(cameraAngle, 0, 0) * Vector3.back * cameraHeight;
        Vector3 cameraPosition = targetPosition + cameraOffset;
        
        transform.position = cameraPosition;
        
        transform.LookAt(targetPosition);
    }


   
}