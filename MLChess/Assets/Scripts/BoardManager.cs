using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class for managing the chess board and Chessmans
 **/

public class BoardManager : MonoBehaviour {
	public static BoardManager Instance{ set; get; }
	private bool[,] allowedMoves{ set; get; }

	public Chessman[,] Chessmans{ set; get; }
	private Chessman selectedChessman;

	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;

	private int selectionX = -1;
	private int selectionY = 1;

	public List<GameObject> chessmanPrefabs;
	private List<GameObject> activeChessman;

	public bool isWhiteTurn = true;

	public int[] EnPassantMove { set; get;}

	private void Start(){
		isWhiteTurn = true;
		Instance = this;
		SpawnAllChessmans ();
	}

	private void Update()
	{
		UpdateSelection ();
		DrawChessBoard ();

		if (Input.GetMouseButtonUp (0)) {
			if (selectionX >= 0 && selectionY >= 0) {
				if (selectedChessman == null) {
					SelectChessman (selectionX, selectionY);
				} else {
					// move the chessman
					MoveChessman(selectionX, selectionY);
				}
			}
		}
	}

	// method for selecting the chessman at clicking cell
	private void SelectChessman(int x, int y){
		if (Chessmans [x, y] == null) {
			return;
		}

		if (Chessmans [x, y].isWhite != isWhiteTurn)
			return;

		bool hasAtLeastOneMove = false;
		allowedMoves = Chessmans [x, y].PossibleMove ();
		for (int i = 0; i < 8; i++)
			for (int j = 0; j < 8; j++)
				if (allowedMoves [i, j])
					hasAtLeastOneMove = true;

		if (!hasAtLeastOneMove)
			return;

		selectedChessman = Chessmans [x, y];
		BoardHighlights.Instance.HighlightAllowedMoves (allowedMoves);
	}


	// method for moving the chess piece on another cell
	private void MoveChessman(int x, int y){
		if (allowedMoves[x, y]) {
			Chessman c = Chessmans [x, y];

			if (c != null && c.isWhite != isWhiteTurn) {
				// capture the piece

				// if it is a king
				if (c.GetType () == typeof(King)) {
					EndGame ();
					return;
				}

				activeChessman.Remove (c.gameObject);
				Destroy (c.gameObject);
			}

			if (x == EnPassantMove [0] && y == EnPassantMove [1]) {
				// white turn
				if (isWhiteTurn) {
					c = Chessmans [x, y - 1];
				} else {
					c = Chessmans [x, y + 1];
				}
				activeChessman.Remove (c.gameObject);
				Destroy (c.gameObject);
			}

			EnPassantMove [0] = -1;
			EnPassantMove [1] = -1;
			if (selectedChessman.GetType () == typeof(Pawn)) {
				// time for promotion
				// white
				if (y == 7) {
					activeChessman.Remove (selectedChessman.gameObject);
					Destroy (selectedChessman.gameObject);
					SpawnChessman (1, x, y, -90);
					selectedChessman = Chessmans [x, y];
				} else if (y == 0) {
					activeChessman.Remove (selectedChessman.gameObject);
					Destroy (selectedChessman.gameObject);
					SpawnChessman(7, x, y, -90);
					selectedChessman = Chessmans [x, y];
				}

				if (selectedChessman.CurrentY == 1 && y == 3) {
					EnPassantMove [0] = x;
					EnPassantMove [1] = y - 1;
				}
				else if (selectedChessman.CurrentY == 6 && y == 4) {
					EnPassantMove [0] = x;
					EnPassantMove [1] = y + 1;
				}
			}

			Chessmans [selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
			selectedChessman.transform.position = GetTileCenter (x, y);
			selectedChessman.SetPosition (x, y);
			Chessmans [x, y] = selectedChessman;
			isWhiteTurn = !isWhiteTurn;
		}
		// Check status
		if (selectedChessman.Threatened()){
			Debug.Log ("Check");
		}
		BoardHighlights.Instance.hideHighlights ();
		selectedChessman = null;
	}

	// method for drawing the chessboard outline using Gizmos
	private void DrawChessBoard()
	{
		Vector3 widthLine = Vector3.right * 8;
		Vector3 heightLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++) {
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine, Color.white);
			for (int j = 0; j <= 8; j++) {
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine, Color.white);
			}
		}

		// drawing the selection line 'X' on pointed cell
		if (selectionX >= 0 && selectionY >= 0) {
			Debug.DrawLine (
				Vector3.forward * selectionY + Vector3.right * selectionX,
				Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

			Debug.DrawLine (
				Vector3.forward * ( selectionY + 1 ) + Vector3.right * selectionX,
				Vector3.forward * selectionY + Vector3.right * ( selectionX + 1 )
			);
		}
	}

	// method for instantiating the chessman prefab according to the parameter given
	private void SpawnChessman(int index, int x, int y, int rotation_angle){
		GameObject go = Instantiate (chessmanPrefabs [index], GetTileCenter(x, y), Quaternion.identity) as GameObject;
		go.transform.Rotate (Vector3.right * rotation_angle);
		//go.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
		go.transform.SetParent (transform);

		Chessmans [x, y] = go.GetComponent<Chessman> ();
		Chessmans [x, y].SetPosition (x, y);
		/*
		Chessman script = go.GetComponent<Chessman> ();
		if (!script.isWhite) {
			go.transform.Rotate (Vector3.forward * 180);
		}
		*/

		activeChessman.Add (go);
	}

	private void UpdateSelection(){
		if (!Camera.main)
			return;

		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("ChessPlane"))) {
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		} else {
			selectionX = -1;
			selectionY = -1;
		}
	}

	private Vector3 GetTileCenter(int x, int y){
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * y) + TILE_OFFSET;
		return origin;
	}

	private void SpawnAllChessmans(){
		activeChessman = new List<GameObject> ();
		Chessmans = new Chessman[8, 8];
		EnPassantMove = new int[2]{-1, -1};
		// Spwan white chessmans
		// king
		SpawnChessman(0, 4, 0, -90);
		// queen
		SpawnChessman(1, 3, 0, -90);
		// rook
		SpawnChessman(2, 0, 0, -90);
		SpawnChessman(2, 7, 0, -90);
		// bishop
		SpawnChessman(3, 2, 0, 90);
		SpawnChessman(3, 5, 0, -90);
		// knight
		SpawnChessman(4, 1, 0, 0);
		SpawnChessman(4, 6, 0, 0);
		// pawns
		for (int i = 0; i < 8; i++) {
			SpawnChessman(5, i, 1, -90);
		}

		// Spwan black chessmans
		// king
		SpawnChessman(6, 4, 7, -90);
		// queen
		SpawnChessman(7, 3, 7, -90);
		// rook
		SpawnChessman(8, 0, 7, -90);
		SpawnChessman(8, 7, 7, -90);
		// bishop
		SpawnChessman(9, 2, 7, -90);
		SpawnChessman(9, 5, 7, -90);
		// knight
		SpawnChessman(10, 1, 7, 0);
		SpawnChessman(10, 6, 7, 0);
		// pawns
		for (int i = 0; i < 8; i++) {
			SpawnChessman(11, i, 6, -90);
		}
	}

	private void EndGame(){
		if (isWhiteTurn) {
			Debug.Log ("White team wins.");
		} else {
			Debug.Log ("Black team wins.");
		}

		foreach (GameObject go in activeChessman) {
			Destroy (go);
		}

		isWhiteTurn = true;
		BoardHighlights.Instance.hideHighlights ();
		SpawnAllChessmans ();
	}
}
