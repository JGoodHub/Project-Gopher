using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private static PlayerCamera _singleton;

    public static PlayerCamera Singleton => _singleton;

    public PlayerManager playerManager;
    public float cameraHeight = 12.0f;
    public float cameraAngle = 60.0f;

    private void Awake()
    {
        _singleton = this;
    }

    public void SetPlayerTarget(PlayerManager player)
    {
        playerManager = player;
    }

    private void Update()
    {
        if (playerManager == null)
            return;

        Vector3 targetPosition = playerManager.transform.position;
        Vector3 cameraOffset = Quaternion.Euler(cameraAngle, 0, 0) * Vector3.back * cameraHeight;
        Vector3 cameraPosition = targetPosition + cameraOffset;

        transform.position = cameraPosition;

        transform.LookAt(targetPosition);
    }
}