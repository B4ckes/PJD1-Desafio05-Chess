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
    public int positionX { private get; set; }
    public int positionY { private get; set; }

    GameObject[,] board;
    GameController gameController;

    void Awake()
    {
        this.currentPiece = new BasePiece(false, 0, 0);
        this.gameController = GameController.FindObjectOfType<GameController>();
        this.board = this.gameController.board;
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

    public void onClick() {
        bool isMoveSpace = this.canMoveBorder.gameObject.activeSelf;
        bool isAttackSpace = this.attackBorder.gameObject.activeSelf;

        if (this.gameController.winner == PlayerColor.None) {
            if (isMoveSpace) {
                this.movePiece();
                return;
            } else if (isAttackSpace) {
                if (this.currentPiece.type == PieceType.Queen) {
                    this.gameController.winner = this.gameController.selectedPiece.playerColor;
                }

                this.movePiece();
                return;
            }
            
            if (this.gameController.currentTurn == this.currentPiece.playerColor) {
                this.setSelected();
            }
        }
    }

    public void setPosition(int x, int y) {
        this.positionX = x;
        this.positionY = y;
    }

    public void setSelected() {
        this.gameController.setSelectedPiece(this.currentPiece);

        if (gameController.currentTurn == this.currentPiece.playerColor) {
            bool shouldSelect = !this.selectedBorder.gameObject.activeSelf;

            if (this.currentPiece.type != PieceType.None) {
                this.currentPiece.onPieceSelected(this.board, shouldSelect);
            }
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

    public void removePiece() {
        this.currentPiece = new BasePiece(false, 0, 0);
    }

    void clearPreviousPosition() {
        int oldSpacePositionX = this.gameController.selectedPiece.currentX;
        int oldSpacePositionY = this.gameController.selectedPiece.currentY;

        BoardSpaceController oldSpace = this.board[oldSpacePositionX, oldSpacePositionY].GetComponent<BoardSpaceController>();
        oldSpace.currentPiece.onPieceSelected(this.board, false);
        oldSpace.removePiece();
    }

    void clearBorders() {
        this.setCurrent(false);
        this.setHighlight(false);
        this.setAttack(false);
    }

    void movePiece() {
        this.clearPreviousPosition();

        this.currentPiece = this.gameController.selectedPiece;
        this.currentPiece.setCurrentPosition(this.positionX, this.positionY);

        this.gameController.setSelectedPiece(new BasePiece(false, 0, 0));
        
        this.clearBorders();

        this.gameController.changeTurn();
    }
}
