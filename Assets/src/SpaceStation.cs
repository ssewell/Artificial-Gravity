using UnityEngine;
using System.Collections;

public class SpaceStation : MonoBehaviour {

    protected float maxTorque = 1e+09f;
    protected float targetVelocity = 0.05f;

    void Start () {
        SetTorque(1f);
	}
	
	void Update () {
        
    }

    void FixedUpdate() {
        
        // Poor man's torque to hit a target angular velocity because Unity's Motor doesn't handle large values
        float angularVelocity = GetComponent<Rigidbody>().angularVelocity.magnitude;

        if (angularVelocity > targetVelocity - targetVelocity / 10) {
            SetTorque(0.01f);
        }

        if (angularVelocity > targetVelocity) {
            SetTorque(0.0f);
            maxTorque = 0;
        }
    }

    public void SetTorque(float magnitude) {
        GetComponent<ConstantForce>().torque = new Vector3(0, maxTorque * magnitude, 0);
    }
}
