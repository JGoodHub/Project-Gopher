using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGlow : MonoBehaviour
{
    public float deathTime = 2.0f;
    private void Start()
    {
        transform.Rotate(-90, 0, 0);
    }
    private void Update()
    {
        Vector3 rotationToAdd = new Vector3(0, 0, 2);
        transform.Rotate(rotationToAdd);
        deathTime -= Time.deltaTime;

        if (deathTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
