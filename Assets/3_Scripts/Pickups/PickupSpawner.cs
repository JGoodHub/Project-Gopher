using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float targetTime = 5.0f;
    public float roll = 0;
    public bool hasChild = false;
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
        }    
        targetTime = 5.0f;
    }

}

