using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour {
	private MazeConstructor _generator;

	// Use this for initialization
	void Start() {
		_generator = GetComponent<MazeConstructor>();

		_generator.GenerateNewMaze(13, 15);
	}

	// Update is called once per frame
	void Update() {

	}
}
