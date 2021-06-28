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
        bool shouldSelect = !this.selectedBorder.gameObject.activeSelf;

        if (this.currentPiece.type != PieceType.None) {
            this.selectedBorder.gameObject.SetActive(shouldSelect);
            this.currentPiece.onPieceSelected(this.board, shouldSelect);
        }
    }

    public void setHighlight(bool value) {
        if (this.canMoveBorder != null) {
            this.canMoveBorder.gameObject.SetActive(value);
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
