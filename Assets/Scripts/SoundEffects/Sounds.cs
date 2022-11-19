using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds {

	public string name;

	public AudioClip clip;
	public AudioMixerGroup mixer;

	[Range(0f, 1f)]
	public float volume = 1;

	[Range(-3f, 3f)]
	public float pitch = 1;

	public bool loop = false;

	[HideInInspector]
	public AudioSource source;

	public void SetSource()
    {
		source.volume = volume;
		source.clip = clip;
		source.pitch = pitch;
		source.loop = loop;
    }

}
