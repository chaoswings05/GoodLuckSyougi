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

}
