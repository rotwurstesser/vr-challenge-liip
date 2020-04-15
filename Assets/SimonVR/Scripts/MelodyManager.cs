using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyManager : MonoBehaviour
{

	private System.Random randomizer = new System.Random();
	private bool playMelody = false;
	private double nextNotePlayTime;
	private int currentNoteIndex = 0; // TODO: Use List.Enumerator instead of this hand built cursor to traverse the list of note while plaing the melody.


	// Melody sequence
	public List<int> theMelody = new List<int>();

	// Mapping of the melody notes (0-5) to walls (to the wall's AudioSource). The mapping is done in the Inspector in Unity.
	public AudioSource[] noteToWallAudio;

	// Start is called before the first frame update
	void Start()
    {
		// we never need an empty melody in this game, so this initializes the melody to one note at the start of the scene
		addToMelody();
    }

    // Update is called once per frame
    void Update()
    {
		// Check if player leftclicks mous and melody not already playing and then toggles the melody to play. This is just to be able to test.
		if (Input.GetButtonDown("Fire1") & !playMelody)
		{
			playMelody = true;
			nextNotePlayTime = AudioSettings.dspTime + 0.2d;
		}

		double time = AudioSettings.dspTime;

		// We don't want to play a note at each frame (60 FPS!), but only when the note is scheduled (or very shortly thereafter) by nextNotePlayTime
		if (time > nextNotePlayTime & playMelody)
		{
			int note = theMelody[currentNoteIndex];
			noteToWallAudio[note].Play(); // This (Play instead of PlayScheduled) is not the best implementation, because it is dependent on the frame rate, but sufficient for our needs and works around the problem of PlayScheduled not able to schedule an AudioSource to play while it is already playing (same notes close together).
			currentNoteIndex++;
			nextNotePlayTime = AudioSettings.dspTime + 0.4d;

			// when last note of melody is reached, stop playing, reset melody cursor and add a not to the melody (this one is also for testing purposes only)
			if (currentNoteIndex > theMelody.Count-1)
			{
				playMelody = false;
				currentNoteIndex = 0;
				addToMelody();
			}
		}
    }

	// Append one random note to the melody
	void addToMelody()
	{
		theMelody.Add(randomizer.Next(0, 6));
	}

	void clearMelody() => theMelody.Clear();
}
