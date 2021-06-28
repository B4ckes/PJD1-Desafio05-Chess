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

    public int currentX { get; set; }
    public int currentY { get; set; }

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

    protected void setSpritePath() {
        string spriteName = this.blackPieceName;

        if (this.isWhitePiece) {
            spriteName = this.whitePieceName;
        }
    
        this.pieceSpritePath = "Assets/Sprites/Pieces/" + spriteName + ".png";
    }
}
