using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BaseMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //Transform komaOuTrans = transform.Find(komaOu).gameObject.transform;
        //Transform komaHisyaTrans = transform.Find(komaHisya).gameObject.transform;
        //Transform komaKakuTrans = transform.Find(komaKaku).gameObject.transform;
        //Transform komaKinTrans = transform.Find(komaKin).gameObject.transform;
        //Transform komaGinTrans = transform.Find(komaGin).gameObject.transform;
        //Transform komaKeimaTrans = transform.Find(komaKeima).gameObject.transform;
        //Transform komaKyousyaTrans = transform.Find(komaKyousya).gameObject.transform;
        //Transform komaHoheiTrans = transform.Find(komaHohei).gameObject.transform;

        //komaOuTrans.position = new Vector3(basex + per1xy * 5, basey + per1xy * 1, 2);//左端の値に右にずれる領分の値に一コマの移動距離をかけて足す。かけている値が
        //komaHisyaTrans.position = new Vector3(basex + per1xy * 8, basey + per1xy * 2, 2);
        //komaKakuTrans.position = new Vector3(basex + per1xy * 2, basey + per1xy * 2, 2);
        //komaKinTrans.position = new Vector3(basex + per1xy * 4, basey + per1xy * 1, 2);
        //komaGinTrans.position = new Vector3(basex + per1xy * 3, basey + per1xy * 1, 2);
        //komaKeimaTrans.position = new Vector3(basex + per1xy * 2, basey + per1xy * 1, 2);
        //komaKyousyaTrans.position = new Vector3(basex + per1xy * 1, basey + per1xy * 1, 2);
        //komaHoheiTrans.position = new Vector3(basex + per1xy * 5, basey + per1xy * 3, 2);
        string komaOu = "koma_0";
        string komaHisya = "koma_1";
        string komaKaku = "koma_2";
        string komaKin = "koma_3";
        string komaGin = "koma_4";
        string komaKeima = "koma_5";
        string komaKyousya = "koma_6";
        string komaHohei = "koma_7";

        CreateKomaObj(komaOu, 5, 1);
        CreateKomaObj(komaKaku, 2, 2);
        CreateKomaObj(komaOu, 8, 2);


    }

    
    void CreateKomaObj(string name,int x,int y)
    {
        float per1xy = 0.928f;//1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
        float basex = -3.708f - per1xy; //0に当たる場所。今回は左端の値
        float basey = -3.7146f - per1xy; //0に当たる場所。今回は下の値

        SpriteRenderer[] sprites = Resources.LoadAll<SpriteRenderer>("Koma");
        SpriteRenderer sp = System.Array.Find<SpriteRenderer>(sprites, (spriterenderer) => spriterenderer.name.Equals(name));
        GameObject gameObj = new GameObject();
        SpriteRenderer sprite = gameObj.AddComponent<SpriteRenderer>();
        sprite = sprites[1];
        gameObj.transform.parent = GameObject.Find("MapManager").transform;
        //gameObj.transform.name = name;
        gameObj.transform.position = new Vector3(basex + per1xy * x, basey + per1xy * y, 2);


    }
}
