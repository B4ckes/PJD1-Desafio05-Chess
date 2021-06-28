using System.Collections;
using System.Collections.Generic;
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
    public Turn turn;

    protected string whitePieceName;
    protected string blackPieceName;

    public int currentX;
    public int currentY;

    Sprite sprite;

    public BasePiece(bool isWhitePiece, int initialX, int initialY) {
        this.whitePieceName = "";
        this.blackPieceName = "";
        this.isWhitePiece = isWhitePiece;
        this.turn = isWhitePiece ? Turn.White : Turn.Black;
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
}
