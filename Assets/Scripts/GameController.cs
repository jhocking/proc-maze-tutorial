using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public bool showDebug;

	[SerializeField] private Material stoneMat;
	[SerializeField] private Material brickMat;

	private MazeDataGenerator _dataGenerator;
	private MazeMeshGenerator _meshGenerator;

	// Use this for initialization
	void Start() {
		_dataGenerator = new MazeDataGenerator();
		_meshGenerator = new MazeMeshGenerator();

		GenerateNewMaze(13, 15, 3.75f, 3.5f);
	}

	public void GenerateNewMaze(int sizeRows, int sizeCols, float width, float height) {
		if (sizeRows % 2 == 0 && sizeCols % 2 == 0) {
			Debug.LogError("Odd numbers work better for dungeon size.");
		}
		_dataGenerator.GenerateMaze(sizeRows, sizeCols);
		_meshGenerator.GenerateMaze(_dataGenerator.maze, width, height);

		GameObject go = new GameObject();
		go.transform.position = Vector3.zero;
		go.name = "Generated Dungeon";

		MeshFilter mf = go.AddComponent<MeshFilter>();
		mf.mesh = _meshGenerator.maze;

		MeshCollider mc = go.AddComponent<MeshCollider>();
		mc.sharedMesh = mf.mesh;

		// multiple materials for floors and walls
		MeshRenderer mr = go.AddComponent<MeshRenderer>();
		mr.materials = new Material[2] {stoneMat, brickMat};

		// TODO FindStartPosition();
	}

	// top-down debug display
	void OnGUI() {
		if (!showDebug) return;

		int[,] maze = _dataGenerator.maze;
		int rMax = maze.GetUpperBound(0);
		int cMax = maze.GetUpperBound(1);

		string msg = "";
		for (int i = rMax; i >= 0; i--) { // loop top to bottom
			for (int j = 0; j <= cMax; j++) {
				//msg += maze[i, j];
				if (maze[i, j] == 0) {
					msg += "....";
				} else {
					msg += "==";
				}
			}
			msg += "\n";
		}

		GUI.Label(new Rect(20, 20, 500, 500), msg);
	}

	// Update is called once per frame
	void Update() {

	}
}
