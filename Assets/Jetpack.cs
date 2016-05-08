using UnityEngine;
using System.Collections;

public class Jetpack {

    protected GameObject target;
    protected GameObject spaceStation;

    float xCal = 0;
    float yCal = 0;
    float zCal = 0;
    float tvCal = 0;
    float tCal = 0;
    float tFwdCal = 0;
    float tRevCal = 0;



    public Jetpack(GameObject spaceStation, GameObject target) {
        this.target = target;
        this.spaceStation = spaceStation;
        CalibrateJoystick();
    }

	public void Apply() {
        var targetForce = target.GetComponent<ConstantForce>();
        Vector3 newForce = Vector3.zero;
        if (Input.GetButton("Thrust Up")) {
            newForce.y = 1f;
        }


        float joystickAmplitude = 0.1f;
        float thrustAmplitude = 8.0f;

        // Collect joystick input
        float joyX = (Input.GetAxis("Roll") - xCal) * joystickAmplitude;
        float joyY = (Input.GetAxis("Pitch") - yCal) * joystickAmplitude;
        float joyZ = (Input.GetAxis("Yaw") - zCal) * joystickAmplitude;
        float joyVerticalThrust = (Input.GetAxis("Thrust Vertical") - tvCal) * thrustAmplitude;
        //float joyThrust = (Input.GetAxis("Thrust") - tCal) * thrustAmplitude;
        float joyThrustFwd = (Input.GetAxis("Thrust Fwd") - tFwdCal) * thrustAmplitude;
        float joyThrustRev = (Input.GetAxis("Thrust Rev") - tRevCal) * thrustAmplitude;
        float joyThrust = joyThrustFwd - joyThrustRev;
        

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
        rigidbody.AddRelativeForce(new Vector3(0, joyVerticalThrust, joyThrust)); //joyThrust







        targetForce.relativeForce += newForce;        
    }

    void CalibrateJoystick() {
        xCal = Input.GetAxis("Roll");
        yCal = Input.GetAxis("Pitch");
        zCal = Input.GetAxis("Yaw");
        tvCal = Input.GetAxis("Thrust Vertical");
        tCal = Input.GetAxis("Thrust");
    }
}
