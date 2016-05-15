using UnityEngine;
using System.Collections;

public class CollectableItem : MonoBehaviour {

    /// <summary>
    /// When the collectable item collides with the player controller, the player will gain this behavior
    /// </summary>
    public string BehaviorName;

    /// <summary>
    /// An optional sound that is played when the user collects the item
    /// </summary>
    public AudioClip CollectSound;
	
	/// <summary>
    /// Collectable items rotate, so it's more apparent to the user that it's colletable
    /// </summary>
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
