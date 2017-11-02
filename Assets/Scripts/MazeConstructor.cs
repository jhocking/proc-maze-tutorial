using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour {
	public bool showDebug;

	[SerializeField] private Material mazeMat1;
	[SerializeField] private Material mazeMat2;
	[SerializeField] private Material startMat;
	[SerializeField] private Material treasureMat;

	public int[,] data { get; private set; }

	public float hallWidth { get; private set; }
	public float hallHeight { get; private set; }

	public int startRow { get; private set; }
	public int startCol { get; private set; }

	public int goalRow { get; private set; }
	public int goalCol { get; private set; }

	private MazeDataGenerator _dataGenerator;
	private MazeMeshGenerator _meshGenerator;

	void Awake() {
		_dataGenerator = new MazeDataGenerator();
		_meshGenerator = new MazeMeshGenerator();

		// default to walls surrounding a single empty cell
		data = new int[,] {
			{1, 1, 1},
			{1, 0, 1},
			{1, 1, 1}
		};
	}

	public void GenerateNewMaze(int sizeRows, int sizeCols) {
		if (sizeRows % 2 == 0 && sizeCols % 2 == 0) {
			Debug.LogError("Odd numbers work better for dungeon size.");
		}

		data = _dataGenerator.FromDimensions(sizeRows, sizeCols);

		FindStartPosition();
		FindGoalPosition();

		// store values used to generate this mesh
		hallWidth = _meshGenerator.width;
		hallHeight = _meshGenerator.height;

		PlaceStartTrigger();
		PlaceGoalTrigger();

		DisplayMaze();
	}

	private void DisplayMaze() {
		GameObject go = new GameObject();
		go.transform.position = Vector3.zero;
		go.name = "Generated Dungeon";

		MeshFilter mf = go.AddComponent<MeshFilter>();
		mf.mesh = _meshGenerator.FromData(data);

		MeshCollider mc = go.AddComponent<MeshCollider>();
		mc.sharedMesh = mf.mesh;

		MeshRenderer mr = go.AddComponent<MeshRenderer>();
		mr.materials = new Material[2] {mazeMat1, mazeMat2};
	}

	private void FindStartPosition() {
		int[,] maze = data;
		int rMax = maze.GetUpperBound(0);
		int cMax = maze.GetUpperBound(1);

		for (int i = 0; i <= rMax; i++) {
			for (int j = 0; j <= cMax; j++) {
				if (maze[i, j] == 0) {
					startRow = i;
					startCol = j;
					return;
				}
			}
		}
	}

	private void FindGoalPosition() {
		int[,] maze = data;
		int rMax = maze.GetUpperBound(0);
		int cMax = maze.GetUpperBound(1);

		for (int i = 0; i <= rMax; i++) {
			for (int j = cMax; j >= 0; j--) { // loop right to left
				if (maze[i, j] == 0) {
					goalRow = i;
					goalCol = j;
					return;
				}
			}
		}
	}

	private void PlaceStartTrigger() {
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.transform.position = new Vector3(startCol * hallWidth, .5f, startRow * hallWidth);
		go.name = "Start Trigger";

		go.GetComponent<BoxCollider>().isTrigger = true;
		go.GetComponent<MeshRenderer>().sharedMaterial = startMat;
	}

	private void PlaceGoalTrigger() {
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.transform.position = new Vector3(goalCol * hallWidth, .5f, goalRow * hallWidth);
		go.name = "Treasure";

		go.GetComponent<BoxCollider>().isTrigger = true;
		go.GetComponent<MeshRenderer>().sharedMaterial = treasureMat;
	}

	// top-down debug display
	void OnGUI() {
		if (!showDebug) return;

		int[,] maze = data;
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
}
