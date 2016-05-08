using UnityEngine;
using System.Collections;

public class SpaceStation : MonoBehaviour {

    protected float maxTorque = 1e+09f;
    protected float targetVelocity = 0.01f;

    void Start () {
        SetTorque(1f);
	}
	
	void Update () {

    }

    void FixedUpdate() {
        
        // Poor man's torque to hit a target velocity because Unity's Motor doesn't handle large values
        float angularVelocity = GetComponent<Rigidbody>().angularVelocity.magnitude;

        if (angularVelocity > targetVelocity - targetVelocity / 10) {
            SetTorque(0.01f);
        }

        if (angularVelocity > targetVelocity) {
            SetTorque(0.0f);
        }
    }

    public void SetTorque(float magnitude) {
        var forceComponent = GetComponent<ConstantForce>().torque = new Vector3(0, maxTorque * magnitude, 0);
    }
}
