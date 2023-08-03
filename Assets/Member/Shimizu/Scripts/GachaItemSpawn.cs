using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaItemSpawn : MonoBehaviour
{
    [SerializeField]
    PieceDataBase dataBase;  //スクリタブルのやつ
    [SerializeField]
    Gacha gacha;
    [SerializeField] private GachaSystem gachaSystem = null;

    private List<GameObject> gachaItem = new List<GameObject>();

    private int count = 0;

    public void GachaGacha()
    {
        //レアリティだけ渡してここでランダムとか
        PieceChoice(gacha.GachaMethod());
    }

    public void PieceChoice(string rarity)
    {
        gachaItem.Clear();
        foreach (var x in dataBase.pieceList)
        {
            if (x.rarity.ToString() == rarity)
            {
                gachaItem.Add(x.obj);
            }
        }
        Debug.Log(gachaItem.Count);
        gachaSystem.gachaItemUpdate(gachaItem[Random.Range(0, gachaItem.Count)].name);
    }
}

public class GachaItemParameter
{
    public GameObject obj = null;
    public string rarity = null;
}
