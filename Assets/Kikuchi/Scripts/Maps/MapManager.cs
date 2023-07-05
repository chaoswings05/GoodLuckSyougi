using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public CursorController cursor;
    [SerializeField] BaseMap baseMap;
    [SerializeField] KomaManager komaManager;

    [SerializeField] GManager gameManager;

    // 生成したマップを管理する。
    List<TileObj> tileObjs = new List<TileObj>();
    

    Koma onTileKoma = null;

    float per1xy = 0.928f;//1マスあたりの移動値 (駒が動く座標範囲の全体の大きさ/一コマの移動距離)
    float basex = -3.708f; //0に当たる場所。今回は左端の値
    float basey = -3.7146f;//0に当たる場所。今回は下の値
    int mapWidth = 9;
    int mapHeight = 9;


    private void Start()
    {
        basex = -3.708f - per1xy;
        basey = -3.7146f - per1xy;
        tileObjs = baseMap.CreateBaseMap();
    }

    public void PosCursor(int player)
    {
        if(player == 1)
        cursor.gameObject.transform.position = new Vector2(0.076f, -3.64f);
        else if(player == 2)
        cursor.gameObject.transform.position = new Vector2(0.076f, 3.78f);
    }

    public TileObj GetClickTileObj() //クリックしたタイルを取得する関数
    {

        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//そのままやるとうまくいかないのでCamera.main.ScreenToWorldPointで座標形式を変換する。
        RaycastHit2D hit2D = Physics2D.Raycast(clickPosition, Vector2.down);
        if (hit2D && hit2D.collider)
        {
            cursor.SetPosition(hit2D.transform);
            TileObj check = hit2D.collider.GetComponent<TileObj>();
            Debug.Log(check.positionInt);
            return hit2D.collider.GetComponent<TileObj>();
        }

        return null;
    }

    public void ShowSetPanels(Koma koma , List<TileObj> setTiles)
    {
        Debug.Log(koma.name);
        Vector2Int onTilePos = new Vector2Int(1, 1);
        for (int x = 1; x <= mapWidth; x++)
        {
            for (int y = 1; y <= mapHeight; y++)
            {
                onTilePos.x = x;
                onTilePos.y = y;
                onTileKoma = komaManager.GetKoma(onTilePos);
                if (onTileKoma == null)
                {
                    setTiles.Add(tileObjs.Find(tile => tile.positionInt == onTilePos));
                    
                }

            }
            onTilePos = new Vector2Int(1, 1);
        }

        for (int x = 1; x <= mapWidth; x++)
        {
            for (int y = 1; y <= mapHeight; y++)
            {
                onTilePos.x = x;
                onTilePos.y = y;
                onTileKoma = komaManager.GetKoma(onTilePos);
                if (onTileKoma != null)
                {
                    if (koma.name.Contains("koma_7") && onTileKoma.name.Contains("koma_7"))
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            setTiles.Remove(tileObjs.Find(tile => tile.positionInt == new Vector2Int(onTilePos.x, onTilePos.y + i)));
                            setTiles.Remove(tileObjs.Find(tile => tile.positionInt == new Vector2Int(onTilePos.x, onTilePos.y - i)));

                        }
                    }
                    else if (koma.name.Contains("koma_15") && onTileKoma.name.Contains("koma_15"))
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            setTiles.Remove(tileObjs.Find(tile => tile.positionInt == new Vector2Int(onTilePos.x, onTilePos.y + i)));
                            setTiles.Remove(tileObjs.Find(tile => tile.positionInt == new Vector2Int(onTilePos.x, onTilePos.y - i)));

                        }
                    }
                }

            }
        }

        foreach (var tile in setTiles)
        {
            if (tile != null)
            {
                tile.ShowMovablePanel(true);
            }

        }
    }

    public void ResetSetPanels(List<TileObj> setTiles) //移動範囲をリセットする
    {
        foreach (var tile in setTiles)
        {
            if (tile != null)
            {
                tile.ShowMovablePanel(false);
            }

        }
        setTiles.Clear();
    }
    public void ShowMovablePanels(Koma koma , List<TileObj> movableTiles) //移動範囲を表示する
    {
        //centerposから上下左右のタイルを探す。

        if(koma.name.Contains("koma_7")) //p1の歩兵の動き
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
        }

        if (koma.name.Contains("koma_15")) //p2の歩兵の動き
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
        }

        if (koma.name == ("koma_0")|| koma.name == ("koma_8")) //王の動き
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }



        }

        


        if (koma.name == "koma_1" || koma.name == "koma_9") //飛車の動き
        {

            for (int i = 1; i < 9; i++) //上の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(0, i));
                if (onTileKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                    break;
                }
                else if (onTileKoma.tag == koma.tag)
                {
                    break;
                }
            }

            for (int i = 1; i < 9; i++) //下の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(0, -i));
                if (onTileKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, -i)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, -i)));
                    break;
                }
                else if (onTileKoma.tag == koma.tag)
                {
                    break;
                }
            }

            for (int i = 1; i < 9; i++) //上の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(i, 0));
                if (onTileKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, 0)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, 0)));
                    break;
                }
                else if (onTileKoma.tag == koma.tag)
                {
                    break;
                }
            }

            for (int i = 1; i < 9; i++) //上の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(-i, 0));
                if (onTileKoma == null )
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, 0)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, 0)));
                    break;
                }
                else if (onTileKoma.tag == koma.tag)
                {
                    break;
                }
            }

        }

        if (koma.name == ("koma_2") || koma.name == ("koma_10"))
        {

            for (int i = 1; i < 9; i++) //上の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(i, i));
                if (onTileKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, i)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, i)));
                    break;
                }
                else if (onTileKoma.tag == koma.tag) break;
                else
                {
                    Debug.Log("error");
                }
            }

            for (int i = 1; i < 9; i++) //下の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(-i, i));
                if (onTileKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, i)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, i)));
                    break;
                }
                else if (onTileKoma.tag == koma.tag) break;
                else
                {
                    Debug.Log("error");
                }
            }

            for (int i = 1; i < 9; i++) //上の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(i, -i));
                if (onTileKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, -i)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, -i)));
                    break;
                }
                else if (onTileKoma.tag == koma.tag) break;

                else
                {
                    Debug.Log("error");
                }
            }

            for (int i = 1; i < 9; i++) //上の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(-i, -i));
                if (onTileKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, -i)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, -i)));
                    break;
                }
                else if (onTileKoma.tag == koma.tag) break;
                else
                {
                    Debug.Log("error");
                }
            }

        }

        if (koma.name.Contains("koma_3")) //p1の金の動き
        {

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }

        }

        if (koma.name.Contains("koma_11")) //p2の金の動き
        {

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }

        }

        if (koma.name.Contains("koma_4")) //p1の銀の動き
        {

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            //左下
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            //右下
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }

            //左上
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }

            //右上
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }

        }

        if (koma.name.Contains("koma_12")) //p2の銀の動き
        {
            //下
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            //左下
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            //右下
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }

            //左上
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            //右上
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }

        }

        if (koma.name.Contains("koma_5")) //p1の桂馬の動き
        {
            onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(1, 2));
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, 2)));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, 2)));
            }


            onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(-1, 2));
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, 2)));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, 2)));
            }


        }

        if (koma.name.Contains("koma_13")) //p2の桂馬の動き
        {

            onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(1, -2));
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, -2)));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, -2)));
            }


            onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(-1, -2));
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, -2)));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, -2)));
            }

        }

        if (koma.name.Contains("koma_6")) //p1の香車の動き
        {

            for (int i = 1; i < 9; i++) //上の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(0, i));
                if (onTileKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                    break;
                }
                else break;
            }

        }

        if (koma.name.Contains("koma_14")) //p2の香車の動き
        {

            for (int i = 1; i < 9; i++) //上の値最大まで取る。null対処はmuvableTile関数で行う。
            {
                Koma onTileKoma = null;
                Koma onTileEnemyKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(0, -i));
                if (onTileKoma == null && onTileEnemyKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, -i)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, -i)));
                    break;
                }
                else
                {
                    break;
                }
            }

        }

        foreach (var tile in movableTiles)
        {
            if(tile != null)
            {
                tile.ShowMovablePanel(true);
            }
            
        }
    }

    public void ResetMovablePanels(List<TileObj> movableTiles) //移動範囲をリセットする
    {
        foreach (var tile in movableTiles)
        {
            if (tile != null)
            {
                tile.ShowMovablePanel(false);
            }
            
        }
        movableTiles.Clear();
    }

}
