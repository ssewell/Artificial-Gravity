using UnityEngine;
using System.Collections;

public class Jetpack {

    public Jetpack(GameObject target, float powerScale, Character character) {
        this.target = target;
        this.powerScale = powerScale;
        CalibrateJoystick();
        sound = character.Sound;
    }

	public void Apply() {
        Vector3 newForce = Vector3.zero;
        if (Input.GetButton("Thrust Up")) {
            newForce.y = 1f;
        }


        float joystickAmplitude = 0.1f * powerScale;
        float thrustAmplitude = 8.0f * powerScale;

        // Collect joystick input
        float joyX = Input.GetAxis("Roll") - xCal;
        float joyY = Input.GetAxis("Pitch") - yCal;
        float joyZ = Input.GetAxis("Yaw") - zCal;
        float joyVerticalThrust = Input.GetAxis("Thrust Vertical") - tvCal;
        float joyThrustFwd = Input.GetAxis("Thrust Fwd") - tFwdCal;
        float joyThrustRev = Input.GetAxis("Thrust Rev") - tRevCal;
        float joyThrust = (joyThrustFwd - joyThrustRev) / 2;
        float joySideThrust = 0;
        
        if (Input.GetButton("Thrust Left")) {
            joySideThrust = -1;
        }

        if (Input.GetButton("Thrust Right")) {
            joySideThrust = 1;
        }



        if (Input.GetButton("Thrust Up")) {
            joyVerticalThrust = 1;
        }

        if (Input.GetButton("Thrust Down")) {
            joyVerticalThrust = -1;
        }

        if (Input.GetButton("Thrust Fwd")) {
            joyThrust = 1;
        }

        if (Input.GetButton("Thrust Rev")) {
            joyThrust = -1;
        }

        if (Input.GetButton("Thrust Left")) {
            joySideThrust = 1;
        }

        if (Input.GetButton("Thrust Right")) {
            joySideThrust = -1;
        }

        if (Input.GetButton("Roll Left")) {
            joyX = 1;
        }

        if (Input.GetButton("Roll Right")) {
            joyX = -1;
        }

        if (Input.GetButton("Pitch Down")) {
            joyY = 1;
        }

        if (Input.GetButton("Pitch Up")) {
            joyY = -1;
        }

        if (Input.GetButton("Yaw Left")) {
            joyZ = -1;
        }

        if (Input.GetButton("Yaw Right")) {
            joyZ = 1;
        }


        if (Input.GetButton("Center Joystick")) {
            CalibrateJoystick();
        }



        // Apply joystick input to torque
        var rigidbody = target.GetComponent<Rigidbody>();
        rigidbody.AddRelativeTorque(new Vector3(joyY * joystickAmplitude, joyZ * joystickAmplitude, joyX * joystickAmplitude));

        // Apply joystick input for thrut
        rigidbody.AddRelativeForce(new Vector3(joySideThrust * thrustAmplitude, joyVerticalThrust * thrustAmplitude, joyThrust * thrustAmplitude));

        // Play Jetpack sounds
        float smoothing = 0.95f;

        if (joyVerticalThrust < 0) {
            sound.PlayAudioForJoystick(0, joyVerticalThrust, smoothing);
            sound.PlayAudioForJoystick(1, 0, smoothing);
        } else {
            sound.PlayAudioForJoystick(0, 0, smoothing);
            sound.PlayAudioForJoystick(1, joyVerticalThrust, smoothing);
        }

        if (joyThrust < 0) {
            sound.PlayAudioForJoystick(4, joyThrust, smoothing);
            sound.PlayAudioForJoystick(5, 0, smoothing);
        } else {
            sound.PlayAudioForJoystick(4, 0, smoothing);
            sound.PlayAudioForJoystick(5, joyThrust, smoothing);
        }


        if (joySideThrust < 0) {
            sound.PlayAudioForJoystick(2, joySideThrust, smoothing);
            sound.PlayAudioForJoystick(3, 0, smoothing);
        } else {
            sound.PlayAudioForJoystick(2, 0, smoothing);
            sound.PlayAudioForJoystick(3, joySideThrust, smoothing);
        }

        smoothing = 0.8f;
        sound.PlayAudioForJoystick(6, joyX, smoothing);
        sound.PlayAudioForJoystick(7, joyY, smoothing);
        sound.PlayAudioForJoystick(8, joyZ, smoothing);


    }

    void CalibrateJoystick() {
        xCal = Input.GetAxis("Roll");
        yCal = Input.GetAxis("Pitch");
        zCal = Input.GetAxis("Yaw");
        tvCal = Input.GetAxis("Thrust Vertical");
        tCal = Input.GetAxis("Thrust");
    }

    protected GameObject target;

    protected float xCal = 0;
    protected float yCal = 0;
    protected float zCal = 0;
    protected float tvCal = 0;
    protected float tCal = 0;
    protected float tFwdCal = 0;
    protected float tRevCal = 0;
    protected float powerScale = 1.0f;

    protected SoundService sound;
}
