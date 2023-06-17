using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string komaOu = "koma_0";
        string komaHisya = "koma_1";
        string komaKaku = "koma_2";

        float per1x = 0.928f;
        float basex = 11.793f - per1x; //左端の値 - (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
        Transform komaOuTrans = transform.Find(komaOu).gameObject.transform;

        komaOuTrans.position = new Vector3(basex + per1x * 6, 7.379f, 2);//左端の値に右にずれる領分の値に一コマの移動距離をかけて足す。
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
