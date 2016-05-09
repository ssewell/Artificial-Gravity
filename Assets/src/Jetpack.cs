using UnityEngine;
using System.Collections;

public class Jetpack {

    protected GameObject target;
    protected GameObject spaceStation;

    protected float xCal = 0;
    protected float yCal = 0;
    protected float zCal = 0;
    protected float tvCal = 0;
    protected float tCal = 0;
    protected float tFwdCal = 0;
    protected float tRevCal = 0;

    protected float powerScale = 1.0f;



    public Jetpack(GameObject spaceStation, GameObject target, float powerScale) {
        this.target = target;
        this.spaceStation = spaceStation;
        this.powerScale = powerScale;
        CalibrateJoystick();
    }

	public void Apply() {
        var targetForce = target.GetComponent<ConstantForce>();
        Vector3 newForce = Vector3.zero;
        if (Input.GetButton("Thrust Up")) {
            newForce.y = 1f;
        }


        float joystickAmplitude = 0.1f * powerScale;
        float thrustAmplitude = 8.0f * powerScale;

        // Collect joystick input
        float joyX = (Input.GetAxis("Roll") - xCal) * joystickAmplitude;
        float joyY = (Input.GetAxis("Pitch") - yCal) * joystickAmplitude;
        float joyZ = (Input.GetAxis("Yaw") - zCal) * joystickAmplitude;
        float joyVerticalThrust = (Input.GetAxis("Thrust Vertical") - tvCal) * thrustAmplitude;
        float joyThrustFwd = (Input.GetAxis("Thrust Fwd") - tFwdCal) * thrustAmplitude;
        float joyThrustRev = (Input.GetAxis("Thrust Rev") - tRevCal) * thrustAmplitude;
        float joyThrust = joyThrustFwd - joyThrustRev;
        float joySideThrust = 0;


        /*
        if (Input.GetButton("Keyboard Roll Left")) {
            joyX += joystickAmplitude;
        }

        if (Input.GetButton("Keyboard Roll Right")) {
            joyX -= joystickAmplitude;
        }

        if (Input.GetButton("Keyboard Pitch Up")) {
            joyY -= joystickAmplitude;
        }

        if (Input.GetButton("Keyboard Pitch Down")) {
            joyY += joystickAmplitude;
        }

        if (Input.GetButton("Keyboard Yaw Left")) {
            joyZ -= joystickAmplitude;
        }

        if (Input.GetButton("Keyboard Yaw Right")) {
            joyZ += joystickAmplitude;
        }
        */

        if (Input.GetButton("Thrust Left")) {
            joySideThrust = -thrustAmplitude;
        }

        if (Input.GetButton("Thrust Right")) {
            joySideThrust = thrustAmplitude;
        }

        if (Input.GetButton("Center Joystick")) {
            CalibrateJoystick();
        }



        // Apply joystick input to torque
        var rigidbody = target.GetComponent<Rigidbody>();
        rigidbody.AddRelativeTorque(new Vector3(0, 0, joyX));
        rigidbody.AddRelativeTorque(new Vector3(joyY, 0, 0));
        rigidbody.AddRelativeTorque(new Vector3(0, joyZ, 0));

        // Slow down spin in relation to the station
        // TODO: make the power of the spin of the station proportional to how far away you are from center. 
        // spaceStation angular velocity * (% of total possible radius)
        
        Vector3 angularVelocityDelta = spaceStation.GetComponent<Rigidbody>().angularVelocity - target.GetComponent<Rigidbody>().angularVelocity;
        rigidbody.AddTorque(angularVelocityDelta * Time.fixedDeltaTime* 10);

        // Apply joystick input for thrut
        rigidbody.AddRelativeForce(new Vector3(joySideThrust, joyVerticalThrust, joyThrust)); //joyThrust        
    }

    void CalibrateJoystick() {
        xCal = Input.GetAxis("Roll");
        yCal = Input.GetAxis("Pitch");
        zCal = Input.GetAxis("Yaw");
        tvCal = Input.GetAxis("Thrust Vertical");
        tCal = Input.GetAxis("Thrust");
    }
}
