using UnityEngine;

public enum PieceType {
    None,
    Bishop,
    King,
    Knight,
    Pawn,
    Queen,
    Tower,
}

public class BasePiece
{
    public string pieceSpritePath;
    public PieceType type;
    public bool isWhitePiece;
    public PlayerColor playerColor;

    protected const int MIN_INDEX = 0;
    protected const int MAX_INDEX = 7;
    protected string whitePieceName;
    protected string blackPieceName;

    public int currentX;
    public int currentY;

    Sprite sprite;

    public BasePiece(bool isWhitePiece, int initialX, int initialY) {
        this.whitePieceName = "";
        this.blackPieceName = "";
        this.isWhitePiece = isWhitePiece;
        this.playerColor = isWhitePiece ? PlayerColor.White : PlayerColor.Black;
        this.currentX = initialX;
        this.currentY = initialY;
        this.type = PieceType.None;

        this.setSpritePath();
    }

    public virtual void onPieceSelected(GameObject[,] board, bool shouldHighlight) {
        // No action on base piece
    }

    public virtual void setCurrentPosition(int x, int y) {
        this.currentX = x;
        this.currentY = y;
    }

    protected void setSpritePath() {
        string spriteName = this.blackPieceName;

        if (this.isWhitePiece) {
            spriteName = this.whitePieceName;
        }
    
        this.pieceSpritePath = "Assets/Sprites/Pieces/" + spriteName + ".png";
    }

    protected void highlightCurrentSpace(GameObject[,] board, bool shouldHighlight) {
        BoardSpaceController placeToHighlight = board[this.currentX, this.currentY].GetComponent<BoardSpaceController>();

        placeToHighlight.setCurrent(shouldHighlight);
    }

    protected void highlightAround(GameObject[,] board, bool shouldHighlight) {
        //
    }

    protected void highlightHorizontals(GameObject[,] board, bool shouldHighlight) {
        int firstLeftX = this.currentX - 1;
        int firstRightX = this.currentX + 1;
        int firstTopY = this.currentY - 1;
        int firstBottomY = this.currentY + 1;

        for (int i = firstLeftX; i >= 0; i--) {
            BoardSpaceController placeToHighlight = board[i, this.currentY].GetComponent<BoardSpaceController>();
            if (this.hasOwnPieceOnPath(placeToHighlight)) {
                break;
            } else if (this.hasEnemyPieceOnPath(placeToHighlight)) {
                placeToHighlight.setAttack(shouldHighlight);
                break;
            }

            placeToHighlight.setHighlight(shouldHighlight);
        }

        for (int i = firstRightX; i <= 7; i++) {
            BoardSpaceController placeToHighlight = board[i, this.currentY].GetComponent<BoardSpaceController>();
            if (this.hasOwnPieceOnPath(placeToHighlight)) {
                break;
            } else if (this.hasEnemyPieceOnPath(placeToHighlight)) {
                placeToHighlight.setAttack(shouldHighlight);
                break;
            }

            placeToHighlight.setHighlight(shouldHighlight);
        }

        for (int i = firstTopY; i >= 0; i--) {
            BoardSpaceController placeToHighlight = board[this.currentX, i].GetComponent<BoardSpaceController>();
            if (this.hasOwnPieceOnPath(placeToHighlight)) {
                break;
            } else if (this.hasEnemyPieceOnPath(placeToHighlight)) {
                placeToHighlight.setAttack(shouldHighlight);
                break;
            }

            placeToHighlight.setHighlight(shouldHighlight);
        }

        for (int i = firstBottomY; i <= 7; i++) {
            BoardSpaceController placeToHighlight = board[this.currentX, i].GetComponent<BoardSpaceController>();
            if (this.hasOwnPieceOnPath(placeToHighlight)) {
                break;
            } else if (this.hasEnemyPieceOnPath(placeToHighlight)) {
                placeToHighlight.setAttack(shouldHighlight);
                break;
            }

            placeToHighlight.setHighlight(shouldHighlight);
        }
    }

    protected void highlightDiagonals(GameObject[,] board, bool shouldHighlight) {
        //
    }

    protected bool hasPieceOnPath(BoardSpaceController space) {
        return space.currentPiece.type != PieceType.None;
    }

    bool hasOwnPieceOnPath(BoardSpaceController space) {
        return this.hasPieceOnPath(space) && space.currentPiece.playerColor == this.playerColor;
    }

    bool hasEnemyPieceOnPath(BoardSpaceController space) {
        return this.hasPieceOnPath(space) && space.currentPiece.playerColor != this.playerColor;
    }
}
