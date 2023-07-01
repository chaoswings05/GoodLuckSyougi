using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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

    

    public void DeleteKoma(string deleteKoma)
    {
        foreach (var koma in komas)
        {
            if (koma.name == deleteKoma)
            {
                komas.Remove(koma);
                Destroy(koma);
                if(koma.tag == "P1Koma")
                {
                    IncreaceP2Koma(koma);
                }
                else if (koma.tag == "P2Koma")
                {
                    IncreaceP1Koma(koma);
                }
                else
                {
                    Debug.Log("駒ではないものがDeleteKomaに送られています");
                }

                break;
            }
        }
    }

    public void IncreaceP1Koma(Koma p2Koma)
    {
        p2Koma.tag = "P1Koma";
        p2Koma.gameObject.transform.position = new Vector3(-20, 0, 0);
        p2Koma.AddComponent<Koma>();
        if(p2Koma.name == "koma_9")
        {
            p2Koma.name = "koma_1";
        }
        if (p2Koma.name == "koma_10")
        {
            p2Koma.name = "koma_2";
        }
        if (p2Koma.name.Contains("koma_11"))
        {
            p2Koma.name = "koma_3";
        }
        if (p2Koma.name.Contains("koma_12"))
        {
            p2Koma.name = "koma_4";
        }
        if (p2Koma.name.Contains("koma_13"))
        {
            p2Koma.name = "koma_5";
        }
        if (p2Koma.name.Contains("koma_14"))
        {
            p2Koma.name = "koma_6";
        }
        if (p2Koma.name.Contains("koma_15"))
        {
            p2Koma.gameObject.name = "koma_7";
            
        }
    }
    //リストに追加して戻す時
    //komas.Add(koma);
    //p2Koma.Move(new Vector2Int(5,5));

    public void IncreaceP2Koma(Koma p1Koma)
    {
        p1Koma.tag = "P2Koma";
        p1Koma.gameObject.transform.position = new Vector3(20, 0, 0);
        p1Koma.AddComponent<Koma>();


    }

}
