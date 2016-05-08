using UnityEngine;
using System;
using System.Collections;

/* 
 * This is the main class for physics acting on an untethered object
 */

public class OpenPhysicsObject : MonoBehaviour {

    public GameObject SpaceStation;
    public float Mass = 1.0f;


    public void Start() {
        // Create and configure rigidbody component
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.angularDrag = 0.0f;
        rigidbody.mass = Mass;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // Create a constant force component
        var constantForce = gameObject.AddComponent<ConstantForce>();
    }

    void FixedUpdate() {
        (new AirResistanceForce(SpaceStation)).ApplyToGameObject(gameObject);
    }
}
