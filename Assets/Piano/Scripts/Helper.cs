/* 
 * Andy Tran
 * Fanney Zhu
 * 
 * andytran@college.harvard.edu
 * fanneyzhu@college.harvard.edu
 * 
 * CS50 Final Project
 * Leap Motion Piano
 * 
 * Virtual piano keyboard controlled by Leap Motion Sensor.
 * 
 */

// include necessary libraries
using UnityEngine;
using System.Collections;
using Leap;

public class Helper : MonoBehaviour {

	// variables for each piano key
	[SerializeField]
	public string text;
	[SerializeField]
	public float keyPitch;

	// declare controller to detect hand movement
	Controller controller;

	void Start ()
	{
		// initialize controller
		controller = new Controller ();

		// enable controller to recognize key tap gesture
		controller.EnableGesture (Gesture.GestureType.TYPE_KEY_TAP);

		// set properties of key tap gesture
		controller.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", 4.0f);
		controller.Config.SetFloat("Gesture.KeyTap.HistorySeconds", .2f);
		controller.Config.SetFloat("Gesture.KeyTap.MinDistance", 3.0f);
		controller.Config.Save();
	}

	/* Each piano key has a 3D collider component. OnTriggerStay will run if our
	 * fingers (detected via leap motion) are within the collider space of the 
	 * piano key.
	 */
	public void OnTriggerStay (Collider other)
	{
		// get frame of controller
		Frame frame = controller.Frame ();

		// get key tap gestures detected within frame
		GestureList gestures = frame.Gestures ();

		// if no key tap gesture detected, exit method
		if (gestures.Count == 0)
		{
			return;
		}

		// else if key tap gesture is detected
		else
		{
			// console will display the note of the piano key played
			Debug.Log (text);

			/* Base note is middle C. The pitch is changed using a mathematical
			 * function found online. Each subsequent next half note is 1/12th 
			 * root above the previous. 
			 */
			audio.pitch = Mathf.Pow(2f, keyPitch/12.0f);

			// play note of piano key
			audio.Play();

			// play animation (piano key moves down when played)
			animation.Play (); 
		}
	}
}