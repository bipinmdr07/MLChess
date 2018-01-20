using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman {
	private int prev_pos_x = -1;
	private int prev_pos_y = -1;
	public bool moved = false;
	public override bool[,] PossibleMove(){
		bool[,] r = new bool[8, 8];
		Chessman c;
		int i;

		// Rook is moved
		if (prev_pos_x >= 0 && prev_pos_y >= 0) {
			if (CurrentX != prev_pos_x || CurrentY != prev_pos_y) {
				moved = true;
			}
		} else {
			prev_pos_x = CurrentX;
			prev_pos_y = CurrentY;
		}

		// Right
		i = CurrentX;
		while (true) {
			i++;
			if (i >= 8) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, CurrentY];
			if (c == null) {
				r [i, CurrentY] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [i, CurrentY] = true;
				}
				break;
			}
		}

		// Left
		i = CurrentX;
		while (true) {
			i--;
			if (i < 0) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, CurrentY];
			if (c == null) {
				r [i, CurrentY] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [i, CurrentY] = true;
				}
				break;
			}
		}

		// Up
		i = CurrentY;
		while (true) {
			i++;
			if (i >= 8) {
				break;
			}

			c = BoardManager.Instance.Chessmans [CurrentX, i];
			if (c == null) {
				r [CurrentX, i] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [CurrentX, i] = true;
				}
				break;
			}
		}

		// Left
		i = CurrentY;
		while (true) {
			i--;
			if (i < 0) {
				break;
			}

			c = BoardManager.Instance.Chessmans [CurrentX, i];
			if (c == null) {
				r [CurrentX, i] = true;
			} else {
				if (c.isWhite != isWhite) {
					r [CurrentX, i] = true;
				}
				break;
			}
		}
		return r;
	}

	public override bool Threatened(){
		Chessman c;
		int i;
		// Right
		i = CurrentX;
		while (true) {
			i++;
			if (i >= 8) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, CurrentY];
			if (c != null && c.isWhite != isWhite) {
				if (c.GetType() == typeof(King))
					return true;
				break;
			}
		}

		// Left
		i = CurrentX;
		while (true) {
			i--;
			if (i < 0) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, CurrentY];
			if (c != null && c.isWhite != isWhite) {
				if (c.GetType() == typeof(King))
					return true;
				break;
			}
		}

		// Up
		i = CurrentY;
		while (true) {
			i++;
			if (i >= 8) {
				break;
			}

			c = BoardManager.Instance.Chessmans [CurrentX, i];
			if (c != null && c.isWhite != isWhite) {
				if (c.GetType() == typeof(King))
					return true;
				break;
			}
		}

		// Left
		i = CurrentY;
		while (true) {
			i--;
			if (i < 0) {
				break;
			}

			c = BoardManager.Instance.Chessmans [CurrentX, i];
			if (c != null && c.isWhite != isWhite) {
				if (c.GetType () == typeof(King))
					return true;
				break;
			}
		}
		return false;
	}

	protected bool IsRookMoved(){
		return moved;
	}
}
