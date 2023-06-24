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

            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//���̂܂܂��Ƃ��܂������Ȃ��̂�Camera.main.ScreenToWorldPoint�ō��W�`����ϊ�����B
            RaycastHit2D hit2D = Physics2D.Raycast(clickPosition, Vector2.down);
            if (hit2D && hit2D.collider)
            {
                cursor.SetPosition(hit2D.transform);
                TileObj tileobj = hit2D.collider.GetComponent<TileObj>();
                //�I���^�C���̍��W
                Debug.Log(tileobj.positionInt);
                //�q�b�g�����^�C����̋���擾����B
                Koma koma = komaManager.GetKoma(tileobj.positionInt);
                if (koma)
                {
                    Debug.Log("����");
                    //��̕ێ�
                    selectedKoma = koma;
                    ResetMovablePanels();
                    //�ړ��͈͂�\��
                    ShowMovablePanels(selectedKoma);

                }
                else
                {
                    Debug.Log("�N���b�N�����ꏊ�ɋ���Ȃ�");
                    //���ێ����Ă���Ȃ�A�N���b�N�����^�C���̏ꏊ�Ɉړ�������B
                    if (selectedKoma)
                    {
                        //selectedKoma���^�C���܂ňړ�������B
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
        //centerpos����㉺���E�̃^�C����T���B
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
