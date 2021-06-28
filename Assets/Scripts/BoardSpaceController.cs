using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BoardSpaceController : MonoBehaviour, IPointerDownHandler
{
    public Image selectedBorder;
    public Image canMoveBorder;
    public Image currentPieceSprite;
    public BasePiece currentPiece;

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("Teste");
        this.selectedBorder.gameObject.SetActive(true);
    }

    public void activeHighlight() {
        if (this.canMoveBorder != null) {
            this.canMoveBorder.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (this.currentPiece != null) {
            this.currentPieceSprite.gameObject.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(this.currentPiece.pieceSpritePath);
            this.currentPieceSprite.gameObject.SetActive(true);
        } else {
            this.currentPieceSprite.gameObject.SetActive(false);
        }
    }
}
