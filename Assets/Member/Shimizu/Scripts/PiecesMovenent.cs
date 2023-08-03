using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PiecesMovenent : MonoBehaviour , IDragHandler , IBeginDragHandler , IEndDragHandler
{
    [System.NonSerialized]
    public Transform pieceParent;

    private void Awake()
    {
    }
    //ドラッグを始めるときの処理
    public void OnBeginDrag(PointerEventData eventData)
    {
        pieceParent = transform.parent;
        transform.SetParent(pieceParent.parent, true);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    //ドラッグをしているときの処理
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
        TargetPos.z = 0;
        transform.position = TargetPos;
    }
    //カードが離れる時の処理
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(pieceParent, true);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
