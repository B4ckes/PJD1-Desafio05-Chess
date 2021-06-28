using UnityEngine;
using UnityEngine.UI;

public enum PlayerColor {
    None,
    White,
    Black,
}

public class GameController : MonoBehaviour
{
    public GameObject BoardSpacePrefab;
    public GameObject[,] board { get; private set; } = new GameObject[8, 8];
    public PlayerColor currentTurn;
    public PlayerColor winner;

    float spaceSize = 62.5f;
    float centeringValue = 0.5f;
    Canvas canvas;

    public BasePiece selectedPiece;
    
    BasePiece[] initialPieces = new BasePiece[21]
    {
        new Tower(false, 0, 0),
        new Tower(false, 7, 0),
        new Pawn(false, 0, 1),
        new Pawn(false, 1, 1),
        new Pawn(false, 2, 1),
        new Pawn(false, 3, 1),
        new Pawn(false, 4, 1),
        new Pawn(false, 5, 1),
        new Pawn(false, 6, 1),
        new Pawn(false, 7, 1),
        new Tower(true, 0, 7),
        new Tower(true, 7, 7),
        new Tower(true, 4, 4),
        new Pawn(true, 0, 6),
        new Pawn(true, 1, 6),
        new Pawn(true, 2, 6),
        new Pawn(true, 3, 6),
        new Pawn(true, 4, 6),
        new Pawn(true, 5, 6),
        new Pawn(true, 6, 6),
        new Pawn(true, 7, 6),
    };

    void Awake() {
        this.canvas = GameObject.FindObjectOfType<Canvas>();
        this.selectedPiece = new BasePiece(false, 0, 0);
        this.currentTurn = PlayerColor.White;
        this.winner = PlayerColor.None;

        this.createBoard();
        this.placeInitialPieces();
    }

    void createBoard() {
        for (int y = -4; y < 4; y++) {
            for (int x = -4; x < 4; x++) {
                int normalX = x+4;
                int normalY = y+4;

                GameObject instance = Instantiate(BoardSpacePrefab, new Vector3(x * spaceSize, y * spaceSize, 0), Quaternion.identity, this.canvas.transform);
                RectTransform transform = instance.GetComponent<RectTransform>();
                transform.anchoredPosition = new Vector3((x + centeringValue) * spaceSize, (y + centeringValue) * -spaceSize, 0);
        
                instance.name = "(" + normalX + ", " + normalY + ")";

                if ((x + y) % 2 == 0) {
                    instance.GetComponent<Image>().color = Color.black;
                }

                this.board[normalX, normalY] = instance;
                instance.GetComponent<BoardSpaceController>().setPosition(normalX, normalY);
                instance.transform.SetParent(this.canvas.transform, false);
            }
        }
    }

    void placeInitialPieces() {
        foreach (BasePiece piece in initialPieces) {
            this.board[piece.currentX, piece.currentY].GetComponent<BoardSpaceController>().currentPiece = piece;
        }
    }

    public void setSelectedPiece(BasePiece piece) {
        this.selectedPiece.onPieceSelected(this.board, false);
        this.selectedPiece = piece;
    }
}
