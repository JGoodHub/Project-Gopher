using UnityEngine;

public class AmmoPickup : Pickup
{
    public AudioClip clip;
    public float volume = 0.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            AttributeSet playerAttributeSet = other.GetComponent<AttributeSet>();

            if (playerAttributeSet.AreChainsMaxedOut() == false)
            {
                playerAttributeSet.heldChains++;
                AudioSource.PlayClipAtPoint(clip, transform.position, volume);
                Destroy(gameObject);
            }

            return;
        }

        if (other.CompareTag("OtherPlayers"))
        {
            Destroy(gameObject);
        }
    }
}