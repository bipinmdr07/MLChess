﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman {
	public override bool[,] PossibleMove ()
	{
		bool[,] r = new bool[8, 8];

		Chessman c;
		int i, j;

		// Top left
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i--;
			j++;
			if (i < 0 || j >= 8) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null) {
				r [i, j] = true;
			} else {
				if (isWhite != c.isWhite) {
					r [i, j] = true;
				}
				break;	
			}
		}

		// Top Right
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i++;
			j++;
			if (i >= 8 || j >= 8) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null) {
				r [i, j] = true;
			} else {
				if (isWhite != c.isWhite) {
					r [i, j] = true;
				}
				break;	
			}
		}

		// Down left
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i--;
			j--;
			if (i < 0 || j < 0) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null) {
				r [i, j] = true;
			} else {
				if (isWhite != c.isWhite) {
					r [i, j] = true;
				}
				break;	
			}
		}

		// Down Right
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i++;
			j--;
			if (i >= 8 || j < 0) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null) {
				r [i, j] = true;
			} else {
				if (isWhite != c.isWhite) {
					r [i, j] = true;
				}
				break;	
			}
		}

		return r;
	}

	public override bool Threatened(){
		Chessman c;
		int i, j;

		// Top left
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i--;
			j++;
			if (i < 0 || j >= 8) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, j];
			if (c != null && c.isWhite != isWhite) {
				if (c.GetType () == typeof(King))
					return true;
				break;
			}
		}

		// Top Right
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i++;
			j++;
			if (i >= 8 || j >= 8) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, j];
			if (c != null && c.isWhite != isWhite) {
				if (c.GetType () == typeof(King))
					return true;
				break;
			}
		}

		// Down left
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i--;
			j--;
			if (i < 0 || j < 0) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, j];
			if (c != null && c.isWhite != isWhite) {
				if (c.GetType () == typeof(King))
					return true;
				break;
			}
		}

		// Down Right
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i++;
			j--;
			if (i >= 8 || j < 0) {
				break;
			}

			c = BoardManager.Instance.Chessmans [i, j];
			if (c != null && c.isWhite != isWhite) {
				if (c.GetType () == typeof(King))
					return true;
				break;
			}
		}
		return false;
	}
}
