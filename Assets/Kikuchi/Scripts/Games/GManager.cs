using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    // �t�F�[�Y�̊Ǘ�
    enum Phase
    {
        Player1KomaSelection, //��I��
        Player1KomaMoveSelection,//��ړ�
        Player2KomaSelection,
        Player2KomaMoveSelection,

    }

    //��������������part14    7:44

    //�I�������L�����̕ێ�

    Koma selectedKoma;
    //�I���L�����̈ړ��\�͈͂̕ێ�
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

    //�L�����I��
    //�L�����ړ�
    //�v���C���[���N���b�N�����珈��
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
        //����擾���Ĉړ��͈͂�\��
        Koma koma = null;
        koma = komaManager.GetKoma(clickTileObj.positionInt);
        if (koma != null)
        {
            selectedKoma = koma;
            mapManager.ResetMovablePanels(movableTiles);
            //�ړ��͈͂�\��
            mapManager.ShowMovablePanels(selectedKoma, movableTiles);
            phase = Phase.Player1KomaMoveSelection;
            return true;
        }
        return false;
    }

    void PlayerCharactereSelection()
    {
        //�N���b�N�����^�C�����擾
        //���̏�ɋ����Ȃ�
        TileObj clickTileObj = mapManager.GetClickTileObj();
        if (IsClickCharacter(clickTileObj))
        {
            phase = Phase.Player1KomaMoveSelection;
        }
    }

    void PlayerCharactereMoveSelection()
    {

        //�N���b�N�����ꏊ���ړ��͈͂Ȃ�ړ������ēG�̃t�F�[�Y��
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //�L�����N�^�[������Ȃ�
        if(IsClickCharacter(clickTileObj))
        {
            return;
        }

        if (selectedKoma)
        {
            //�N���b�N�����^�C�����ړ��͈͂Ɋ܂܂��Ȃ�
            if (movableTiles.Contains(clickTileObj))
            {
                //selectedKoma���^�C���܂ňړ�������B
                selectedKoma.Move(clickTileObj.positionInt);
            }
            mapManager.ResetMovablePanels(movableTiles);
            selectedKoma = null;

        }

    }

}
