using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BoardSpaceController : MonoBehaviour
{
    public Image selectedBorder;
    public Image canMoveBorder;
    public Image attackBorder;
    public Image currentPieceSprite;
    public BasePiece currentPiece;

    GameObject[,] board;
    GameController gameController;

    void Awake()
    {
        this.currentPiece = new BasePiece(false, 0, 0);
        this.gameController = GameController.FindObjectOfType<GameController>();
        this.board = this.gameController.board;
    }

    public void setSelected() {
        this.gameController.setSelectedPiece(this.currentPiece);

        bool shouldSelect = !this.selectedBorder.gameObject.activeSelf;

        if (this.currentPiece.type != PieceType.None) {
            this.currentPiece.onPieceSelected(this.board, shouldSelect);
        }
    }

    public void setCurrent(bool value) {
        if (this.selectedBorder != null) {
            this.selectedBorder.gameObject.SetActive(value);
        }
    }

    public void setHighlight(bool value) {
        if (this.canMoveBorder != null) {
            this.canMoveBorder.gameObject.SetActive(value);
        }
    }

    public void setAttack(bool value) {
        if (this.attackBorder != null) {
            this.attackBorder.gameObject.SetActive(value);
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
