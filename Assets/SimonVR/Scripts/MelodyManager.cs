using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyManager : MonoBehaviour
{

	private System.Random randomizer = new System.Random();

	// Melody sequence
	public List<int> theMelody = new List<int>();

	// Mapping of notes to walls
	public AudioSource[] noteToWall;

	// Start is called before the first frame update
	void Start()
    {
		addToMelody();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("Fire1"))
		{
			playMelody();
			addToMelody();
		}
    }

	void addToMelody()
	{
		theMelody.Add(randomizer.Next(0, 5));
	}

	void clearMelody() => theMelody.Clear();

	// TODO: Problem with scheduling the same audio source twice (not working)
	void playMelody()
	{
		double nextEventTime = AudioSettings.dspTime + 1.0f;
		foreach(int note in theMelody)
		{
			nextEventTime = nextEventTime + 0.8f;
			noteToWall[note].PlayScheduled(nextEventTime);
		}
	}
}
