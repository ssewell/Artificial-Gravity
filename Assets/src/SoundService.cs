using UnityEngine;
using System;
using System.Collections;

public class SoundService : MonoBehaviour { 

    public AudioSource[] AudioSources;

    protected float[] smoothing;

    public void Start() {
        smoothing = new float[AudioSources.Length];
    }

    // Soon to be deprecated. All joystick sounds should go through PlayAudioForValue
    public void PlayAudioForJoystick(int soundID, float magnitude, float smoothing) {
        magnitude = Math.Abs(magnitude);
        magnitude = smoothe(soundID, magnitude, smoothing);
        var audio = audioSource(soundID);
        audio.volume = GetValueInRange(magnitude, 0, 0.5f);
        audio.pitch = GetValueInRange(magnitude, 1.0f, 1.6f);   
        audio.GetComponent<AudioLowPassFilter>().cutoffFrequency = GetValueInRange(magnitude, 10, 5000);
        playAudio(audio);
    }

    public void PlayAudioForValue(int soundID, float magnitude, float smoothing, float minVolume, float maxVolume, float minPitch, float maxPitch, float minFilter, float maxFilter) {
        var audio = audioSource(soundID);
        magnitude = smoothe(soundID, magnitude, smoothing);
        audio.volume = GetValueInRange(magnitude, minVolume, maxVolume);
        audio.pitch = GetValueInRange(magnitude, minPitch, maxPitch);
        audio.GetComponent<AudioLowPassFilter>().cutoffFrequency = GetValueInRange(magnitude, minFilter, maxFilter);
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
        if (!audio.isPlaying && audio.isActiveAndEnabled) {
            audio.Play();
        }
    }

    protected float GetValueInRange(float value, float low, float high) {
        return low + (high - low) * value;
    }
}
