using UnityEngine;
using UnityEngine.UI;

public class Tower : BasePiece {
    public Tower(bool isWhitePiece, int initialX, int initialY) : base(isWhitePiece, initialX, initialY) {
        this.whitePieceName = "WhiteTower";
        this.blackPieceName = "BlackTower";
        this.type = PieceType.Tower;

        this.setSpritePath();
    }

    public override void onPieceSelected(GameObject[,] board, bool shouldHighlight) {
        this.highlightCurrentSpace(board, shouldHighlight);
        this.highlightMovementSpaces(board, shouldHighlight);
    }

    void highlightMovementSpaces(GameObject[,] board, bool shouldHighlight) {
        this.highlightHorizontals(board, shouldHighlight);
    }
}
