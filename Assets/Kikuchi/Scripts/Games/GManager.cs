using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    Phase phase;
    [SerializeField]
    KomaManager komaManager;
    [SerializeField]
    MapManager mapManager;

    void Start()
    {
        phase = Phase.Player1KomaSelection;
    }

    //キャラ選択
    //キャラ移動
    //プレイヤーがクリックしたら処理
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayerClickAction();
        }
    }

    void PlayerClickAction()
    {
        switch (phase)
        {
            case Phase.Player1KomaSelection:

                PlayerCharactereSelection();

                break;
            case Phase.Player1KomaMoveSelection:

                PlayerCharactereMoveSelection();

                break;
        }
    }

    bool IsClickCharacter(TileObj clickTileObj)
    {
        //駒を取得して移動範囲を表示
        Koma koma = null;
        koma = komaManager.GetKoma(clickTileObj.positionInt);
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

    void PlayerCharactereSelection()
    {
        //クリックしたタイルを取得
        //その上に駒がいるなら
        TileObj clickTileObj = mapManager.GetClickTileObj();
        if (IsClickCharacter(clickTileObj))
        {
            phase = Phase.Player1KomaMoveSelection;
        }
    }

    void PlayerCharactereMoveSelection()
    {

        //クリックした場所が移動範囲なら移動させて敵のフェーズへ
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //キャラクターがいるなら
        if(IsClickCharacter(clickTileObj))
        {
            return;
        }

        if (selectedKoma)
        {
            //クリックしたタイルが移動範囲に含まれるなら
            if (movableTiles.Contains(clickTileObj))
            {
                //selectedKomaをタイルまで移動させる。
                selectedKoma.Move(clickTileObj.positionInt);
            }
            mapManager.ResetMovablePanels(movableTiles);
            selectedKoma = null;

        }

    }

}
