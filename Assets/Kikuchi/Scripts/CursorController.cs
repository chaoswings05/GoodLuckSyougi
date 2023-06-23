using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private bool playerSelected = false;
    //TODO クリックした場所にカーソルを移動したい
    //カーソルを移動させたい
    //クリックした場所を取得したい

    private void Start()
    {
        float per1xy = 0.928f;//1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
        float basex = -3.708f - per1xy; //0に当たる場所。今回は左端の値
        float basey = -3.7146f - per1xy; //0に当たる場所。今回は下の値
        this.transform.position = new Vector3(basex + per1xy * 5, basey + per1xy * 5, 100);
    }
    public void SetPosition(Transform target)
    {
        transform.position = target.position;
    }

}
