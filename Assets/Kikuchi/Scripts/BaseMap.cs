using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BaseMap : MonoBehaviour
{
    int mapWidth = 9;
    int mapHeight = 9;

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


    // Start is called before the first frame update
    void Start()
    {
        CreateBaseMap();

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

    }

    
    void CreateKomaObj(string name,int x,int y)//駒を初期配置に置く。
    {
        float per1xy = 0.928f;//1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
        float basex = -3.708f - per1xy; //0に当たる場所。今回は左端の値
        float basey = -3.7146f - per1xy; //0に当たる場所。今回は下の値

        GameObject obj = GameObject.Find(name);
        Vector3 objPos = new Vector3(basex + per1xy * x, basey + per1xy * y, 2);
        obj.transform.position = objPos;
    }

    void CreateBaseMap()//駒を初期配置に置く。
    {
        float per1xy = 0.928f;//1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
        float basex = -3.708f - per1xy; //0に当たる場所。今回は左端の値
        float basey = -3.7146f - per1xy; //0に当たる場所。今回は下の値

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                TileObj tileObj = null;
                GameObject map = (GameObject)Resources.Load("MapTrout");
                Vector3 objPos = new Vector3(basex + per1xy * x + 1, basey + per1xy * y + 1, 5);
                Vector2Int tilePosFirst = new Vector2Int((int)x + 1, (int)y + 1);
                map.name = "mapTrout_" + x + "_" + tilePosFirst;
                tileObj = map.GetComponent<TileObj>();
                tileObj.positionInt = tilePosFirst;
                Instantiate(map, objPos, Quaternion.identity);

            }

        }

    }

    public void MoveKomaObj(string name, int x, int y)//駒を初期配置に置く。
    {
        float per1xy = 0.928f;//1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
        float basex = -3.708f - per1xy; //0に当たる場所。今回は左端の値
        float basey = -3.7146f - per1xy; //0に当たる場所。今回は下の値

        GameObject obj = GameObject.Find(name);
        Debug.Log(obj.name);
        Vector3 objPos = new Vector3(basex + per1xy * x, basey + per1xy * y, 2);
        obj.transform.position = objPos;

    }
}
