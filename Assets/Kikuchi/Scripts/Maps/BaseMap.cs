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

    public List<TileObj> CreateBaseMap()//��������z�u�ɒu���B
    {
        List<TileObj>  tileObjs = new List<TileObj>();
        float per1xy = 0.928f;//1�}�X������̈ړ��l (��������W�͈͂̑S�̂̑傫��/��R�}�̈ړ�����)
        float basex = -3.708f - per1xy; //0�ɓ�����ꏊ�B����͍��[�̒l
        float basey = -3.7146f - per1xy; //0�ɓ�����ꏊ�B����͉��̒l

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
