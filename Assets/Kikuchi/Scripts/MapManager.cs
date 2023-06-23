using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] CursorController cursor;

    [HideInInspector]
    public bool playerSelected = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerSelected == false)
        {

            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//そのままやるとうまくいかないのでCamera.main.ScreenToWorldPointで座標形式を変換する。
            RaycastHit2D hit2D = Physics2D.Raycast(clickPosition, Vector2.down);
            if (hit2D && hit2D.collider)
            {
                cursor.SetPosition(hit2D.transform);
                TileObj tileobj = hit2D.collider.GetComponent<TileObj>();
                Debug.Log(tileobj.positionInt);

            }


        }
    }
}
