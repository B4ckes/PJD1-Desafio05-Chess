﻿using UnityEngine;

public class Pawn : BasePiece {
    public bool isFirstMovement { private get; set;}

    public Pawn(bool isWhitePiece, int initialX, int initialY) : base(isWhitePiece, initialX, initialY) {
        this.whitePieceName = "WhitePawn";
        this.blackPieceName = "BlackPawn";
        this.type = PieceType.Pawn;

        this.isFirstMovement = true;

        this.setSpritePath();
    }

    public override void onPieceSelected(GameObject[,] board, bool shouldHighlight) {
        this.highlightCurrentSpace(board, shouldHighlight);
        this.highlightMovementSpaces(board, shouldHighlight);
        this.highlightAttackSpace(board, shouldHighlight);
    }

    public override void setCurrentPosition(int x, int y) {
        this.currentX = x;
        this.currentY = y;
        this.isFirstMovement = false;
    }

    void highlightMovementSpaces(GameObject[,] board, bool shouldHighlight) {
        int amountToMove = this.isFirstMovement ? 2 : 1;

        for (int i = 0; i < amountToMove; i++) {
            int movementIndex = this.isWhitePiece ? this.currentY - (i + 1) : this.currentY + (i + 1);
            bool shouldHighlightSpace = this.isWhitePiece ? movementIndex >= MIN_INDEX : movementIndex <= MAX_INDEX;

            if (shouldHighlightSpace) {
                BoardSpaceController placeToHighlight = board[this.currentX, movementIndex].GetComponent<BoardSpaceController>();

                if (this.hasPieceOnPath(placeToHighlight)) {
                    break;
                }

                placeToHighlight.setHighlight(shouldHighlight);
            }
        }
    }

    void highlightAttackSpace(GameObject[,] board, bool shouldHighlight) {
        int attackYIndex = this.isWhitePiece ? this.currentY - 1 : this.currentY + 1;
        int leftAttackXIndex = this.currentX - 1;
        int rightAttackXIndex = this.currentX + 1;

        bool isExistentFrontLine =
            attackYIndex <= MAX_INDEX &&
            attackYIndex >= MIN_INDEX;
        bool isExistentLeftSpace = isExistentFrontLine && leftAttackXIndex >= MIN_INDEX;
        bool isExistentRightSpace = isExistentFrontLine && rightAttackXIndex <= MAX_INDEX;

        if (isExistentLeftSpace) {
            BoardSpaceController placeToHighlight = board[leftAttackXIndex, attackYIndex].GetComponent<BoardSpaceController>();

            bool canAttack = shouldHighlight && placeToHighlight.currentPiece.type != PieceType.None && placeToHighlight.currentPiece.isWhitePiece != this.isWhitePiece;

            placeToHighlight.setAttack(canAttack);
        }

        if (isExistentRightSpace) {
            BoardSpaceController placeToHighlight = board[rightAttackXIndex, attackYIndex].GetComponent<BoardSpaceController>();

            bool canAttack = shouldHighlight && placeToHighlight.currentPiece.type != PieceType.None && placeToHighlight.currentPiece.isWhitePiece != this.isWhitePiece;

            placeToHighlight.setAttack(canAttack);
        }
    }

    public override void resetPosition() {
        this.currentX = this.initialX;
        this.currentY = this.initialY;
        this.isFirstMovement = true;
    }
}
