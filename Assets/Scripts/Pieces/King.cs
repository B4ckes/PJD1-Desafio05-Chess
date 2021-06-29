using UnityEngine;

public class King : BasePiece {
    public King(bool isWhitePiece, int initialX, int initialY) : base(isWhitePiece, initialX, initialY) {
        this.whitePieceName = "WhiteKing";
        this.blackPieceName = "BlackKing";
        this.type = PieceType.King;

        this.setSpritePath();
    }

    public override void onPieceSelected(GameObject[,] board, bool shouldHighlight)
    {
        this.highlightCurrentSpace(board, shouldHighlight);
        this.highlightMovementSpaces(board, shouldHighlight);
    }
    
    void highlightMovementSpaces(GameObject[,] board, bool shouldHighlight) {
        this.highlightAround(board, shouldHighlight);
    }
}
