using System.Collections.Generic;
using UnityEngine;

public class Knight : BasePiece {
    public Knight(bool isWhitePiece, int initialX, int initialY) : base(isWhitePiece, initialX, initialY) {
        this.whitePieceName = "WhiteKnight";
        this.blackPieceName = "BlackKnight";
        this.type = PieceType.Knight;

        this.setSpritePath();
    }

    public override void onPieceSelected(GameObject[,] board, bool shouldHighlight)
    {
        this.highlightCurrentSpace(board, shouldHighlight);
        this.highlightMovementSpaces(board, shouldHighlight);
    }

    void highlightMovementSpaces(GameObject[,] board, bool shouldHighlight) {
        int topLeftX = this.currentX - 1;
        int topLeftY = this.currentY - 2;
        
        int topRightX = this.currentX + 1;
        int topRightY = this.currentY - 2;

        int rightTopX = this.currentX + 2;
        int rightTopY = this.currentY - 1;

        int rightBottomX = this.currentX + 2;
        int rightBottomY = this.currentY + 1;

        int bottomLeftX = this.currentX - 1;
        int bottomLeftY = this.currentY + 2;

        int bottomRightX = this.currentX + 1;
        int bottomRightY = this.currentY + 2;

        int leftTopX = this.currentX - 2;
        int leftTopY = this.currentY - 1;

        int leftBottomX = this.currentX - 2;
        int leftBottomY = this.currentY + 1;

        List<BoardSpaceController> places = new List<BoardSpaceController>();

        if (topLeftX >= MIN_INDEX && topLeftY >= MIN_INDEX) {
            places.Add(board[topLeftX, topLeftY].GetComponent<BoardSpaceController>());
        }

        if (topRightX <= MAX_INDEX && topRightY >= MIN_INDEX) {
            places.Add(board[topRightX, topRightY].GetComponent<BoardSpaceController>());
        }

        if (rightTopX <= MAX_INDEX && rightTopY >= MIN_INDEX) {
            places.Add(board[rightTopX, rightTopY].GetComponent<BoardSpaceController>());
        }

        if (rightBottomX <= MAX_INDEX && rightBottomY <= MAX_INDEX) {
            places.Add(board[rightBottomX, rightBottomY].GetComponent<BoardSpaceController>());
        }

        if (bottomLeftX >= MIN_INDEX && bottomLeftY <= MAX_INDEX) {
            places.Add(board[bottomLeftX, bottomLeftY].GetComponent<BoardSpaceController>());
        }

        if (bottomRightX <= MAX_INDEX && bottomRightY <= MAX_INDEX) {
            places.Add(board[bottomRightX, bottomRightY].GetComponent<BoardSpaceController>());
        }

        if (leftTopX >= MIN_INDEX && leftTopY >= MIN_INDEX) {
            places.Add(board[leftTopX, leftTopY].GetComponent<BoardSpaceController>());
        }

        if (leftBottomX >= MIN_INDEX && leftBottomY <= MAX_INDEX) {
            places.Add(board[leftBottomX, leftBottomY].GetComponent<BoardSpaceController>());
        }

        foreach (BoardSpaceController place in places) {
            if (!this.hasPieceOnPath(place)) {
                place.setHighlight(shouldHighlight);
            } else if (this.hasEnemyPieceOnPath(place)) {
                place.setAttack(shouldHighlight);
            }
        }
    }
}
