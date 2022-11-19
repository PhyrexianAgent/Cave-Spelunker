using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public Sounds[] sounds;

	void Awake ()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		} else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sounds s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.SetSource();
		}
	}

	public void Play(string sound)
	{
		Sounds s = Array.Find(sounds, item => item.name == sound);
		s.source.Play();
	}
	public void Stop(string sound)
	{
		Sounds s = Array.Find(sounds, item => item.name == sound);
		s.source.Stop();
	}

}
