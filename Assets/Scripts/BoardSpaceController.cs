using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BoardSpaceController : MonoBehaviour, IPointerDownHandler
{
    public Image selectedBorder;
    public Image canMoveBorder;

    public void OnPointerDown(PointerEventData eventData) {
        this.selectedBorder.gameObject.SetActive(true);
    }
}
