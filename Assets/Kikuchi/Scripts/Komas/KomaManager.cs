using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaManager : MonoBehaviour
{
    //キャラすべてを管理する。
    public List<Koma> komas = new List<Koma>();
    void Start()
    {
        // 一致するデータ型のの子要素をすべて取得する
        GetComponentsInChildren(komas);
    }


    public Koma GetKoma(Vector2Int pos)
    {
        foreach (var koma in komas)
        {
            if (koma.Position == pos)
            {
                Debug.Log(koma);
                return koma;
            }
        }
        return null;
    }

    public Koma GetP1Koma(Vector2Int pos)
    {
        foreach (var koma in komas)
        {
            if (koma != null && koma.Position == pos && koma.tag == "P1Koma")
            {
                Debug.Log(koma);
                return koma;
            }
        }
        return null;
    }

    public Koma GetP2Koma(Vector2Int pos)
    {
        foreach (var koma in komas)
        {
            if (koma != null && koma.Position == pos && koma.tag == "P2Koma")
            {
                return koma;
            }
        }
        return null;
    }

}
