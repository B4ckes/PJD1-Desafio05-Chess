using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    float spaceSize = 62.5f;
    float centeringValue = 0.5f;

    Canvas canvas;
    BoardSpaceController[,] board = new BoardSpaceController[8, 8];

    public GameObject BoardSpacePrefab;

    void Awake() {
        this.canvas = GameObject.FindObjectOfType<Canvas>();

        this.renderBoard();
    }

    void renderBoard() {
        for (int y = -4; y < 4; y++) {
            for (int x = -4; x < 4; x++) {
                int normalX = x+4;
                int normalY = y+4;

                BoardSpaceController space = board[normalX, normalY];

                GameObject instance = Instantiate(BoardSpacePrefab, new Vector3(x * spaceSize, y * spaceSize, 0), Quaternion.identity, this.canvas.transform);
                RectTransform transform = instance.GetComponent<RectTransform>();
                transform.anchoredPosition = new Vector3((x + centeringValue) * spaceSize, (y + centeringValue) * -spaceSize, 0);
        
                instance.name = "(" + normalX + ", " + normalY + ")";

                if ((x + y) % 2 == 0) {
                    instance.GetComponent<Image>().color = Color.black;
                }

                instance.transform.SetParent(this.canvas.transform, false);
            }
        }
    }
}
