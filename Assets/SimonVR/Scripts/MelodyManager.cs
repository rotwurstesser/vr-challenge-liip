using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyManager : MonoBehaviour
{

	private System.Random randomizer = new System.Random();
	private bool playing = false;
	private double nextEventTime;
	private int currentNoteIndex = 0;
	private int note = 0;


	// Melody sequence
	public List<int> theMelody = new List<int>();

	// Mapping of notes to walls (to the wall's AudioSource)
	public AudioSource[] noteToWall;

	// Start is called before the first frame update
	void Start()
    {
		addToMelody();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("Fire1") & !playing)
		{
			playing = true;
			nextEventTime = AudioSettings.dspTime + 0.2d;
		}

		double time = AudioSettings.dspTime;

		if (time > nextEventTime & playing)
		{
			note = theMelody[currentNoteIndex];
			//noteToWall[note].PlayScheduled(nextEventTime);
			noteToWall[note].Play();
			//Debug.Log(note + " at " + nextEventTime);
			currentNoteIndex++;
			nextEventTime = AudioSettings.dspTime + 0.4d;
			if (currentNoteIndex > theMelody.Count-1)
			{
				addToMelody();
				currentNoteIndex = 0;
				playing = false;
			}
		}
    }

	void addToMelody()
	{
		theMelody.Add(randomizer.Next(0, 5));
	}

	void clearMelody() => theMelody.Clear();
}
