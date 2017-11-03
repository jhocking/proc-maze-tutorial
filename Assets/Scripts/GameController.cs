using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour {
	[SerializeField] private GameObject player;
	[SerializeField] private Text timeLabel;

	private MazeConstructor _generator;

	private DateTime startTime;
	private int timeLimit;
	private int reduceLimitBy;

	// Use this for initialization
	void Start() {
		_generator = GetComponent<MazeConstructor>();

		timeLimit = 80;
		reduceLimitBy = 5;
		startTime = DateTime.Now;

		StartNewMaze();
	}

	private void StartNewMaze() {
		_generator.GenerateNewMaze(13, 15, OnStartTrigger, OnGoalTrigger);

		float x = _generator.startCol * _generator.hallWidth;
		float y = 1;
		float z = _generator.startRow * _generator.hallWidth;
		player.transform.position = new Vector3(x, y, z);

		// restart timer
		timeLimit -= reduceLimitBy;
		startTime = DateTime.Now;
	}

	// Update is called once per frame
	void Update() {
		int timeUsed = (DateTime.Now - startTime).Seconds;
		int timeLeft = timeLimit - timeUsed;
		timeLabel.text = timeLeft.ToString();
	}

	private void OnGoalTrigger(Collider other) {
		Debug.Log("GOAL");
	}

	private void OnStartTrigger(Collider other) {
		Debug.Log("START");
	}
}
