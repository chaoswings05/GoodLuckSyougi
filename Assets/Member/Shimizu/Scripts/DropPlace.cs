using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlace : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //ドラッグ情報から取得
        PiecesMovenent pieces = eventData.pointerDrag.GetComponent<PiecesMovenent>();
        if(pieces != null)
        {
            //親を自分にする
            pieces.pieceParent = this.transform;
        }
    }
}
