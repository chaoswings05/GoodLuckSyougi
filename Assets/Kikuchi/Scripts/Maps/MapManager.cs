using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] CursorController cursor;
    [SerializeField] KomaManager komaManager;
    [SerializeField] BaseMap baseMap;

    Koma selectedKoma;
    [SerializeField]
    List<TileObj> tileObjs = new List<TileObj>();

    List<TileObj> movableTiles = new List<TileObj>();

    private void Start()
    {
        tileObjs = baseMap.CreateBaseMap();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//そのままやるとうまくいかないのでCamera.main.ScreenToWorldPointで座標形式を変換する。
            RaycastHit2D hit2D = Physics2D.Raycast(clickPosition, Vector2.down);
            if (hit2D && hit2D.collider)
            {
                cursor.SetPosition(hit2D.transform);
                TileObj tileobj = hit2D.collider.GetComponent<TileObj>();
                //選択タイルの座標
                Debug.Log(tileobj.positionInt);
                //ヒットしたタイル上の駒を取得する。
                Koma koma = komaManager.GetKoma(tileobj.positionInt);
                if (koma)
                {
                    Debug.Log("いる");
                    //駒の保持
                    selectedKoma = koma;
                    ResetMovablePanels();
                    //移動範囲を表示
                    ShowMovablePanels(selectedKoma);

                }
                else
                {
                    Debug.Log("クリックした場所に駒がいない");
                    //駒を保持しているなら、クリックしたタイルの場所に移動させる。
                    if (selectedKoma)
                    {
                        //selectedKomaをタイルまで移動させる。
                        selectedKoma.Move(tileobj.positionInt);
                        ResetMovablePanels();
                        selectedKoma = null;
                    }
                }
            }


        }
    }
    void ShowMovablePanels(Koma koma)
    {
        //centerposから上下左右のタイルを探す。
        //TileObj centerTile = ;


        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));

        foreach (var tile in movableTiles)
        {
            tile.ShowMovablePanel(true);
        }
    }

    void ResetMovablePanels()
    {
        foreach (var tile in movableTiles)
        {
            tile.ShowMovablePanel(false);
        }
        movableTiles.Clear();
    }

}
