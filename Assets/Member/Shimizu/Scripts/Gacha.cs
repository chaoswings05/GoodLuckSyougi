using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    private float[] item2;

    private int[] item;
    //
    [Header("各レアリティの重み")]
    [SerializeField]
    private int ssrWeight = 5;
    [SerializeField]
    private int srWeight = 20;
    [SerializeField]
    private int rWeight = 100;
    [Header("駒の種類と各レアリティの個数")]
    [SerializeField]
    private int pieceQuantity = 7;
    [SerializeField]
    private int ssrQuantity = 1;
    [SerializeField]
    private int srQuantity = 2;
    [SerializeField]
    private int rQuantity = 4;
    private void Start()
    {
        //個数チェック
        while(pieceQuantity != (ssrQuantity + srQuantity + rQuantity))
        {
            //総個数の方が多かったらRを増やす
            if (pieceQuantity > (ssrQuantity + srQuantity + rQuantity))
            {
                rQuantity++;
                Debug.Log("総個数と各レアリティ合計個数が違うからRの個数を増やしたよ");
            }
            //合算の方が多かったらRを減らし、結果0以下になったらエラーを吐く
            else
            {
                rQuantity--;
                Debug.Log("総個数と各レアリティ合計個数が違うからRの個数を減らしたよ");
                if (rQuantity < 0)
                {
#if UNITY_EDITOR
                    Debug.LogError("ガチャの個数設定おかしいよ!!");
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
        }
        //一個当たりの重みを切り上げで計算
        var ssrPerOne = Mathf.CeilToInt(ssrWeight / ssrQuantity);
        var srPerOne = Mathf.CeilToInt(srWeight / srQuantity);
        var rPerOne = Mathf.CeilToInt(rWeight / rQuantity);
        //配列の初期化
        item = new int[pieceQuantity];
        //配列の値(重み)の設定
        for(int i = 0; i < pieceQuantity; i++)
        {
            if (i < ssrQuantity)
            {
                item[i] = ssrPerOne;
            }
            else if( i< srQuantity + ssrQuantity)
            {
                item[i] = srPerOne;
            }
            else
            {
                item[i] = rPerOne;
            }
        }
        var result = Choose(item);
        //Debug.Log(result);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GachaMethod();
        }
    }
    //ガチャを引くメソッド
    public string GachaMethod()
    {
        //個数チェック
        while (pieceQuantity != (ssrQuantity + srQuantity + rQuantity))
        {
            //総個数の方が多かったらRを増やす
            if (pieceQuantity > (ssrQuantity + srQuantity + rQuantity))
            {
                rQuantity++;
                Debug.Log("総個数と各レアリティ合計個数が違うからRの個数を増やしたよ");
            }
            //合算の方が多かったらRを減らし、結果0以下になったらエラーを吐く
            else
            {
                rQuantity--;
                Debug.Log("総個数と各レアリティ合計個数が違うからRの個数を減らしたよ");
                if (rQuantity < 0)
                {
#if UNITY_EDITOR
                    Debug.LogError("ガチャの個数設定おかしいよ!!");
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
        }
        //一個当たりの重みを切り上げで計算
        var ssrPerOne = Mathf.CeilToInt(ssrWeight / ssrQuantity);
        var srPerOne = Mathf.CeilToInt(srWeight / srQuantity);
        var rPerOne = Mathf.CeilToInt(rWeight / rQuantity);
        //配列の初期化
        item = new int[pieceQuantity];
        //配列の値(重み)の設定
        for (int i = 0; i < pieceQuantity; i++)
        {
            if (i < ssrQuantity)
            {
                item[i] = ssrPerOne;
            }
            else if (i < srQuantity + ssrQuantity)
            {
                item[i] = srPerOne;
            }
            else
            {
                item[i] = rPerOne;
            }
        }
        int result = Choose(item);
        string resultRarity = null;
        //結果からレアリティ判定
        if (result < ssrQuantity)
        {
            resultRarity = "SSR";
        }
        else if (result < ssrQuantity + srQuantity)
        {
            resultRarity = "SR";
        }
        else
        {
            resultRarity = "R";
        }

        return resultRarity;
    }

    //抽選メソッド
    int Choose(int[] probs)
    {

        float total = 0;

        //配列の要素を代入して重みの計算
        foreach (float elem in probs)
        {
            total += elem;
        }

        //重みの総数に0から1.0の乱数をかけて抽選
        float randomPoint = Random.value * total;

        //iが配列の最大要素数になるまで繰り返す
        for (int i = 0; i < probs.Length; i++)
        {
            //ランダムポイントが重みより小さいなら
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                //ランダムポイントが重みより大きいならその値を引いて次の要素へ
                randomPoint -= probs[i];
            }
        }

        //乱数が１の時、配列数の-１＝要素の最後の値をChoose配列に戻している
        return probs.Length - 1;
    }
    public int PieceQuantity()
    {
        return pieceQuantity;
    }
}
