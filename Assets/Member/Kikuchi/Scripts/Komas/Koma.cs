using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koma : MonoBehaviour
{
    float per1xy = 0.928f; //1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
    float posx;
    float posy;
    float roundx;
    float roundy;
    int masuPosx;
    int masuPosy;
    [HideInInspector] public float basex;
    [HideInInspector] public float basey;

    private Vector3 posFix;

    public Vector2Int positionInt;

    public Vector2Int Position { get => positionInt;}

    void Start()
    {
        posFix = new Vector3(0.06f, 0.05f, 0);
    }

    public void Move(Vector2Int newPos)
    {
        transform.position = new Vector3(basex + per1xy * newPos.x, basey + per1xy * newPos.y, 2) + posFix;
        positionInt = newPos;
    }
}

//選択タイルの取得
//キャラの選択
// 選択タイルの座標とキャラの座標を比較
//すべてのキャラクターを管理するクラスを作成する。
//キャラの移動
