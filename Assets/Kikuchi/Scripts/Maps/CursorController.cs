using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    //TODO クリックした場所にカーソルを移動したい
    //カーソルを移動させたい
    //クリックした場所を取得したい
    float per1xy = 0.928f;//1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
    float basex = -3.708f; //0に当たる場所。今回は左端の値
    float basey = -3.7146f;//0に当たる場所。今回は下の値

    private void Start()
    {
        basex = -3.708f - per1xy;
        basey = -3.7146f - per1xy;

        this.transform.position = new Vector2(basex + per1xy * 5, basey + per1xy * 1);
    }
    public void SetPosition(Transform target)
    {
        transform.position = target.position;
    }
    

}
