using UnityEngine;

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

    public override void onPieceSelected(GameObject[,] board, bool shouldHighlight) {
        this.highlightMovementSpaces(board, shouldHighlight);
    }

    void highlightMovementSpaces(GameObject[,] board, bool shouldHighlight) {
        int amountToMove = this.isFirstMovement ? 2 : 1;
        int minIndex = 0;
        int maxIndex = 7;

        for (int i = 0; i < amountToMove; i++) {
            int movementIndex = this.isWhitePiece ? this.currentY + (i + 1) : this.currentY - (i + 1);
            bool shouldHighlightSpace = this.isWhitePiece ? movementIndex <= maxIndex : movementIndex >= minIndex;

            if (shouldHighlightSpace) {
                BoardSpaceController placeToHighlight = board[this.currentX, movementIndex].GetComponent<BoardSpaceController>();

                if (this.hasPieceOnPath(placeToHighlight)) {
                    break;
                }

                placeToHighlight.setHighlight(shouldHighlight);
            }
        }
    }

    bool hasPieceOnPath(BoardSpaceController space) {
        return space.currentPiece.type != PieceType.None;
    }
}
