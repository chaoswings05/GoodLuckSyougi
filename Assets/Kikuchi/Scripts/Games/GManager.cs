using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    // �t�F�[�Y�̊Ǘ�
    enum Phase
    {
        Player1KomaSelection, //��I��
        Player1KomaMoveSelection,//��ړ�
        Player2KomaSelection,
        Player2KomaMoveSelection,
        Player1MotiKomaMoveSelection,
        Player2MotiKomaMoveSelection,
        Player1Gacha,
        Player2Gacha,

    }


    //�I�������L�����̕ێ�

    Koma selectedKoma;
    //�I���L�����̈ړ��\�͈͂̕ێ�
    public List<TileObj> movableTiles = new List<TileObj>();
    public List<TileObj> setTiles = new List<TileObj>();

    [SerializeField] private Text TurnText;

    [SerializeField]
    Phase phase;
    [SerializeField]
    KomaManager komaManager;
    [SerializeField]
    MapManager mapManager;
    [Header("\n�K�`�����������߂̃{�^�����A�^�b�`���Ă��������B")]
    [SerializeField]
    Button button;
    [SerializeField]
    GameObject testGacha;

    private bool gameFinish;

    void Start()
    {
        phase = Phase.Player1KomaSelection;
    }

    //�L�����I��
    //�L�����ړ�
    //�v���C���[���N���b�N�����珈��

    public void Gacha()
    {
        GameObject gachaObj = null;
        if (phase == Phase.Player1MotiKomaMoveSelection)
        {
            mapManager.ResetSetPanels(setTiles);
            phase = Phase.Player1Gacha;
            gachaObj = testGacha;
            Koma gachaKoma = testGacha.GetComponent<Koma>();
            //�K�`���������Ĉ���������擾����B
            if(gachaKoma != null)
            {
                komaManager.IncreaceGachaKoma(gachaKoma, "P1Koma");
                phase = Phase.Player1KomaSelection;
            }
            else
            {
                Debug.Log("�K�`����ɁuKoma�v�X�N���v�g���A�^�b�`���Ă�������");
            }

        }
        if (phase == Phase.Player2MotiKomaMoveSelection)
        {
            mapManager.ResetSetPanels(setTiles);
            phase = Phase.Player2Gacha;
            //�K�`���������Ĉ���������擾����B
            gachaObj = testGacha;
            Koma gachaKoma = testGacha.GetComponent<Koma>();
            komaManager.IncreaceGachaKoma(gachaKoma, "P2Koma");
            phase = Phase.Player2KomaSelection;
        }

    }
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
            case Phase.Player1MotiKomaMoveSelection:
                P1MotiKomaMoveSelection();

                break;
            case Phase.Player2MotiKomaMoveSelection:
                P2MotiKomaMoveSelection();

                break;

        }
    }

    bool IsClickKoma(TileObj clickTileObj)
    {
        //����擾���Ĉړ��͈͂�\��
        Koma koma = null;
        koma = komaManager.GetKoma(clickTileObj.positionInt);
        if (koma != null)//�����Ȃ�
        {
            if (koma.tag == "P1Koma") 
            {
                selectedKoma = koma;
                mapManager.ResetMovablePanels(movableTiles);
                //�ړ��͈͂�\��
                mapManager.ShowMovablePanels(selectedKoma, movableTiles);
                phase = Phase.Player1KomaMoveSelection;
                return true;
            }
            
        }
        return false;
    }

    bool IsClickKoma1(TileObj clickTileObj)
    {
        //����擾���Ĉړ��͈͂�\��
        Koma koma = null;
        koma = komaManager.GetP1Koma(clickTileObj.positionInt);
        if (koma != null)//�����Ȃ�
        {
            selectedKoma = koma;
            mapManager.ResetSetPanels(setTiles);
            mapManager.ResetMovablePanels(movableTiles);
            //�ړ��͈͂�\��
            mapManager.ShowMovablePanels(selectedKoma, movableTiles);
            phase = Phase.Player1KomaMoveSelection;
            return true;
        }
        return false;
    }


    bool IsClickKoma2(TileObj clickTileObj)
    {
        //����擾���Ĉړ��͈͂�\��
        Koma koma = null;
        koma = komaManager.GetP2Koma(clickTileObj.positionInt);
        if (koma != null)
        {
            selectedKoma = koma;
            mapManager.ResetSetPanels(setTiles);
            mapManager.ResetMovablePanels(movableTiles);
            //�ړ��͈͂�\��
            mapManager.ShowMovablePanels(selectedKoma, movableTiles);
            phase = Phase.Player2KomaMoveSelection;
            return true; 
        }
        return false;
    }

    bool IsClickP1MotiKoma(TileObj clickTileObj)
    {
        //����擾���Ĉړ��͈͂�\��
        Koma koma = null;
        koma = komaManager.GetP1MotiKoma(clickTileObj.positionInt);//���X�g������Ď�D��������ɓ����B
        if (koma != null)//�����Ȃ�
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
        //����擾���Ĉړ��͈͂�\��
        Koma koma = null;
        koma = komaManager.GetP2MotiKoma(clickTileObj.positionInt);//���X�g������Ď�D��������ɓ����B
        if (koma != null)//�����Ȃ�
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
        //����擾���Ĉړ��͈͂�\��
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
        //����擾���Ĉړ��͈͂�\��
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
        //�N���b�N�����^�C�����擾
        //���̏�ɋ����Ȃ�
        TileObj clickTileObj = mapManager.GetClickTileObj();
        if(clickTileObj != null)
        {
    
            if (IsClickP1MotiKoma(clickTileObj)) //������擾���\�b�h������ăN���b�N�����^�C������D�^�C�������ׂ�B
            {
                Debug.Log("aaa");
                phase = Phase.Player1MotiKomaMoveSelection;
            }
            else if (IsClickKoma1(clickTileObj))
            {
                Debug.Log("www");
                phase = Phase.Player1KomaMoveSelection;
            }
        }
        
    }

    void Player2KomaSelection()
    {
        //�N���b�N�����^�C�����擾
        //���̏�ɋ����Ȃ�
        TileObj clickTileObj = mapManager.GetClickTileObj();
        if (clickTileObj != null)
        {
            if (IsClickP2MotiKoma(clickTileObj)) //������擾���\�b�h������ăN���b�N�����^�C������D�^�C�������ׂ�B
            {
                Debug.Log("aaa");
                phase = Phase.Player2MotiKomaMoveSelection;
            }
            else if (IsClickKoma2(clickTileObj))
            {
                Debug.Log("www");
                phase = Phase.Player2KomaMoveSelection;
            }

        }
    }

    void P1MotiKomaMoveSelection()
    {
        //�������I��������Ԃł�����x�N���b�N�����Ƃ����̊֐��ɗ���B
        // TODO
        //�N���b�N�����^�C�����擾
        TileObj clickTileObj = mapManager.GetClickTileObj();
        
        if (clickTileObj)
        {
            if (clickTileObj.tag != "TehudaTile" && setTiles.Contains(clickTileObj))//�N���b�N�����^�C������D�^�C������Ȃ��Ȃ�N���b�N�����^�C���Ɠ����|�W�V�����̋���邩����̃��X�g���g���ĎQ��
            {
                komaManager.Motikomas.Remove(selectedKoma);
                komaManager.DeleteTehudaTile(selectedKoma.Position);
                komaManager.komas.Add(selectedKoma);
                selectedKoma.Move(clickTileObj.positionInt);
                phase = Phase.Player2KomaSelection;
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
                TurnText.GetComponent<Text>().text = "���̎��";

            }
            else if(IsClickKoma1(clickTileObj))
            {
                phase = Phase.Player1KomaMoveSelection;
                return;
            }
            else
            {
                Debug.Log("Error");
                return;
            }
        }

    }

    void P2MotiKomaMoveSelection()
    {
        //�������I��������Ԃł�����x�N���b�N�����Ƃ����̊֐��ɗ���B
        // TODO
        //�N���b�N�����^�C�����擾
        TileObj clickTileObj = mapManager.GetClickTileObj();
        //�N���b�N�����^�C������D�^�C������Ȃ��Ȃ�N���b�N�����^�C���Ɠ����|�W�V�����̋���邩����̃��X�g���g���ĎQ��
        if (clickTileObj)
        {
            if (clickTileObj.tag != "TehudaTile" && setTiles.Contains(clickTileObj))
            {
                komaManager.DeleteTehudaTile(selectedKoma.Position);
                komaManager.Motikomas.Remove(selectedKoma);
                komaManager.komas.Add(selectedKoma);
                selectedKoma.Move(clickTileObj.positionInt);
                phase = Phase.Player1KomaSelection;
                mapManager.ResetSetPanels(setTiles);
                selectedKoma = null;
                TurnText.GetComponent<Text>().text = "���̎��";

            }
            else if (IsClickKoma2(clickTileObj))
            {
                phase = Phase.Player2KomaMoveSelection;
                return;
            }
            else
            {
                return;
            }
        }

    }

    void Player1KomaMoveSelection()
    {

        //�N���b�N�����ꏊ���ړ��͈͂Ȃ�ړ������ēG�̃t�F�[�Y��
        TileObj clickTileObj = mapManager.GetClickTileObj();
        
        if (clickTileObj) //�N���b�N�����ꏊ�Ƀ^�C�������݂���Ȃ�
        {
            if (IsClickP1MotiKoma(clickTileObj))
            {
                mapManager.ResetMovablePanels(movableTiles);
                phase = Phase.Player1MotiKomaMoveSelection;
                return;
            }
            if (IsClickKoma1(clickTileObj))//1p�̋����Ȃ�
            {
                return;
            }
            else if (OnTileP2Check(clickTileObj))
            {
                if (movableTiles.Contains(clickTileObj)) //�N���b�N�����ꏊ�ɑ���̋���Ĉړ��͈͓��Ȃ�
                {
                    Koma enemyKoma = komaManager.GetP2Koma(clickTileObj.positionInt);//���̍��W�̋���擾
                    if (enemyKoma.name == "koma_8")//�擾�����G����Ȃ�v���C���[�P�̏����ɂ���B
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                        Player1Win();
                    }
                    else//�擾��������ȊO�Ȃ�ړ����ăt�F�[�Y��ς���B
                    {
                        komaManager.DeleteKoma(enemyKoma.name);
                        selectedKoma.Move(clickTileObj.positionInt);
                    }
                    mapManager.PosCursor(2);
                    TurnText.GetComponent<Text>().text = "���̎��" ;
                    phase = Phase.Player2KomaSelection;
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }
            else
            {
                //�N���b�N�����^�C�����ړ��͈͂Ɋ܂܂��Ȃ�
                if (movableTiles.Contains(clickTileObj))
                {
                    //selectedKoma���^�C���܂ňړ�������B
                    selectedKoma.Move(clickTileObj.positionInt);
                    mapManager.PosCursor(2);
                    TurnText.GetComponent<Text>().text = "���̎��";
                    phase = Phase.Player2KomaSelection;
                    
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;

            }
            
        }

    }

    void Player2KomaMoveSelection()
    {

        //�N���b�N�����ꏊ���ړ��͈͂Ȃ�ړ������ēG�̃t�F�[�Y��
        TileObj clickTileObj = mapManager.GetClickTileObj();

        if(clickTileObj)
        {
            if (IsClickP2MotiKoma(clickTileObj))
            {
                mapManager.ResetMovablePanels(movableTiles);
                phase = Phase.Player2MotiKomaMoveSelection;
                return;
            }
            if (IsClickKoma2(clickTileObj))//p2�̋����Ȃ�
            {
                return;
            }
            else if (OnTileP1Check(clickTileObj))
            {
                if(movableTiles.Contains(clickTileObj)) //�N���b�N�����ꏊ�ɑ���̋���Ĉړ��͈͓��Ȃ�
                {
                    Koma enemyKoma = komaManager.GetP1Koma(clickTileObj.positionInt);
                    if (enemyKoma.name == "koma_0")//�擾�����G����Ȃ�
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
                    TurnText.GetComponent<Text>().text = "���̎��";
                    phase = Phase.Player1KomaSelection;
                }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;
            }
        else
        {
            //�N���b�N�����^�C�����ړ��͈͂Ɋ܂܂��Ȃ�
            if (movableTiles.Contains(clickTileObj))
            {
                //selectedKoma���^�C���܂ňړ�������B
                selectedKoma.Move(clickTileObj.positionInt);
                mapManager.PosCursor(1);
                    TurnText.GetComponent<Text>().text = "���̎��";
                    phase = Phase.Player1KomaSelection;
            }
                mapManager.ResetMovablePanels(movableTiles);
                selectedKoma = null;

            }
        }

    }

    private void Player1Win()//�����Ƀv���C���[�P�����������̏��������肢���܂��B
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
