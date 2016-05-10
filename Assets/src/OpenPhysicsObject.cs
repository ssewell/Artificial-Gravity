using UnityEngine;
using System;
using System.Collections;

/* 
 * This is the main class for physics acting on an untethered object
 */

public class OpenPhysicsObject : MonoBehaviour {

    public GameObject SpaceStation;
    public float Mass = 1.0f;
    public Character Parent;

    public void Start() {
        // Create and configure rigidbody component
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.angularDrag = 0.0f;
        rigidbody.mass = Mass;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // Create a constant force component
        gameObject.AddComponent<ConstantForce>();

        airResistanceForce = new AirResistanceForce(SpaceStation, gameObject, Parent);
        jetpack = new Jetpack(gameObject, 1.0f, Parent);
    }

    protected AirResistanceForce airResistanceForce;
    protected Jetpack jetpack;

    void FixedUpdate() {
        var forceComponent = GetComponent<ConstantForce>();
        forceComponent.force = Vector3.zero;
        forceComponent.relativeForce = Vector3.zero;

        airResistanceForce.Apply();
        jetpack.Apply();
    }
}
