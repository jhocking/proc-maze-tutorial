using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour {
	[SerializeField] private GameObject player;

	private MazeConstructor _generator;

	// Use this for initialization
	void Start() {
		_generator = GetComponent<MazeConstructor>();
		_generator.GenerateNewMaze(13, 15);

		float x = _generator.startCol * _generator.hallWidth;
		float y = 1;
		float z = _generator.startRow * _generator.hallWidth;
		player.transform.position = new Vector3(x, y, z);
	}

	// Update is called once per frame
	void Update() {

	}
}
