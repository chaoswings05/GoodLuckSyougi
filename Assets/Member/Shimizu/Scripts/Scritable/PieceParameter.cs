using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PieceParameter
{
    public string name;  //名前
    public enum Rarity  //レアリティ
    {
        SSR,
        SR,
        R,
    }
    public Rarity rarity;
    public GameObject obj;  //プレハブ格納
}
