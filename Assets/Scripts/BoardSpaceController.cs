using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BoardSpaceController : MonoBehaviour
{
    public Image selectedBorder;
    public Image canMoveBorder;
    public Image currentPieceSprite;
    public BasePiece currentPiece;

    GameObject[,] board;

    void Awake()
    {
        this.currentPiece = new BasePiece(false, 0, 0);
        this.board = GameController.FindObjectOfType<GameController>().board;
    }

    public void setSelected() {
        if (this.currentPiece.type != PieceType.None) {
            this.selectedBorder.gameObject.SetActive(true);
            this.currentPiece.highlightMovementPieces(this.board);
        }
    }

    public void activeHighlight() {
        if (this.canMoveBorder != null) {
            this.canMoveBorder.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (this.currentPiece.type != PieceType.None) {
            this.currentPieceSprite.gameObject.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(this.currentPiece.pieceSpritePath);
            this.currentPieceSprite.gameObject.SetActive(true);
        } else {
            this.currentPieceSprite.gameObject.SetActive(false);
        }
    }
}
