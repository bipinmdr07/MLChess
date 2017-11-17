using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman {
	public override bool[,] PossibleMove(){
		bool[,] r = new bool[8,8];

		// up left
		KnightMove(CurrentX - 1, CurrentY + 2, ref r);

		// up right
		KnightMove(CurrentX + 1, CurrentY + 2, ref r);

		// right up
		KnightMove(CurrentX + 2, CurrentY + 1, ref r);

		// right down
		KnightMove(CurrentX + 2, CurrentY - 1, ref r);

		// down left
		KnightMove(CurrentX - 1, CurrentY - 2, ref r);

		// down right
		KnightMove(CurrentX + 1, CurrentY - 2, ref r);

		// left up
		KnightMove(CurrentX - 2, CurrentY + 1, ref r);

		// left down
		KnightMove(CurrentX - 2, CurrentY - 1, ref r);

		return r;
	}

	public void KnightMove(int x, int y, ref bool[,] r){
		Chessman c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8) {
			c = BoardManager.Instance.Chessmans [x, y];
			if (c == null) {
				r [x, y] = true;
			}
			else if (isWhite != c.isWhite) {
				r [x, y] = true;
			}
		}
	}

	public override bool Threatened(){
		// up left
		bool[] threat = new bool[8];
		threat[0] = Check(CurrentX - 1, CurrentY + 2);

		// up right
		threat[1] = Check(CurrentX + 1, CurrentY + 2);

		// right up
		threat[2] = Check(CurrentX + 2, CurrentY + 1);

		// right down
		threat[3] = Check(CurrentX + 2, CurrentY - 1);

		// down left
		threat[4] = Check(CurrentX - 1, CurrentY - 2);

		// down right
		threat[5] = Check(CurrentX + 1, CurrentY - 2);

		// left up
		threat[6] = Check(CurrentX - 2, CurrentY + 1);

		// left down
		threat[7] = Check(CurrentX - 2, CurrentY - 1);

		foreach (bool threatened in threat) {
			if (threatened)
				return true;
		}
		return false;
	}

	private bool Check(int x, int y){
		Chessman c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8) {
			c = BoardManager.Instance.Chessmans [x, y];
			if (c != null && c.isWhite != isWhite && c.GetType() == typeof(King)) {
				return true;
			}
		}
		return false;
	}
}
