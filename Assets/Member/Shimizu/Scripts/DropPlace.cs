using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlace : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //�h���b�O��񂩂�擾
        PiecesMovenent pieces = eventData.pointerDrag.GetComponent<PiecesMovenent>();
        if(pieces != null)
        {
            //�e�������ɂ���
            pieces.pieceParent = this.transform;
        }
    }
}
