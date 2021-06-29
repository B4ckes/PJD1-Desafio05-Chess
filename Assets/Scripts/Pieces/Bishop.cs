using UnityEngine;

public class Bishop : BasePiece {
    public Bishop(bool isWhitePiece, int initialX, int initialY) : base(isWhitePiece, initialX, initialY) {
        this.whitePieceName = "WhiteBishop";
        this.blackPieceName = "BlackBishop";
        this.type = PieceType.Bishop;

        this.setSpritePath();
    }

    public override void onPieceSelected(GameObject[,] board, bool shouldHighlight)
    {
        this.highlightCurrentSpace(board, shouldHighlight);
        this.highlightMovementSpaces(board, shouldHighlight);
    }
    
    void highlightMovementSpaces(GameObject[,] board, bool shouldHighlight) {
        this.highlightDiagonals(board, shouldHighlight);
    }
}
