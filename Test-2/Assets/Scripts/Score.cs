using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static int score;
	private Text scoreText;

	// Use this for initialization
	void Start () {
		score = 0;
		scoreText = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = score + " points";
	}
}
