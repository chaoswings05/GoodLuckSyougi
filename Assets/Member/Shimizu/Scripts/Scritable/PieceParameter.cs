using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PieceParameter
{
    public string name;  //���O
    public enum Rarity  //���A���e�B
    {
        SSR,
        SR,
        R,
    }
    public Rarity rarity;
    public GameObject obj;  //�v���n�u�i�[
}
