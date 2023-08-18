using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public GameObject pickupGlowPrefab;
    public float targetTime = 5.0f;
    public float roll = 0;
    public bool hasChild = false;

    private void Start()
    {
        targetTime = Random.Range(5.0f, 30.0f);
    }

    private void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
        if (transform.childCount > 0)
        {
            hasChild = true;
        }   
        else
        {
            hasChild = false;
        }
    }

    public void timerEnded()
    {
        roll = Random.Range(0.0f, 100.0f);
        if ((roll >= 50) && (hasChild == false))
        {
            Instantiate(pickupPrefab, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Quaternion.identity, transform);
            Instantiate(pickupGlowPrefab, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
        }    
        targetTime = 15.0f;
    }
}
