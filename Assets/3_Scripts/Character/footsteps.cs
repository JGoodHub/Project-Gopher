using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;

    private void Update()
    {
        if (footstepsSound == enabled)
        {
            if (footstepsSound.time == footstepsSound.clip.length)
            {
                footstepsSound.pitch = Random.Range(-1.0f, 1.0f);
            }
                
        }
        
    }
}
