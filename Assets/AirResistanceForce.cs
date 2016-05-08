using UnityEngine;
using System;
using System.Collections;


public class AirResistanceForce {

    protected GameObject spinningObject;

    public AirResistanceForce(GameObject spinningObjectSource) {
        spinningObject = spinningObjectSource;
    }

    public void ApplyToGameObject(GameObject target) {

        // Setup variables
        float angularVelocity = spinningObject.GetComponent<Rigidbody>().angularVelocity.magnitude;
        Vector3 targetVector = target.transform.position;
        Vector3 targetVelocityVector = target.GetComponent<Rigidbody>().velocity;
        ConstantForce targetForce = target.GetComponent<ConstantForce>();

        // Get air vector
        Vector3 forceVectorNormalized = GetForceVectorNormalized(targetVector);
        float radius = GetDeltaVector(GetClosestPointOnAxis(targetVector), targetVector).magnitude;
        float forceMagnitude = GetLinearVelocity(angularVelocity, radius);
        Vector3 forceVector = forceVectorNormalized * forceMagnitude;
        Vector3 frictionVector = forceVector * Time.fixedDeltaTime;

        // Apply air vector to target
        Vector3 frictionVelocityVector = forceVector - targetVelocityVector;
        targetForce.force = frictionVelocityVector.normalized * (float)Math.Pow(frictionVelocityVector.magnitude, 2) * 0.01f;

    }

    // Convert angular velocity and radians to linear velocity
    protected float GetLinearVelocity(float angularVelocity, float radius) {
        return angularVelocity * radius;
    }


    // Calcululate the closest point to the target that is on the axis line
    protected Vector3 GetClosestPointOnAxis(Vector3 target) {
        return new Vector3(0, target.y, 0);
    }

    // Get the difference between two vectors
    protected Vector3 GetDeltaVector(Vector3 a, Vector3 b) {
        return b - a;
    }

    // Get the vector of the moving air
    protected Vector3 GetForceVectorNormalized(Vector3 target) {
        Vector3 axisHelper = GetClosestPointOnAxis(target);
        Vector3 deltaVector = GetDeltaVector(axisHelper, target); // target - axisHelper;
        float forceMagnitude = deltaVector.magnitude;
        Vector3 forceVector = Vector3.Cross(target, axisHelper).normalized;
        // Invert this to change the rotational direction
        forceVector = -forceVector;
        return forceVector;
    }
}
