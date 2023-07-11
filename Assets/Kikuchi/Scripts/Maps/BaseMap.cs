using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BaseMap : MonoBehaviour
{
    int mapWidth = 9;
    int mapHeight = 9;
    


    // Start is called before the first frame update
    void Start()
    {
    }

    public List<TileObj> CreateBaseMap()//駒を初期配置に置く。
    {
        List<TileObj>  tileObjs = new List<TileObj>();
        float per1xy = 0.928f;//1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
        float basex = -3.708f - per1xy; //0に当たる場所。今回は左端の値
        float basey = -3.7146f - per1xy; //0に当たる場所。今回は下の値

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {

                TileObj tileObj ;
                GameObject map = (GameObject)Resources.Load("MapTrout");
                Vector3 objPos = new Vector3(basex + per1xy * x + 1, basey + per1xy * y + 1, 5);
                Vector2Int tilePosFirst = new Vector2Int((int)x + 1, (int)y + 1);
                map.name = "mapTrout_" + tilePosFirst;
                
                map = Instantiate(map, objPos, Quaternion.identity);
                tileObj = map.GetComponent<TileObj>();
                tileObj.positionInt = tilePosFirst;
                tileObjs.Add(tileObj);
                Debug.Log(tileObj);

            }
        }
        return tileObjs;
    }
}
