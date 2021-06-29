using UnityEngine;

public class Queen : BasePiece {
    public Queen(bool isWhitePiece, int initialX, int initialY) : base(isWhitePiece, initialX, initialY) {
        this.whitePieceName = "WhiteQueen";
        this.blackPieceName = "BlackQueen";
        this.type = PieceType.Queen;

        this.setSpritePath();
    }

    public override void onPieceSelected(GameObject[,] board, bool shouldHighlight)
    {
        this.highlightCurrentSpace(board, shouldHighlight);
        this.highlightMovementSpaces(board, shouldHighlight);
    }
    
    void highlightMovementSpaces(GameObject[,] board, bool shouldHighlight) {
        this.highlightHorizontals(board, shouldHighlight);
        this.highlightDiagonals(board, shouldHighlight);
    }
}
