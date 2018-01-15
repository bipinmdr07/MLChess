﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman {
	public override bool[,] PossibleMove ()
	{
		bool[,] r = new bool[8, 8];

		Chessman c;
		int i, j;

		// top side
		i = CurrentX - 1;
		j = CurrentY + 1;
		if (CurrentY != 7) {
			for (int k = 0; k < 3; k++) {
				if (i >= 0 || i < 8) {
					c = BoardManager.Instance.Chessmans [i, j];
					if (c == null) {
						r [i, j] = true;
					}
					else if (isWhite != c.isWhite){
						r [i, j] = true;
					}
				}
				i++;
			}
		}

		// down side
		i = CurrentX - 1;
		j = CurrentY - 1;
		if (CurrentY != 0) {
			for (int k = 0; k < 3; k++) {
				if (i >= 0 || i < 8) {
					c = BoardManager.Instance.Chessmans [i, j];
					if (c == null) {
						r [i, j] = true;
					}
					else if (isWhite != c.isWhite){
						r [i, j] = true;
					}
				}
				i++;
			}
		}

		// middle left
		if (CurrentX != 0){
			c = BoardManager.Instance.Chessmans [CurrentX - 1, CurrentY];
			if (c == null) {
				r [CurrentX - 1, CurrentY] = true;
			}
			else if (isWhite != c.isWhite){
				r [CurrentX - 1, CurrentY] = true;
			}
		}

		// middle right
		if (CurrentX != 7){
			c = BoardManager.Instance.Chessmans [CurrentX + 1, CurrentY];
			if (c == null) {
				r [CurrentX + 1, CurrentY] = true;
			}
			else if (isWhite != c.isWhite){
				r [CurrentX + 1, CurrentY] = true;
			}
		}

		// casteling for white piece
		if (isWhite) {
			// left
			i = CurrentX;
			while (i > 0 && CurrentY == 0) {
				i--;
				c = BoardManager.Instance.Chessmans [i, CurrentY];
				if (i == 0) {
					if (c != null && c.GetType () == typeof(Rook) && c.isWhite == true)
						r [i, CurrentY] = true;
				} else {
					if (c != null)
						break;
				}
			}

			// right
			while (i < 7 && CurrentY == 0) {
				i++;
				c = BoardManager.Instance.Chessmans [i, CurrentY];
				if (i == 7) {
					if (c != null && c.GetType () == typeof(Rook) && c.isWhite == true) {
						r [i, CurrentY] = true;
					} else {
						if (c != null) {
							break;
						}
					}
				}
			}

		} else {
			// left
			i = CurrentX;
			while (i > 0 && CurrentY == 7) {
				i--;
				c = BoardManager.Instance.Chessmans [i, CurrentY];
				if (i == 0) {
					if (c != null && c.GetType () == typeof(Rook) && c.isWhite == false)
						r [i, CurrentY] = true;
				} else {
					if (c != null)
						break;
				}
			}

			// right
			while (i < 7 && CurrentY == 7) {
				i++;
				c = BoardManager.Instance.Chessmans [i, CurrentY];
				if (i == 7) {
					if (c != null && c.GetType () == typeof(Rook) && c.isWhite == false) {
						r [i, CurrentY] = true;
					} else {
						if (c != null) {
							break;
						}
					}
				}
			}
		}

		return r;
	}

	public override bool Threatened(){
		return false;
	}
}
