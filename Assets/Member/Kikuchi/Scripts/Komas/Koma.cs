using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Koma : MonoBehaviour
{
    public int owner = 0;
    public string PieceName = "";
    float per1xy = 0.928f; //1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
    [HideInInspector] public float basex;
    [HideInInspector] public float basey;

    private Vector3 posFix;

    public Vector2Int positionInt;

    public Vector2Int Position { get => positionInt;}

    [SerializeField] private SpriteRenderer gachaKomaNameObj = null;

    void Start()
    {
        posFix = new Vector3(0.06f, 0.05f, 0);
    }

    public void Move(Vector2Int newPos)
    {
        transform.position = new Vector3(basex + per1xy * newPos.x, basey + per1xy * newPos.y, 2) + posFix;
        positionInt = newPos;
    }

    public void UpdateKomaData(int num, Sprite image)
    {
        gachaKomaNameObj.sprite = image;

        switch(num)
        {
            case 0:
            PieceName = "聖剣";
            this.name = "Seiken";
            break;

            case 1:
            PieceName = "忍者";
            break;

            case 2:
            PieceName = "屈強";
            break;

            case 3:
            PieceName = "卑怯";
            break;

            case 4:
            PieceName = "富豪";
            break;

            case 5:
            PieceName = "砲台";
            break;

            case 6:
            PieceName = "筋肉";
            break;

            case 7:
            PieceName = "俳優";
            break;

            default:
            break;
        }
    }
}

//選択タイルの取得
//キャラの選択
// 選択タイルの座標とキャラの座標を比較
//すべてのキャラクターを管理するクラスを作成する。
//キャラの移動
