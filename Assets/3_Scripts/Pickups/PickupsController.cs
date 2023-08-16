using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsController : MonoBehaviour
{
    [Serializable]
    private class PickupLibraryEntry
    {
        public Pickup.Type type;
        public GameObject prefab;
    }

    private static PickupsController _singleton;

    public static PickupsController Singleton => _singleton ??= FindObjectOfType<PickupsController>();

    private Transform _pickupsContainer;


    [SerializeField] private List<PickupLibraryEntry> _pickupsLibrary;


    private void Start()
    {
        _pickupsContainer ??= new GameObject("[PICKUPS]").transform;
    }

    public void SpawnPickup(Pickup.Type type, Vector3 position)
    {
        GameObject pickupPrefab = _pickupsLibrary.Find(entry => entry.type == type).prefab;
        Instantiate(pickupPrefab, position, Quaternion.identity, _pickupsContainer);
    }
}