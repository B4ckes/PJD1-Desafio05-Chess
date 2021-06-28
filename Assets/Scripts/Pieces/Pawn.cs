using UnityEngine;

public class Pawn : BasePiece
{
    private bool isFirstMovement;
    private const int MIN_INDEX = 0;
    private const int MAX_INDEX = 7;

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

    void highlightCurrentSpace(GameObject[,] board, bool shouldHighlight) {
        BoardSpaceController placeToHighlight = board[this.currentX, this.currentY].GetComponent<BoardSpaceController>();

        placeToHighlight.setCurrent(shouldHighlight);
    }

    void highlightMovementSpaces(GameObject[,] board, bool shouldHighlight) {
        int amountToMove = this.isFirstMovement ? 2 : 1;

        for (int i = 0; i < amountToMove; i++) {
            int movementIndex = this.isWhitePiece ? this.currentY + (i + 1) : this.currentY - (i + 1);
            bool shouldHighlightSpace = this.isWhitePiece ? movementIndex <= MAX_INDEX : movementIndex >= MIN_INDEX;

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
        int attackYIndex = this.isWhitePiece ? this.currentY + 1 : this.currentY - 1;
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

    bool hasPieceOnPath(BoardSpaceController space) {
        return space.currentPiece.type != PieceType.None;
    }
}
