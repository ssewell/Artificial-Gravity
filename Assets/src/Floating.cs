using UnityEngine;
using System.Collections;

/// <summary>
/// Simple class to demonstrate adding a behavior to the player object at runtime when collecting inventory
/// </summary>
public class Floating : MonoBehaviour {

    public float thrust;
    private Rigidbody playerBody;

	// Use this for initialization
	void Start () {
        playerBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        playerBody.isKinematic = false;
        playerBody.AddForce(transform.up * 40);
    }
}
