using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsManager : MonoBehaviour
{
    [Serializable]
    private class PickupLibraryEntry
    {
        public Pickup.Type type;
        public GameObject prefab;
    }

    private static PickupsManager _singleton;

    public static PickupsManager Singleton => _singleton ??= FindObjectOfType<PickupsManager>();

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