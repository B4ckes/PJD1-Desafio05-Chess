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

    protected bool isWhitePiece;
    protected string whitePieceName;
    protected string blackPieceName;

    public GameObject[,] board { get; private set; }
    public int currentX { get; set; }
    public int currentY { get; set; }
    
    PieceType type { get; } = PieceType.None;

    Sprite sprite;

    protected virtual void Awake()
    {
        this.board = GameObject.FindObjectOfType<GameController>().board;
    }

    protected BasePiece(bool isWhitePiece, int initialX, int initialY) {
        this.whitePieceName = "";
        this.blackPieceName = "";
        this.isWhitePiece = isWhitePiece;
        this.currentX = initialX;
        this.currentY = initialY;

        this.setSpritePath();
    }

    protected virtual void highlightMovementPieces() {
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
