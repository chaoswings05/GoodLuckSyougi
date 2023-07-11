using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koma : MonoBehaviour
{
    float per1xy = 0.928f;//1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
    float posx;
    float posy;
    float roundx;
    float roundy;
    int masuPosx;
    int masuPosy;
    float basex;
    float basey;

    private Vector3 posFix;

    public  Vector2Int positionInt;

    public Vector2Int Position { get => positionInt;}

    [HideInInspector]
    public string p1komaOu = "koma_0";
    [HideInInspector]
    public string p1komaHisya = "koma_1";
    [HideInInspector]
    public string p1komaKaku = "koma_2";
    [HideInInspector]
    public string p1komaKin = "koma_3_1";
    [HideInInspector]
    public string p1komaKin2 = "koma_3_2";
    [HideInInspector]
    public string p1komaGin = "koma_4_1";
    [HideInInspector]
    public string p1komaGin2 = "koma_4_2";
    [HideInInspector]
    public string p1komaKeima = "koma_5_1";
    [HideInInspector]
    public string p1komaKeima2 = "koma_5_2";
    [HideInInspector]
    public string p1komaKyousya = "koma_6_1";
    [HideInInspector]
    public string p1komaKyousya2 = "koma_6_2";
    [HideInInspector]
    public string p1komaHohyou1 = "koma_7_1";
    [HideInInspector]
    public string p1komaHohyou2 = "koma_7_2";
    [HideInInspector]
    public string p1komaHohyou3 = "koma_7_3";
    [HideInInspector]
    public string p1komaHohyou4 = "koma_7_4";
    [HideInInspector]
    public string p1komaHohyou5 = "koma_7_5";
    [HideInInspector]
    public string p1komaHohyou6 = "koma_7_6";
    [HideInInspector]
    public string p1komaHohyou7 = "koma_7_7";
    [HideInInspector]
    public string p1komaHohyou8 = "koma_7_8";
    [HideInInspector]
    public string p1komaHohyou9 = "koma_7_9";

    [HideInInspector]
    public string p2komaOu = "koma_8";
    [HideInInspector]
    public string p2komaHisya = "koma_9";
    [HideInInspector]
    public string p2komaKaku = "koma_10";
    [HideInInspector]
    public string p2komaKin = "koma_11_1";
    [HideInInspector]
    public string p2komaKin2 = "koma_11_2";
    [HideInInspector]
    public string p2komaGin = "koma_12_1";
    [HideInInspector]
    public string p2komaGin2 = "koma_12_2";
    [HideInInspector]
    public string p2komaKeima = "koma_13_1";
    [HideInInspector]
    public string p2komaKeima2 = "koma_13_2";
    [HideInInspector]
    public string p2komaKyousya = "koma_14_1";
    [HideInInspector]
    public string p2komaKyousya2 = "koma_14_2";
    [HideInInspector]
    public string p2komaHohyou1 = "koma_15_1";
    [HideInInspector]
    public string p2komaHohyou2 = "koma_15_2";
    [HideInInspector]
    public string p2komaHohyou3 = "koma_15_3";
    [HideInInspector]
    public string p2komaHohyou4 = "koma_15_4";
    [HideInInspector]
    public string p2komaHohyou5 = "koma_15_5";
    [HideInInspector]
    public string p2komaHohyou6 = "koma_15_6";
    [HideInInspector]
    public string p2komaHohyou7 = "koma_15_7";
    [HideInInspector]
    public string p2komaHohyou8 = "koma_15_8";
    [HideInInspector]
    public string p2komaHohyou9 = "koma_15_9";

    void Start()
    {
        posFix = new Vector3(0.06f, 0.05f, 0);
        CreateKomaObj(p1komaOu, 5, 1);

        CreateKomaObj(p1komaKaku, 2, 2);

        CreateKomaObj(p1komaHisya, 8, 2);

        CreateKomaObj(p1komaKin, 4, 1);
        CreateKomaObj(p1komaKin2, 6, 1);

        CreateKomaObj(p1komaGin, 7, 1);
        CreateKomaObj(p1komaGin2, 3, 1);

        CreateKomaObj(p1komaKeima, 2, 1);
        CreateKomaObj(p1komaKeima2, 8, 1);

        CreateKomaObj(p1komaKyousya, 1, 1);
        CreateKomaObj(p1komaKyousya2, 9, 1);

        CreateKomaObj(p1komaHohyou1, 1, 3);
        CreateKomaObj(p1komaHohyou2, 2, 3);
        CreateKomaObj(p1komaHohyou3, 3, 3);
        CreateKomaObj(p1komaHohyou4, 4, 3);
        CreateKomaObj(p1komaHohyou5, 5, 3);
        CreateKomaObj(p1komaHohyou6, 6, 3);
        CreateKomaObj(p1komaHohyou7, 7, 3);
        CreateKomaObj(p1komaHohyou8, 8, 3);
        CreateKomaObj(p1komaHohyou9, 9, 3);

        CreateKomaObj(p2komaOu, 5, 9);

        CreateKomaObj(p2komaKaku, 8, 8);

        CreateKomaObj(p2komaHisya, 2, 8);

        CreateKomaObj(p2komaKin, 4, 9);
        CreateKomaObj(p2komaKin2, 6, 9);

        CreateKomaObj(p2komaGin, 7, 9);
        CreateKomaObj(p2komaGin2, 3, 9);

        CreateKomaObj(p2komaKeima, 2, 9);
        CreateKomaObj(p2komaKeima2, 8, 9);

        CreateKomaObj(p2komaKyousya, 1, 9);
        CreateKomaObj(p2komaKyousya2, 9, 9);

        CreateKomaObj(p2komaHohyou1, 1, 7);
        CreateKomaObj(p2komaHohyou2, 2, 7);
        CreateKomaObj(p2komaHohyou3, 3, 7);
        CreateKomaObj(p2komaHohyou4, 4, 7);
        CreateKomaObj(p2komaHohyou5, 5, 7);
        CreateKomaObj(p2komaHohyou6, 6, 7);
        CreateKomaObj(p2komaHohyou7, 7, 7);
        CreateKomaObj(p2komaHohyou8, 8, 7);
        CreateKomaObj(p2komaHohyou9, 9, 7);
    }

    void CreateKomaObj(string name, int x, int y)//駒を初期配置に置く。
    {

        basex = -3.708f - per1xy; //0に当たる場所。今回は左端の値
        basey = -3.7146f - per1xy; //0に当たる場所。今回は下の値

        GameObject obj = GameObject.Find(name);
        Vector3 objPos = new Vector3(basex + per1xy * x, basey + per1xy * y, 2);
        posx = transform.position.x;
        posy = transform.position.y;
        roundx = (posx - basex) / per1xy;
        roundy = (posy - basey) / per1xy;
        Vector2Int pos = new Vector2Int((int)roundx, (int)roundy);//ポジション正規化
        positionInt = pos;
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
