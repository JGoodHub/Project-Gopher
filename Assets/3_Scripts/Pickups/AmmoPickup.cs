using UnityEngine;

public class AmmoPickup : Pickup
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MyPlayer"))
        {
            AttributeSet playerAttributeSet = other.GetComponent<AttributeSet>();

            if (playerAttributeSet.AreChainsMaxedOut() == false)
            {
                playerAttributeSet.heldChains++;
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