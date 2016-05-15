using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

/// <summary>
/// The player inventory object manages collectable items, attaching player behaviors defined by the collectable items.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    private AudioSource playerAudioSource;

    /// <summary>
    /// Creates a player audio source used to play the collectable item sounds
    /// </summary>
    void Start()
    {
        playerAudioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// When the player collides with a item with a "Collectable" tag, the script defined by the item's Behavior attirbuted is added to the player.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        var collectableItem = other.GetComponent<CollectableItem>();

        // Determine if the object we collided with defines a CollectableItem
        if (collectableItem != null)
        {
            // Hide the object with which we collided
            other.gameObject.SetActive(false);

            var sound = collectableItem.CollectSound;

            // If the collectable item defines a sound, play it
            if (sound != null)
            {
                playerAudioSource.PlayOneShot(sound);
            }

            // Determine if a behavior is defined on the collectable object
            if (collectableItem.BehaviorName != null)
            { 
                // It is, add the behavior to the player
                gameObject.AddComponent(Type.GetType(collectableItem.BehaviorName));
            }
        }

    }
}
