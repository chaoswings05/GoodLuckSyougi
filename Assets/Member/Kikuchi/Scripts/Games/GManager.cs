using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GManager : SingletonMonoBehaviour<GManager>
{
    // フェーズの管理
    public enum Phase
    {
        None,
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
        Player1PieceSkill,
        Player2PieceSkill,
        Player1WindowSelection,
        Player2WindowSelection,
        GameEnd,
    }

    //選択したキャラの保持
    public Koma selectedKoma;

    //選択キャラの移動可能範囲の保持
    public List<TileObj> movableTiles = new List<TileObj>();
    public List<TileObj> setTiles = new List<TileObj>();

    [SerializeField] private GameObject P1TurnUI;
    [SerializeField] private GameObject P2TurnUI;

    public Phase gamePhase = Phase.None;
    [SerializeField] private KomaManager komaManager;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private NarrationBuild narration = null;
    [SerializeField] private GachaSystem gachaSystem = null;
    [SerializeField] private ResultManager resultManager = null;
    [SerializeField] private WindowManager windowManager = null;

    private bool IsGameFinished = false;
    private bool IsKillSelf = false;

    void Start()
    {
        SoundManager.Instance.PlayBGM(1);
        gamePhase = Phase.Player1KomaSelection;
    }

    //キャラ選択
    //キャラ移動
    //プレイヤーがクリックしたら処理

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gamePhase != Phase.GameEnd)
        {
            PlayerClickAction();
        }

        switch(gamePhase)
        {
            case Phase.Player1Narration:
            if (!SoundManager.Instance.IsNarrationPlaying)
            {
                if (IsGameFinished && !IsKillSelf)
                {
                    Player1Win();
                }
                else if (IsGameFinished && IsKillSelf)
                {
                    Player2Win();
                }
                else if (komaManager.defeatedKomas.Count > 0)
                {
                    windowManager.UnshowP1PieceEffectWindow();
                    windowManager.UnshowP2PieceEffectWindow();
                    gamePhase = Phase.Player1Gacha;
                    gachaSystem.GachaStart(komaManager.defeatedKomas.Count);
                }
                else
                {
                    TurnPlayerUIMove(2);
                    gamePhase = Phase.Player2KomaSelection;
                }
            }
            break;

            case Phase.Player2Narration:
            if (!SoundManager.Instance.IsNarrationPlaying)
            {
                if (IsGameFinished && !IsKillSelf)
                {
                    Player2Win();
                }
                else if (IsGameFinished && IsKillSelf)
                {
                    Player1Win();
                }
                else if (komaManager.defeatedKomas.Count > 0)
                {
                    windowManager.UnshowP1PieceEffectWindow();
                    windowManager.UnshowP2PieceEffectWindow();
                    gamePhase = Phase.Player2Gacha;
                    gachaSystem.GachaStart(komaManager.defeatedKomas.Count);
                }
                else
                {
                    TurnPlayerUIMove(1);
                    gamePhase = Phase.Player1KomaSelection;
                }
            }
            break;

            default:
            break;
        }
    }

    void PlayerClickAction()
    {
        switch (gamePhase)
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

            case Phase.Player1PieceSkill:
                if (selectedKoma.name == "Ninja")
                {
                    P1NinjaMoveSelection();
                }
                else if (selectedKoma.name == "Hikyo")
                {
                    P1HikyoMoveSelection();
                }
                break;

            case Phase.Player2PieceSkill:
                if (selectedKoma.name == "Ninja")
                {
                    P2NinjaMoveSelection();
                }
                else if (selectedKoma.name == "Hikyo")
                {
                    P2HikyoMoveSelection();
                }
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
            if (koma.name == "Ninja")
            {
                gamePhase = Phase.Player1WindowSelection;
                windowManager.ShowNinjaWindow();
            }
            else if (koma.name == "Hikyo")
            {
                gamePhase = Phase.Player1WindowSelection;
                windowManager.ShowHikyoWindow();
            }
            else
            {
                gamePhase = Phase.Player1KomaMoveSelection;
            }
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
            if (koma.name == "Ninja")
            {
                gamePhase = Phase.Player2WindowSelection;
                windowManager.ShowNinjaWindow();
            }
            else if (koma.name == "Hikyo")
            {
                gamePhase = Phase.Player2WindowSelection;
                windowManager.ShowHikyoWindow();
            }
            else
            {
                gamePhase = Phase.Player2KomaMoveSelection;
            }
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
            gamePhase = Phase.Player1MotiKomaMoveSelection;
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
            gamePhase = Phase.Player2MotiKomaMoveSelection;
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
        if(clickTileObj != null)
        {
            //その上に駒がいるなら
            if (IsClickP1MotiKoma(clickTileObj)) //持ち駒取得メソッドを作ってクリックしたタイルが手札タイルか調べる。
            {
                gamePhase = Phase.Player1MotiKomaMoveSelection;
            }
            else if (IsClickKoma1(clickTileObj) && selectedKoma.name != "Ninja" && selectedKoma.name != "Hikyo")
            {
                gamePhase = Phase.Player1KomaMoveSelection;
            }
        }
    }

    void Player2KomaSelection()
    {
        //クリックしたタイルを取得
        TileObj clickTileObj = mapManager.GetClickTileObj();
        if (clickTileObj != null)
        {
            //その上に駒がいるなら
            if (IsClickP2MotiKoma(clickTileObj)) //持ち駒取得メソッドを作ってクリックしたタイルが手札タイルか調べる。
            {
                gamePhase = Phase.Player2MotiKomaMoveSelection;
            }
            else if (IsClickKoma2(clickTileObj) && selectedKoma.name != "Ninja" && selectedKoma.name != "Hikyo")
            {
                gamePhase = Phase.Player2KomaMoveSelection;
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
            if (setTiles.Contains(clickTileObj)) //クリックしたタイルが手札タイルじゃないならクリックしたタイルと同じポジションの駒がいるかを駒のリストを使って参照
            {
                komaManager.Motikomas.Remove(selectedKoma);
                komaManager.DeleteTehudaTile(selectedKoma.Position);
                komaManager.RearrangeMotiKoma();
                komaManager.komas.Add(selectedKoma);
                selectedKoma.Move(clickTileObj.positionInt);
                gamePhase = Phase.Player1Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(1,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
            }
            else if(IsClickKoma1(clickTileObj))
            {
                gamePhase = Phase.Player1KomaMoveSelection;
                return;
            }
            else
            {
                gamePhase = Phase.Player1KomaSelection;
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
            if (setTiles.Contains(clickTileObj))
            {
                komaManager.DeleteTehudaTile(selectedKoma.Position);
                komaManager.Motikomas.Remove(selectedKoma);
                komaManager.RearrangeMotiKoma();
                komaManager.komas.Add(selectedKoma);
                selectedKoma.Move(clickTileObj.positionInt);
                gamePhase = Phase.Player2Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(2,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
            }
            else if (IsClickKoma2(clickTileObj))
            {
                gamePhase = Phase.Player2KomaMoveSelection;
                return;
            }
            else
            {
                gamePhase = Phase.Player2KomaSelection;
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
                gamePhase = Phase.Player1MotiKomaMoveSelection;
                return;
            }

            if (selectedKoma != null && selectedKoma.name == "Seiken" && movableTiles.Contains(clickTileObj))
            {
                int firstPos = selectedKoma.Position.y;
                int lastPos = clickTileObj.positionInt.y;
                int moveLength = lastPos - firstPos;
                if(moveLength > 0)
                {
                    while(Mathf.Abs(moveLength) != 0)
                    {
                        Koma enemyKoma = komaManager.GetP2Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                        if (enemyKoma == null)
                        {
                            enemyKoma = komaManager.GetP1Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                        }
                        if(enemyKoma != null)
                        {
                            if (enemyKoma.name != "Kukkyou")
                            {
                                komaManager.DeleteKoma(enemyKoma.name);
                            }
                            if (enemyKoma.name == "koma_8") //取得した敵駒が王ならプレイヤー１の勝ちにする。
                            {
                                IsGameFinished = true;
                            }
                            if (enemyKoma.name == "koma_0")
                            {
                                IsGameFinished = true;
                                IsKillSelf = true;
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
                        if (enemyKoma == null)
                        {
                            enemyKoma = komaManager.GetP1Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                        }
                        if (enemyKoma != null)
                        {
                            if (enemyKoma.name != "Kukkyou")
                            {
                                komaManager.DeleteKoma(enemyKoma.name);
                            }
                            if (enemyKoma.name == "koma_8")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                            {
                                IsGameFinished = true;
                            }
                            if (enemyKoma.name == "koma_0")
                            {
                                IsGameFinished = true;
                                IsKillSelf = true;
                            }
                        }
                        moveLength++;
                    }
                }
                selectedKoma.Move(clickTileObj.positionInt);
                gamePhase = Phase.Player1Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(1,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }
            else if (IsClickKoma1(clickTileObj)) //自駒ならなにもしない
            {
                return;
            }
            else if (OnTileP2Check(clickTileObj)) //相手の駒が居たら
            {
                if (movableTiles.Contains(clickTileObj)) //移動範囲内なら
                {
                    bool IsStartFromEnemyZone = false;
                    if (selectedKoma.Position.y == 7 || selectedKoma.Position.y == 8 || selectedKoma.Position.y == 9)
                    {
                        IsStartFromEnemyZone = true;
                    }
                    bool IsEndOnEnemyZone = false;
                    if (clickTileObj.positionInt.y == 7 || clickTileObj.positionInt.y == 8 || clickTileObj.positionInt.y == 9)
                    {
                        IsEndOnEnemyZone = true;
                    }
                    Koma enemyKoma = komaManager.GetP2Koma(clickTileObj.positionInt); //その座標の駒を取得
                    if (enemyKoma.name == "koma_8") //取得した敵駒が王ならプレイヤー１の勝ちにする。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                        IsGameFinished = true;
                    }
                    else//取得した駒が王以外なら移動してフェーズを変える。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                    }
                    selectedKoma.Move(clickTileObj.positionInt);
                    SoundManager.Instance.PlaySE(0);
                    if (selectedKoma.CanReverse && (IsStartFromEnemyZone || IsEndOnEnemyZone))
                    {
                        gamePhase = Phase.Player1WindowSelection;
                        windowManager.ShowReverseWindow();
                    }
                    else
                    {
                        gamePhase = Phase.Player1Narration;
                        narration.WordCombine(1,clickTileObj.positionInt,selectedKoma.PieceName,false);
                        SoundManager.Instance.PlayNarration();
                        if (selectedKoma.name == "Haiyu")
                        {
                            selectedKoma.HaiyuChange(enemyKoma.PieceName, enemyKoma.name, enemyKoma.nameObj.text);
                        }
                        if (selectedKoma.name == "Fugo")
                        {
                            Koma newKoma = Instantiate(komaManager.defeatedKomas[0], komaManager.defeatedKomas[0].transform);
                            newKoma.transform.SetParent(komaManager.transform);
                            komaManager.defeatedKomas.Add(newKoma);
                        }
                        selectedKoma = null;
                    }
                }
                mapManager.ResetMovablePanels(movableTiles);
            }
            else//自分、相手の駒がない時
            {
                //クリックしたタイルが移動範囲に含まれるなら
                if (movableTiles.Contains(clickTileObj))
                {
                    bool IsStartFromEnemyZone = false;
                    if (selectedKoma.Position.y == 7 || selectedKoma.Position.y == 8 || selectedKoma.Position.y == 9)
                    {
                        IsStartFromEnemyZone = true;
                    }
                    bool IsEndOnEnemyZone = false;
                    if (clickTileObj.positionInt.y == 7 || clickTileObj.positionInt.y == 8 || clickTileObj.positionInt.y == 9)
                    {
                        IsEndOnEnemyZone = true;
                    }
                    //selectedKomaをタイルまで移動させる。
                    selectedKoma.Move(clickTileObj.positionInt);
                    SoundManager.Instance.PlaySE(0);
                    if (selectedKoma.CanReverse && (IsStartFromEnemyZone || IsEndOnEnemyZone))
                    {
                        gamePhase = Phase.Player1WindowSelection;
                        windowManager.ShowReverseWindow();
                    }
                    else
                    {
                        gamePhase = Phase.Player1Narration;
                        narration.WordCombine(1,clickTileObj.positionInt,selectedKoma.PieceName,false);
                        SoundManager.Instance.PlayNarration();
                        selectedKoma = null;
                    }
                }
                mapManager.ResetMovablePanels(movableTiles);
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
                gamePhase = Phase.Player2MotiKomaMoveSelection;
                return;
            }

            if (selectedKoma != null && selectedKoma.name == "Seiken" && movableTiles.Contains(clickTileObj))
            {
                int firstPos = selectedKoma.Position.y;
                int lastPos = clickTileObj.positionInt.y;
                int moveLength = lastPos - firstPos;
                if(moveLength > 0)
                {
                    while(Mathf.Abs(moveLength) != 0)
                    {
                        Koma enemyKoma = komaManager.GetP1Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                        if (enemyKoma == null)
                        {
                            enemyKoma = komaManager.GetP2Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                        }
                        if(enemyKoma != null)
                        {
                            if (enemyKoma.name != "Kukkyou")
                            {
                                komaManager.DeleteKoma(enemyKoma.name);
                            }
                            if (enemyKoma.name == "koma_0")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                            {
                                IsGameFinished = true;
                            }
                            if (enemyKoma.name == "koma_8")
                            {
                                IsGameFinished = true;
                                IsKillSelf = true;
                            }
                        }
                        moveLength--;
                    }
                }
                else if(moveLength < 0)
                {
                    while (Mathf.Abs(moveLength) != 0)
                    {
                        Koma enemyKoma = komaManager.GetP1Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                        if (enemyKoma == null)
                        {
                            enemyKoma = komaManager.GetP2Koma(new Vector2Int(clickTileObj.positionInt.x, firstPos + moveLength));
                        }
                        if (enemyKoma != null)
                        {
                            if (enemyKoma.name != "Kukkyou")
                            {
                                komaManager.DeleteKoma(enemyKoma.name);
                            }
                            if (enemyKoma.name == "koma_0")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                            {
                                IsGameFinished = true;
                            }
                            if (enemyKoma.name == "koma_8")
                            {
                                IsGameFinished = true;
                                IsKillSelf = true;
                            }
                        }
                        moveLength++;
                    }
                }
                selectedKoma.Move(clickTileObj.positionInt);
                gamePhase = Phase.Player2Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(2,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }
            else if (IsClickKoma2(clickTileObj))//自駒ならなにもしない
            {
                return;
            }
            else if (OnTileP1Check(clickTileObj))//相手の駒が居たら
            {
                if (movableTiles.Contains(clickTileObj)) //移動範囲内なら
                {
                    bool IsStartFromEnemyZone = false;
                    if (selectedKoma.Position.y == 1 || selectedKoma.Position.y == 2 || selectedKoma.Position.y == 3)
                    {
                        IsStartFromEnemyZone = true;
                    }
                    bool IsEndOnEnemyZone = false;
                    if (clickTileObj.positionInt.y == 1 || clickTileObj.positionInt.y == 2 || clickTileObj.positionInt.y == 3)
                    {
                        IsEndOnEnemyZone = true;
                    }
                    Koma enemyKoma = komaManager.GetP1Koma(clickTileObj.positionInt);//その座標の駒を取得
                    if (enemyKoma.name == "koma_0")//取得した敵駒が王ならプレイヤー１の勝ちにする。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                        IsGameFinished = true;
                    }
                    else//取得した駒が王以外なら移動してフェーズを変える。
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                    }
                    selectedKoma.Move(clickTileObj.positionInt);
                    SoundManager.Instance.PlaySE(0);
                    if (selectedKoma.CanReverse && (IsStartFromEnemyZone || IsEndOnEnemyZone))
                    {
                        gamePhase = Phase.Player2WindowSelection;
                        windowManager.ShowReverseWindow();
                    }
                    else
                    {
                        gamePhase = Phase.Player2Narration;
                        narration.WordCombine(2,clickTileObj.positionInt,selectedKoma.PieceName,false);
                        SoundManager.Instance.PlayNarration();
                        if (selectedKoma.name == "Haiyu")
                        {
                            selectedKoma.HaiyuChange(enemyKoma.PieceName, enemyKoma.name, enemyKoma.nameObj.text);
                        }
                        if (selectedKoma.name == "Fugo")
                        {
                            Koma newKoma = Instantiate(komaManager.defeatedKomas[0], komaManager.defeatedKomas[0].transform);
                            newKoma.transform.SetParent(komaManager.transform);
                            komaManager.defeatedKomas.Add(newKoma);
                        }
                        selectedKoma = null;
                    }
                }
                mapManager.ResetMovablePanels(movableTiles);
            }
            else//自分、相手の駒がない時
            {
                //クリックしたタイルが移動範囲に含まれるなら
                if (movableTiles.Contains(clickTileObj))
                {
                    bool IsStartFromEnemyZone = false;
                    if (selectedKoma.Position.y == 1 || selectedKoma.Position.y == 2 || selectedKoma.Position.y == 3)
                    {
                        IsStartFromEnemyZone = true;
                    }
                    bool IsEndOnEnemyZone = false;
                    if (clickTileObj.positionInt.y == 1 || clickTileObj.positionInt.y == 2 || clickTileObj.positionInt.y == 3)
                    {
                        IsEndOnEnemyZone = true;
                    }
                    //selectedKomaをタイルまで移動させる。
                    selectedKoma.Move(clickTileObj.positionInt);
                    SoundManager.Instance.PlaySE(0);
                    if (selectedKoma.CanReverse && (IsStartFromEnemyZone || IsEndOnEnemyZone))
                    {
                        gamePhase = Phase.Player2WindowSelection;
                        windowManager.ShowReverseWindow();
                    }
                    else
                    {
                        gamePhase = Phase.Player2Narration;
                        narration.WordCombine(2,clickTileObj.positionInt,selectedKoma.PieceName,false);
                        SoundManager.Instance.PlayNarration();
                        selectedKoma = null;
                    }
                }
                mapManager.ResetMovablePanels(movableTiles);
            }
        }
    }

    private void Player1Win() //ここにプレイヤー１が勝った時の処理をお願いします。
    {
        SoundManager.Instance.StopBGM();
        gamePhase = Phase.GameEnd;
        resultManager.winner = 1;
        resultManager.ShowGameEndUI();
    }

    private void Player2Win()
    {
        SoundManager.Instance.StopBGM();
        gamePhase = Phase.GameEnd;
        resultManager.winner = 2;
        resultManager.ShowGameEndUI();
    }

    public void Player1GiveUp()
    {
        SoundManager.Instance.StopBGM();
        gamePhase = Phase.GameEnd;
        resultManager.winner = 2;
        resultManager.ShowGiveUpUI();
    }

    public void Player2GiveUp()
    {
        SoundManager.Instance.StopBGM();
        gamePhase = Phase.GameEnd;
        resultManager.winner = 1;
        resultManager.ShowGiveUpUI();
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
        if(gamePhase == Phase.Player1Gacha)
        {
            komaManager.IncreaceGachaKoma(komaManager.defeatedKomas[0], "P1Koma", num, image);
        }
        else if (gamePhase == Phase.Player2Gacha)
        {
            komaManager.IncreaceGachaKoma(komaManager.defeatedKomas[0], "P2Koma", num, image);
        }
    }

    public void GachaFinish()
    {
        if (gamePhase == Phase.Player1Gacha)
        {
            TurnPlayerUIMove(2);
            gamePhase = Phase.Player2KomaSelection;
        }
        else if (gamePhase == Phase.Player2Gacha)
        {
            TurnPlayerUIMove(1);
            gamePhase = Phase.Player1KomaSelection;
        }
    }

    public void SetNinjaEffectMoveTile()
    {
        mapManager.ResetMovablePanels(movableTiles);
        mapManager.ResetSetPanels(setTiles);
        mapManager.ShowSetPanels(selectedKoma, setTiles);
    }

    public void SetHikyoEffectMoveTile()
    {
        mapManager.ResetMovablePanels(movableTiles);
        mapManager.ResetSetPanels(setTiles);
        mapManager.ShowHikyoSkillSetPanels(selectedKoma, setTiles);
    }

    private void P1NinjaMoveSelection()
    {
        //クリックしたタイルを取得
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //クリックしたタイルが手札タイルじゃないならクリックしたタイルと同じポジションの駒がいるかを駒のリストを使って参照
        if (clickTileObj)
        {
            if (setTiles.Contains(clickTileObj))
            {
                selectedKoma.Move(clickTileObj.positionInt);
                gamePhase = Phase.Player1Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(1,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
            }
        }
    }

    private void P1HikyoMoveSelection()
    {
        //クリックしたタイルを取得
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //クリックしたタイルが手札タイルじゃないならクリックしたタイルと同じポジションの駒がいるかを駒のリストを使って参照
        if (clickTileObj)
        {
            if (setTiles.Contains(clickTileObj))
            {
                Koma shiftKoma = komaManager.GetKoma(clickTileObj.positionInt);
                shiftKoma.Move(selectedKoma.Position);
                selectedKoma.Move(clickTileObj.positionInt);
                gamePhase = Phase.Player1Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(1,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
            }
        }
    }

    private void P2NinjaMoveSelection()
    {
        //クリックしたタイルを取得
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //クリックしたタイルが手札タイルじゃないならクリックしたタイルと同じポジションの駒がいるかを駒のリストを使って参照
        if (clickTileObj)
        {
            if (setTiles.Contains(clickTileObj))
            {
                selectedKoma.Move(clickTileObj.positionInt);
                gamePhase = Phase.Player2Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(2,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
            }
        }
    }

    private void P2HikyoMoveSelection()
    {
        //クリックしたタイルを取得
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //クリックしたタイルが手札タイルじゃないならクリックしたタイルと同じポジションの駒がいるかを駒のリストを使って参照
        if (clickTileObj)
        {
            if (setTiles.Contains(clickTileObj))
            {
                Koma shiftKoma = komaManager.GetKoma(clickTileObj.positionInt);
                shiftKoma.Move(selectedKoma.Position);
                selectedKoma.Move(clickTileObj.positionInt);
                gamePhase = Phase.Player2Narration;
                SoundManager.Instance.PlaySE(0);
                narration.WordCombine(2,clickTileObj.positionInt,selectedKoma.PieceName,false);
                SoundManager.Instance.PlayNarration();
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
            }
        }
    }
}