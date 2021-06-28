using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Pawn : BasePiece
{
    PieceType type { get; } = PieceType.Pawn;

    private bool isFirstMovement = true;

    public Pawn(bool isWhitePiece, int initialX, int initialY) : base(isWhitePiece, initialX, initialY) {
        this.whitePieceName = "WhitePawn";
        this.blackPieceName = "BlackPawn";

        this.setSpritePath();
    }

    protected override void highlightMovementPieces() {
        int amountToMove = this.isFirstMovement ? 2 : 1;

        if (this.isWhitePiece) {
            for (int i = 0; i < amountToMove; i++) {
                GameObject placeToHighlight = this.board[this.currentX, this.currentY + (i + 1)];
            }

            return;
        }

        for (int i = 0; i < amountToMove; i++) {
            GameObject placeToHighlight = this.board[this.currentX, this.currentY - (i + 1)];
        }
    }
}
