using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Type
    {
        Ammo
    }

    [Header("Animation")]
    [SerializeField] private Transform _pickupRoot;
    [SerializeField] private float _bobRate;
    [SerializeField] private float _bobRange;
    [SerializeField] private float _spinRate;

    private float _timeAlive;
    private float _baseY;

    protected virtual void Start()
    {
        _baseY = _pickupRoot.position.y;
    }

    protected virtual void Update()
    {
        Vector3 newPosition = _pickupRoot.position;
        newPosition.y = _baseY + (_bobRange * Mathf.Sin(Time.time * _bobRate));
        _pickupRoot.position = newPosition;

        _pickupRoot.Rotate(Vector3.up, Time.deltaTime * _spinRate);
    }
}