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
    public GameObject winnerUI;
    public Text winnerText;

    float spaceSize = 62.5f;
    float centeringValue = 0.5f;
    Canvas canvas;

    public BasePiece selectedPiece;
    
    BasePiece[] initialPieces = new BasePiece[32]
    {
        // black pieces
        new Tower(false, 0, 0),
        new Knight(false, 1, 0),
        new Bishop(false, 2, 0),
        new Queen(false, 3, 0),
        new King(false, 4, 0),
        new Bishop(false, 5, 0),
        new Knight(false, 6, 0),
        new Tower(false, 7, 0),
        new Pawn(false, 0, 1),
        new Pawn(false, 1, 1),
        new Pawn(false, 2, 1),
        new Pawn(false, 3, 1),
        new Pawn(false, 4, 1),
        new Pawn(false, 5, 1),
        new Pawn(false, 6, 1),
        new Pawn(false, 7, 1),
        // white pieces
        new Tower(true, 0, 7),
        new Knight(true, 1, 7),
        new Bishop(true, 2, 7),
        new Queen(true, 3, 7),
        new King(true, 4, 7),
        new Bishop(true, 5, 7),
        new Knight(true, 6, 7),
        new Tower(true, 7, 7),
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
        this.currentTurn = PlayerColor.Black;
        this.winner = PlayerColor.None;

        this.createBoard();
        this.placeInitialPieces();
    }

    void Update()
    {
        if (this.winnerUI != null && this.winner != PlayerColor.None) {
            this.winnerUI.SetActive(true);

            if (this.winner == PlayerColor.Black) {
                this.winnerText.text = this.winnerText.text.Replace("{0}", "pretas");
            } else if (this.winner == PlayerColor.White) {
                this.winnerText.text = this.winnerText.text.Replace("{0}", "brancas");
            }
        }
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
