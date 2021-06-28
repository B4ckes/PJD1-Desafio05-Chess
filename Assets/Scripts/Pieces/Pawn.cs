using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Pawn : BasePiece
{
    private bool isFirstMovement;

    public Pawn(bool isWhitePiece, int initialX, int initialY) : base(isWhitePiece, initialX, initialY) {
        this.whitePieceName = "WhitePawn";
        this.blackPieceName = "BlackPawn";
        this.type = PieceType.Pawn;

        this.isFirstMovement = true;

        this.setSpritePath();
    }

    public override void highlightMovementPieces(GameObject[,] board) {
        int amountToMove = this.isFirstMovement ? 2 : 1;

        if (board != null) {
            if (this.isWhitePiece) {
                for (int i = 0; i < amountToMove; i++) {
                    GameObject placeToHighlight = board[this.currentX, this.currentY + (i + 1)];
                    placeToHighlight.GetComponent<BoardSpaceController>().activeHighlight();
                }

                return;
            }

            for (int i = 0; i < amountToMove; i++) {
                GameObject placeToHighlight = board[this.currentX, this.currentY - (i + 1)];
                placeToHighlight.GetComponent<BoardSpaceController>().activeHighlight();
            }
        }
    }
}
