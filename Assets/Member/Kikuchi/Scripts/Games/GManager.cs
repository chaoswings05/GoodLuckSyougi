using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    // フェーズの管理
    enum Phase
    {
        Player1KomaSelection, //P1駒選択
        Player1KomaMoveSelection, //P1駒移動
        Player2KomaSelection,
        Player2KomaMoveSelection,
        Player1MotiKomaMoveSelection,
        Player2MotiKomaMoveSelection,
        Player1Gacha,
        Player2Gacha,
        Player1Narration,
        Player2Narration,
        GameEnd,
    }

    //選択したキャラの保持
    Koma selectedKoma;

    //選択キャラの移動可能範囲の保持
    public List<TileObj> movableTiles = new List<TileObj>();
    public List<TileObj> setTiles = new List<TileObj>();

    [SerializeField] private GameObject P1TurnUI;
    [SerializeField] private GameObject P2TurnUI;

    [SerializeField] private Phase phase;
    [SerializeField] private KomaManager komaManager;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private NarrationBuild narration = null;
    [SerializeField] private GachaSystem gachaSystem = null;
    [SerializeField] private ResultManager resultManager = null;

    private bool IsGameFinished = false;

    void Start()
    {
        SoundManager.Instance.PlayBGM(1);
        phase = Phase.Player1KomaSelection;
    }

    //キャラ選択
    //キャラ移動
    //プレイヤーがクリックしたら処理

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && phase != Phase.GameEnd)
        {
            PlayerClickAction();
        }

        switch(phase)
        {
            case Phase.Player1Narration:
            if (!SoundManager.Instance.IsNarrationPlaying)
            {
                if (IsGameFinished)
                {
                    Player1Win();
                }
                else if (komaManager.defeatedKomas.Count > 0)
                {
                    phase = Phase.Player1Gacha;
                    komaManager.gameObject.SetActive(false);
                    gachaSystem.GachaStart();
                }
                else
                {
                    TurnPlayerUIMove(2);
                    phase = Phase.Player2KomaSelection;
                }
            }
            break;

            case Phase.Player2Narration:
            if (!SoundManager.Instance.IsNarrationPlaying)
            {
                if (IsGameFinished)
                {
                    Player2Win();
                }
                else if (komaManager.defeatedKomas.Count > 0)
                {
                    phase = Phase.Player2Gacha;
                    komaManager.gameObject.SetActive(false);
                    gachaSystem.GachaStart();
                }
                else
                {
                    TurnPlayerUIMove(1);
                    phase = Phase.Player1KomaSelection;
                }
            }
            break;

            default:
            break;
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

            case Phase.Player1MotiKomaMoveSelection:
                P1MotiKomaMoveSelection();
                break;

            case Phase.Player2MotiKomaMoveSelection:
                P2MotiKomaMoveSelection();
                break;
        }
    }

    bool IsClickKoma1(TileObj clickTileObj)
    {
        //駒を取得して移動範囲を表示
        Koma koma = null;
        koma = komaManager.GetP1Koma(clickTileObj.positionInt);
        if (koma != null)//駒があるなら
        {
            selectedKoma = koma;
            mapManager.ResetSetPanels(setTiles);
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
            mapManager.ResetSetPanels(setTiles);
            mapManager.ResetMovablePanels(movableTiles);
            //移動範囲を表示
            mapManager.ShowMovablePanels(selectedKoma, movableTiles);
            phase = Phase.Player2KomaMoveSelection;
            return true; 
        }
        return false;
    }

    bool IsClickP1MotiKoma(TileObj clickTileObj)
    {
        //駒を取得して移動範囲を表示
        Koma koma = null;
        koma = komaManager.GetP1MotiKoma(clickTileObj.positionInt);//リストを作って手札駒をそこに入れる。
        if (koma != null)//駒があるなら
        {
            selectedKoma = koma;
            mapManager.ResetMovablePanels(movableTiles);
            mapManager.ResetSetPanels(setTiles);
            mapManager.ShowSetPanels(selectedKoma, setTiles);
            phase = Phase.Player1MotiKomaMoveSelection;
            return true;
        }
        return false;
    }

    bool IsClickP2MotiKoma(TileObj clickTileObj)
    {
        //駒を取得して移動範囲を表示
        Koma koma = null;
        koma = komaManager.GetP2MotiKoma(clickTileObj.positionInt);//リストを作って手札駒をそこに入れる。
        if (koma != null)//駒があるなら
        {
            selectedKoma = koma;
            mapManager.ResetMovablePanels(movableTiles);
            mapManager.ResetSetPanels(setTiles);
            mapManager.ShowSetPanels(selectedKoma, setTiles);
            phase = Phase.Player2MotiKomaMoveSelection;
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
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //その上に駒がいるなら
        if(clickTileObj != null)
        {
            if (IsClickP1MotiKoma(clickTileObj)) //持ち駒取得メソッドを作ってクリックしたタイルが手札タイルか調べる。
            {
                phase = Phase.Player1MotiKomaMoveSelection;
            }
            else if (IsClickKoma1(clickTileObj))
            {
                phase = Phase.Player1KomaMoveSelection;
            }
        }
    }

    void Player2KomaSelection()
    {
        //クリックしたタイルを取得
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //その上に駒がいるなら
        if (clickTileObj != null)
        {
            if (IsClickP2MotiKoma(clickTileObj)) //持ち駒取得メソッドを作ってクリックしたタイルが手札タイルか調べる。
            {
                phase = Phase.Player2MotiKomaMoveSelection;
            }
            else if (IsClickKoma2(clickTileObj))
            {
                phase = Phase.Player2KomaMoveSelection;
            }
        }
    }

    void P1MotiKomaMoveSelection()
    {
        //持ち駒を選択した状態でもう一度クリックしたときこの関数に来る。
        // TODO
        //クリックしたタイルを取得
        TileObj clickTileObj = mapManager.GetClickTileObj();
        if (clickTileObj)
        {
            if (clickTileObj.tag != "TehudaTile" && setTiles.Contains(clickTileObj)) //クリックしたタイルが手札タイルじゃないならクリックしたタイルと同じポジションの駒がいるかを駒のリストを使って参照
            {
                komaManager.Motikomas.Remove(selectedKoma);
                komaManager.DeleteTehudaTile(selectedKoma.Position);
                komaManager.komas.Add(selectedKoma);
                selectedKoma.Move(clickTileObj.positionInt);
                phase = Phase.Player1Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(1,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
            }
            else if(IsClickKoma1(clickTileObj))
            {
                phase = Phase.Player1KomaMoveSelection;
                return;
            }
            else
            {
                phase = Phase.Player1KomaSelection;
                mapManager.ResetSetPanels(setTiles);
                return;
            }
        }
    }

    void P2MotiKomaMoveSelection()
    {
        //持ち駒を選択した状態でもう一度クリックしたときこの関数に来る。
        // TODO
        //クリックしたタイルを取得
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //クリックしたタイルが手札タイルじゃないならクリックしたタイルと同じポジションの駒がいるかを駒のリストを使って参照
        if (clickTileObj)
        {
            if (clickTileObj.tag != "TehudaTile" && setTiles.Contains(clickTileObj))
            {
                komaManager.DeleteTehudaTile(selectedKoma.Position);
                komaManager.Motikomas.Remove(selectedKoma);
                komaManager.komas.Add(selectedKoma);
                selectedKoma.Move(clickTileObj.positionInt);
                phase = Phase.Player2Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(2,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
            }
            else if (IsClickKoma2(clickTileObj))
            {
                phase = Phase.Player2KomaMoveSelection;
                return;
            }
            else
            {
                phase = Phase.Player2KomaSelection;
                mapManager.ResetSetPanels(setTiles);
                return;
            }
        }
    }

    void Player1KomaMoveSelection()
    {
        TileObj clickTileObj = mapManager.GetClickTileObj();
        
        if (clickTileObj) //クリックした場所にタイルが存在するなら
        {
            if (IsClickP1MotiKoma(clickTileObj)) //持ち駒なら持ち駒phaseに
            {
                mapManager.ResetMovablePanels(movableTiles);
                phase = Phase.Player1MotiKomaMoveSelection;
                return;
            }
            if (IsClickKoma1(clickTileObj)) //自駒ならなにもしない
            {
                return;
            }
            else if (OnTileP2Check(clickTileObj)) //相手の駒が居たら
            {
                if (movableTiles.Contains(clickTileObj)) //移動範囲内なら
                {
                    int firstPos = selectedKoma.Position.y;
                    int lastPos = clickTileObj.positionInt.y;
                    int moveLength = lastPos - firstPos;
                    Koma enemyKoma = komaManager.GetP2Koma(clickTileObj.positionInt); //その座標の駒を取得
                    if (enemyKoma.name == "koma_8") //取得した敵駒が王ならプレイヤー１の勝ちにする。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                        IsGameFinished = true;
                    }
                    else if (selectedKoma.name == "Seiken")
                    {
                        if (moveLength > 0)
                        {
                            komaManager.DeleteKoma(enemyKoma.name);
                            while (Mathf.Abs(moveLength) != 0)
                            {
                                enemyKoma = komaManager.GetP2Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                                if (enemyKoma != null)
                                {
                                    komaManager.DeleteKoma(enemyKoma.name);
                                    if (enemyKoma.name == "koma_8")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                                    {
                                        IsGameFinished = true;
                                    }
                                }
                                moveLength--;
                            }

                        }
                        else if (moveLength < 0)
                        {
                            while (Mathf.Abs(moveLength) != 0)
                            {
                                enemyKoma = komaManager.GetP2Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                                if (enemyKoma != null)
                                {
                                    komaManager.DeleteKoma(enemyKoma.name);
                                    if (enemyKoma.name == "koma_8")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                                    {
                                        IsGameFinished = true;
                                    }
                                }
                                moveLength++;
                            }
                        }
                        else
                        {
                            Debug.Log("自分のマスを選択したときのはず。");
                        }
                    }
                    else//取得した駒が王以外なら移動してフェーズを変える。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                    }
                    mapManager.PosCursor(2);
                    selectedKoma.Move(clickTileObj.positionInt);
                    phase = Phase.Player1Narration;
                    SoundManager.Instance.PlaySE(0);
                    narration.WordCombine(1,clickTileObj.positionInt,selectedKoma.PieceName,false);
                    SoundManager.Instance.PlayNarration();
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }
            else//自分、相手の駒がない時
            {
                //クリックしたタイルが移動範囲に含まれるなら
                if (movableTiles.Contains(clickTileObj))
                {
                    //selectedKomaをタイルまで移動させる。
                    int firstPos = selectedKoma.Position.y;
                    int lastPos = clickTileObj.positionInt.y;
                    int moveLength = lastPos - firstPos;
                    if(selectedKoma.name == "Seiken")
                    {
                        Debug.Log(moveLength);
                        if(moveLength > 0)
                        {
                            while(Mathf.Abs(moveLength) != 0)
                            {
                                Koma enemyKoma = komaManager.GetP2Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                                if(enemyKoma != null)
                                {
                                    komaManager.DeleteKoma(enemyKoma.name);
                                    if (enemyKoma.name == "koma_8")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                                    {
                                        IsGameFinished = true;
                                    }
                                }
                                moveLength--;
                            }
                            
                        }
                        else if(moveLength < 0)
                        {
                            while (Mathf.Abs(moveLength) != 0)
                            {
                                Koma enemyKoma = komaManager.GetP2Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                                if (enemyKoma != null)
                                {
                                    komaManager.DeleteKoma(enemyKoma.name);
                                    if (enemyKoma.name == "koma_8")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                                    {
                                        IsGameFinished = true;
                                    }
                                }
                                moveLength++;
                            }
                        }
                        else
                        {
                            Debug.Log("自分のマスを選択したときのはず。");
                        }
                    }
                    selectedKoma.Move(clickTileObj.positionInt);
                    phase = Phase.Player1Narration;
                    SoundManager.Instance.PlaySE(0);
                    narration.WordCombine(1,clickTileObj.positionInt,selectedKoma.PieceName,false);
                    SoundManager.Instance.PlayNarration();
                    mapManager.PosCursor(2);
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }   
        }
    }

    void Player2KomaMoveSelection()
    {
        TileObj clickTileObj = mapManager.GetClickTileObj();

        if (clickTileObj) //クリックした場所にタイルが存在するなら
        {
            if (IsClickP2MotiKoma(clickTileObj))//持ち駒なら持ち駒phaseに
            {
                mapManager.ResetMovablePanels(movableTiles);
                phase = Phase.Player2MotiKomaMoveSelection;
                return;
            }
            if (IsClickKoma2(clickTileObj))//自駒ならなにもしない
            {
                return;
            }
            else if (OnTileP1Check(clickTileObj))//相手の駒が居たら
            {
                if (movableTiles.Contains(clickTileObj)) //移動範囲内なら
                {
                    int firstPos = selectedKoma.Position.y;
                    int lastPos = clickTileObj.positionInt.y;
                    int moveLength = lastPos - firstPos;
                    Koma enemyKoma = komaManager.GetP1Koma(clickTileObj.positionInt);//その座標の駒を取得
                    if (enemyKoma.name == "koma_0")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                        IsGameFinished = true;
                    }
                    else if (selectedKoma.name == "Seiken")
                    {
                        if (moveLength > 0)
                        {
                            komaManager.DeleteKoma(enemyKoma.name);
                            while (Mathf.Abs(moveLength) != 0)
                            {
                                enemyKoma = komaManager.GetP1Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                                if (enemyKoma != null)
                                {
                                    komaManager.DeleteKoma(enemyKoma.name);
                                    if (enemyKoma.name == "koma_0")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                                    {
                                        IsGameFinished = true;
                                    }
                                }
                                moveLength--;
                            }

                        }
                        else if (moveLength < 0)
                        {
                            while (Mathf.Abs(moveLength) != 0)
                            {
                                enemyKoma = komaManager.GetP1Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                                if (enemyKoma != null)
                                {
                                    komaManager.DeleteKoma(enemyKoma.name);
                                    if (enemyKoma.name == "koma_0")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                                    {
                                        IsGameFinished = true;
                                    }
                                }
                                moveLength++;
                            }
                        }
                        else
                        {
                            Debug.Log("自分のマスを選択したときのはず。");
                        }
                    }
                    else//取得した駒が王以外なら移動してフェーズを変える。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                    }
                    mapManager.PosCursor(2);
                    selectedKoma.Move(clickTileObj.positionInt);
                    phase = Phase.Player2Narration;
                    SoundManager.Instance.PlaySE(0);
                    narration.WordCombine(2,clickTileObj.positionInt,selectedKoma.PieceName,false);
                    SoundManager.Instance.PlayNarration();
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }
            else//自分、相手の駒がない時
            {
                //クリックしたタイルが移動範囲に含まれるなら
                if (movableTiles.Contains(clickTileObj))
                {
                    //selectedKomaをタイルまで移動させる。
                    int firstPos = selectedKoma.Position.y;
                    int lastPos = clickTileObj.positionInt.y;
                    int moveLength = lastPos - firstPos;
                    if (selectedKoma.name == "Seiken")
                    {
                        Debug.Log(moveLength);
                        if (moveLength > 0)
                        {
                            while (Mathf.Abs(moveLength) != 0)
                            {
                                Koma enemyKoma = komaManager.GetP1Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                                if (enemyKoma != null)
                                {
                                    komaManager.DeleteKoma(enemyKoma.name);
                                    if (enemyKoma.name == "koma_0")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                                    {
                                        IsGameFinished = true;
                                    }
                                }
                                moveLength--;
                            }

                        }
                        else if (moveLength < 0)
                        {
                            while (Mathf.Abs(moveLength) != 0)
                            {
                                Koma enemyKoma = komaManager.GetP1Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                                if (enemyKoma != null)
                                {
                                    komaManager.DeleteKoma(enemyKoma.name);
                                    if (enemyKoma.name == "koma_0")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                                    {
                                        IsGameFinished = true;
                                    }
                                }
                                moveLength++;
                            }
                        }
                        else
                        {
                            Debug.Log("自分のマスを選択したときのはず。");
                        }
                    }
                    selectedKoma.Move(clickTileObj.positionInt);
                    phase = Phase.Player2Narration;
                    SoundManager.Instance.PlaySE(0);
                    narration.WordCombine(2,clickTileObj.positionInt,selectedKoma.PieceName,false);
                    SoundManager.Instance.PlayNarration();
                    mapManager.PosCursor(2);
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }
        }
    }

    private void Player1Win() //ここにプレイヤー１が勝った時の処理をお願いします。
    {
        SoundManager.Instance.StopBGM();
        phase = Phase.GameEnd;
        resultManager.winner = 1;
        Debug.Log("p1Win");
        resultManager.ShowGameEndUI();
    }

    private void Player2Win()
    {
        SoundManager.Instance.StopBGM();
        phase = Phase.GameEnd;
        resultManager.winner = 2;
        Debug.Log("p2Win");
        resultManager.ShowGameEndUI();
    }

    

    private void TurnPlayerUIMove(int nextTurnPlayer)
    {
        if (nextTurnPlayer == 1)
        {
            P2TurnUI.SetActive(false);
            P1TurnUI.SetActive(true);
        }
        else if (nextTurnPlayer == 2)
        {
            P1TurnUI.SetActive(false);
            P2TurnUI.SetActive(true);
        }
    }

    public void KomaChange(int num, Sprite image)
    {
        if(phase == Phase.Player1Gacha)
        {
            komaManager.IncreaceGachaKoma(komaManager.defeatedKomas[0], "P1Koma", num, image);
        }
        else if (phase == Phase.Player2Gacha)
        {
            komaManager.IncreaceGachaKoma(komaManager.defeatedKomas[0], "P2Koma", num, image);
        }
    }

    public void GachaFinish()
    {
        komaManager.gameObject.SetActive(true);
        if (phase == Phase.Player1Gacha)
        {
            phase = Phase.Player2KomaSelection;
        }
        else if (phase == Phase.Player2Gacha)
        {
            phase = Phase.Player1KomaSelection;
        }
    }
}