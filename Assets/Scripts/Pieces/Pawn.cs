using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Pawn
{
    public string type = "pawn";

    Sprite sprite;

    Pawn(bool isWhitePiece) {
        string spriteName = "WhitePawn";

        if (isWhitePiece) {
            spriteName = "BlackPawn";
        }

        this.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Pieces/" + spriteName + ".png");
    }

    virtual protected void ShowPossibleMovement() {
        //
    }
}
