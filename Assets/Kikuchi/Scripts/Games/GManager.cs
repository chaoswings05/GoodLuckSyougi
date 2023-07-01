using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    // フェーズの管理
    enum Phase
    {
        Player1KomaSelection, //駒選択
        Player1KomaMoveSelection,//駒移動
        Player2KomaSelection,
        Player2KomaMoveSelection,

    }

    //※※※※続きはpart14    7:44

    //選択したキャラの保持

    Koma selectedKoma;
    //選択キャラの移動可能範囲の保持
    public List<TileObj> movableTiles = new List<TileObj>();

    [SerializeField] private Text TurnText;

    [SerializeField]
    Phase phase;
    [SerializeField]
    KomaManager komaManager;
    [SerializeField]
    MapManager mapManager;

    private bool gameFinish;

    void Start()
    {
        phase = Phase.Player1KomaSelection;
    }

    //キャラ選択
    //キャラ移動
    //プレイヤーがクリックしたら処理
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameFinish == false)
        {
            PlayerClickAction();
        }
    }

    void PlayerClickAction()
    {
        switch (phase)
        {
            case Phase.Player1KomaSelection:
                Player1KomaSelection();

                break;
            case Phase.Player1KomaMoveSelection:

                Player1KomaMoveSelection();

                break;
            case Phase.Player2KomaSelection:
                Player2KomaSelection();

                break;
            case Phase.Player2KomaMoveSelection:

                Player2KomaMoveSelection();

                break;
        }
    }

    bool IsClickKoma1(TileObj clickTileObj)
    {
        //駒を取得して移動範囲を表示
        Koma koma = null;
        koma = komaManager.GetP1Koma(clickTileObj.positionInt);
        if (koma != null)
        {
            selectedKoma = koma;
            mapManager.ResetMovablePanels(movableTiles);
            //移動範囲を表示
            mapManager.ShowMovablePanels(selectedKoma, movableTiles);
            phase = Phase.Player1KomaMoveSelection;
            return true;
        }
        return false;
    }

    bool IsClickKoma2(TileObj clickTileObj)
    {
        //駒を取得して移動範囲を表示
        Koma koma = null;
        koma = komaManager.GetP2Koma(clickTileObj.positionInt);
        if (koma != null)
        {
            selectedKoma = koma;
            mapManager.ResetMovablePanels(movableTiles);
            //移動範囲を表示
            mapManager.ShowMovablePanels(selectedKoma, movableTiles);
            phase = Phase.Player2KomaMoveSelection;
            return true; 
        }
        return false;
    }

    bool OnTileP1Check(TileObj clickTileObj)
    {
        //駒を取得して移動範囲を表示
        Koma koma = null;
        koma = komaManager.GetP1Koma(clickTileObj.positionInt);
        if (koma != null)
        {
            return true;
        }
        return false;
    }

    bool OnTileP2Check(TileObj clickTileObj)
    {
        //駒を取得して移動範囲を表示
        Koma koma = null;
        koma = komaManager.GetP2Koma(clickTileObj.positionInt);
        if (koma != null )
        {
            return true;
        }
        return false;
    }


    void Player1KomaSelection()
    {
        //クリックしたタイルを取得
        //その上に駒がいるなら
        TileObj clickTileObj = mapManager.GetClickTileObj();
        if(clickTileObj != null)
        {
            if (IsClickKoma1(clickTileObj))
            {
                phase = Phase.Player1KomaMoveSelection;
            }
        }
        
    }

    void Player2KomaSelection()
    {
        //クリックしたタイルを取得
        //その上に駒がいるなら
        TileObj clickTileObj = mapManager.GetClickTileObj();
        if (clickTileObj != null)
        {
            if (IsClickKoma2(clickTileObj))
            {
                phase = Phase.Player2KomaMoveSelection;
            }
            
        }
    }

    void Player1KomaMoveSelection()
    {

        //クリックした場所が移動範囲なら移動させて敵のフェーズへ
        TileObj clickTileObj = mapManager.GetClickTileObj();
        
        if (clickTileObj) //クリックした場所にタイルが存在するなら
        {
            if (IsClickKoma1(clickTileObj))//1pの駒がいるなら
            {
                return;
            }
            else if (OnTileP2Check(clickTileObj))
            {
                if (movableTiles.Contains(clickTileObj)) //クリックした場所に相手の駒がいて移動範囲内なら
                {
                    Koma enemyKoma = komaManager.GetP2Koma(clickTileObj.positionInt);//その座標の駒を取得
                    if (enemyKoma.name == "koma_8")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                        Player1Win();
                    }
                    else//取得した駒が王以外なら移動してフェーズを変える。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                    }
                    mapManager.PosCursor(2);
                    TurnText.GetComponent<Text>().text = "後手の手番" ;
                    phase = Phase.Player2KomaSelection;
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }
            else
            {
                //クリックしたタイルが移動範囲に含まれるなら
                if (movableTiles.Contains(clickTileObj))
                {
                    //selectedKomaをタイルまで移動させる。
                    selectedKoma.Move(clickTileObj.positionInt);
                    mapManager.PosCursor(2);
                    TurnText.GetComponent<Text>().text = "後手の手番";
                    phase = Phase.Player2KomaSelection;
                    
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;

            }
            
        }

    }

    void Player2KomaMoveSelection()
    {

        //クリックした場所が移動範囲なら移動させて敵のフェーズへ
        TileObj clickTileObj = mapManager.GetClickTileObj();

        if(clickTileObj != null)
        {
            if (IsClickKoma2(clickTileObj))//p2の駒がいるなら
            {
                return;
            }
            else if (OnTileP1Check(clickTileObj))
            {
                if(movableTiles.Contains(clickTileObj)) //クリックした場所に相手の駒がいて移動範囲内なら
                {
                    Koma enemyKoma = komaManager.GetP1Koma(clickTileObj.positionInt);
                    if (enemyKoma.name == "koma_0")//取得した敵駒が王なら
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                        Player2Win();
                    }
                    else
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                    }
                    mapManager.PosCursor(1);
                    TurnText.GetComponent<Text>().text = "先手の手番";
                    phase = Phase.Player1KomaSelection;
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }
        else
        {
            //クリックしたタイルが移動範囲に含まれるなら
            if (movableTiles.Contains(clickTileObj))
            {
                //selectedKomaをタイルまで移動させる。
                selectedKoma.Move(clickTileObj.positionInt);
                mapManager.PosCursor(1);
                    TurnText.GetComponent<Text>().text = "先手の手番";
                    phase = Phase.Player1KomaSelection;
            }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;

            }
        }

    }

    private void Player1Win()//ここにプレイヤー１が勝った時の処理をお願いします。
    {
        gameFinish = true;
        Debug.Log("p1Win");
    }

    private void Player2Win()
    {
        gameFinish = true;
        Debug.Log("p2Win");
    }

}
