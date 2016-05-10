using UnityEngine;
using System;
using System.Collections;

public class SoundService : MonoBehaviour { 

    public AudioSource[] AudioSources;

    protected float[] smoothing;

    public void Start() {
        smoothing = new float[AudioSources.Length];
    }

	public void PlayAudioForJoystick(int soundID, float magnitude, float smoothing) {
        magnitude = Math.Abs(magnitude);
        magnitude = smoothe(soundID, magnitude, smoothing);
        var audio = audioSource(soundID);
        audio.volume = GetValueInRange(magnitude, 0, 1);
        audio.pitch = GetValueInRange(magnitude, 1.0f, 1.6f);   
        audio.GetComponent<AudioLowPassFilter>().cutoffFrequency = GetValueInRange(magnitude, 10, 5000);
        playAudio(audio);
    }

    protected float smoothe(int soundID, float magnitude, float smoothing) {
        this.smoothing[soundID] = this.smoothing[soundID] * smoothing + magnitude * (1 - smoothing);
        return this.smoothing[soundID];
    }

    protected AudioSource audioSource(int soundID) {
        return AudioSources[soundID];
    }

    protected void playAudio(AudioSource audio) {        
        audio.loop = true;
        if (!audio.isPlaying) {
            audio.Play();
        }
    }

    protected float GetValueInRange(float value, float low, float high) {
        return low + (high - low) * value;
    }
}
