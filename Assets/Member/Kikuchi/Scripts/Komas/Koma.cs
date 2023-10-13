using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Koma : MonoBehaviour
{
    public string PieceName = "";
    public bool CanReverse = false;
    private string ReverseName = "";
    private string ReverseText = "";

    float per1xy = 0.928f; //1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
    [HideInInspector] public float basex;
    [HideInInspector] public float basey;

    private Vector3 posFix;

    public Vector2Int positionInt;

    public Vector2Int Position { get => positionInt;}

    public TextMesh nameObj = null;

    [SerializeField] private GameObject effect = null;
    [SerializeField] private GameObject HaiyuEffect = null;

    void Start()
    {
        posFix = new Vector3(0.06f, 0.05f, 0);
        SetReverseData();
    }

    public void Move(Vector2Int newPos)
    {
        transform.position = new Vector3(basex + per1xy * newPos.x, basey + per1xy * newPos.y, 2) + posFix;
        Instantiate(effect, transform);
        positionInt = newPos;
    }

    private void SetReverseData()
    {
        switch(PieceName)
        {
            case "歩兵":
            CanReverse = true;
            ReverseName = "と金";
            ReverseText = "と\n金";
            break;

            case "香車":
            CanReverse = true;
            ReverseName = "成香";
            ReverseText = "成\n香";
            break;

            case "桂馬":
            CanReverse = true;
            ReverseName = "成桂";
            ReverseText = "成\n桂";
            break;

            case "銀将":
            CanReverse = true;
            ReverseName = "成銀";
            ReverseText = "成\n銀";
            break;

            case "角行":
            CanReverse = true;
            ReverseName = "龍馬";
            ReverseText = "龍\n馬";
            break;

            case "飛車":
            CanReverse = true;
            ReverseName = "龍王";
            ReverseText = "龍\n王";
            break;

            default:
            break;
        }
    }

    public void PieceReverse()
    {
        if (!CanReverse)
        {
            return;
        }

        if (this.name.Contains("koma_7")) //P1歩→と金
        {
            this.name = "RK07";
        }
        else if (this.name.Contains("koma_15")) //P2歩→と金
        {
            this.name = "RK15";
        }
        else if (this.name.Contains("koma_6")) //P1香車→成香
        {
            this.name = "RK06";
        }
        else if (this.name.Contains("koma_14")) //P2香車→成香
        {
            this.name = "RK14";
        }
        else if (this.name.Contains("koma_5")) //P1桂馬→成桂
        {
            this.name = "RK05";
        }
        else if (this.name.Contains("koma_13")) //P2桂馬→成桂
        {
            this.name = "RK13";
        }
        else if (this.name.Contains("koma_4")) //P1銀→成銀
        {
            this.name = "RK04";
        }
        else if (this.name.Contains("koma_12")) //P2銀→成銀
        {
            this.name = "RK12";
        }
        else if (this.name == ("koma_2")) //P1角→馬
        {
            this.name = "RK02";
        }
        else if (this.name == ("koma_10")) //P2角→馬
        {
            this.name = "RK10";
        }
        else if (this.name == "koma_1") //P1飛車→龍
        {
            this.name = "RK01";
        }
        else if (this.name == "koma_9") //P2飛車→龍
        {
            this.name = "RK09";
        }

        Instantiate(HaiyuEffect, transform);
        PieceName = ReverseName;
        nameObj.text = ReverseText;
        nameObj.color = Color.red;
        CanReverse = false;
    }

    public void UpdateKomaData(int num, Sprite image)
    {
        CanReverse = false;
        nameObj.color = Color.black;
        switch(num)
        {
            case 0:
            PieceName = "聖剣";
            this.name = "Seiken";
            nameObj.text = "聖\n剣";
            break;

            case 1:
            PieceName = "忍者";
            this.name = "Ninja";
            nameObj.text = "忍\n者";
            break;

            case 2:
            PieceName = "屈強";
            this.name = "Kukkyou";
            nameObj.text = "屈\n強";
            break;

            case 3:
            PieceName = "卑怯";
            this.name = "Hikyo";
            nameObj.text = "卑\n怯";
            break;

            case 4:
            PieceName = "富豪";
            this.name = "Fugo";
            nameObj.text = "富\n豪";
            break;

            case 5:
            PieceName = "砲台";
            this.name = "Houdai";
            nameObj.text = "砲\n台";
            break;

            case 6:
            PieceName = "筋肉";
            this.name = "Kinniku";
            nameObj.text = "筋\n肉";
            break;

            case 7:
            PieceName = "俳優";
            this.name = "Haiyu";
            nameObj.text = "俳\n優";
            break;

            default:
            break;
        }
    }

    public void HaiyuChange(string pieceName, string objName, TextMesh name)
    {
        Instantiate(HaiyuEffect, transform);
        nameObj.text = name.text;
        nameObj.color = name.color;
        PieceName = pieceName;
        this.name = objName;
    }
}

//選択タイルの取得
//キャラの選択
// 選択タイルの座標とキャラの座標を比較
//すべてのキャラクターを管理するクラスを作成する。
//キャラの移動
