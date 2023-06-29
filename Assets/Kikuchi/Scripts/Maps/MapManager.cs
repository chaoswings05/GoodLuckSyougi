using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] CursorController cursor;
    [SerializeField] BaseMap baseMap;
    [SerializeField] KomaManager komaManager;

    [SerializeField] GManager gameManager;

    // ���������}�b�v���Ǘ�����B
    List<TileObj> tileObjs = new List<TileObj>();

    Koma onTileKoma = null;


    private void Start()
    {
        tileObjs = baseMap.CreateBaseMap();
    }

    public TileObj GetClickTileObj() //�N���b�N�����^�C�����擾����֐�
    {

        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//���̂܂܂��Ƃ��܂������Ȃ��̂�Camera.main.ScreenToWorldPoint�ō��W�`����ϊ�����B
        RaycastHit2D hit2D = Physics2D.Raycast(clickPosition, Vector2.down);
        if (hit2D && hit2D.collider)
        {
            cursor.SetPosition(hit2D.transform);
            return hit2D.collider.GetComponent<TileObj>();
        }

        return null;
    }

    public void ShowMovablePanels(Koma koma , List<TileObj> movableTiles) //�ړ��͈͂�\������
    {
        //centerpos����㉺���E�̃^�C����T���B

        if(koma.name.Contains("koma_7")) //p1�̕����̓���
        {
            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
        }

        if (koma.name.Contains("koma_15")) //p2�̕����̓���
        {
            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
        }

        if (koma.name == ("koma_0")|| koma.name == ("koma_8")) //���̓���
        {

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
        }

        if (koma.name == ("koma_1") || koma.name == ("koma_9")) //��Ԃ̓���
        {

            for(int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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
                else if(onTileKoma.tag == koma.tag) break;
                else
                {
                    Debug.Log("error");
                }
            }

            for (int i = 1; i < 9; i++) //���̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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
                else if (onTileKoma.tag == koma.tag) break;
                else
                {
                    Debug.Log("error");
                }
            }

            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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
                else if (onTileKoma.tag == koma.tag) break;
                else
                {
                    Debug.Log("error");
                }
            }

            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(-i, 0));
                if (onTileKoma == null)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, 0)));
                }
                else if (onTileKoma.tag != koma.tag)
                {
                    movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, 0)));
                    break;
                }
                else if (onTileKoma.tag == koma.tag) break;
                else
                {
                    Debug.Log("error");
                }
            }

        }

        if (koma.name == ("koma_2") || koma.name == ("koma_10")) //�p�̓���
        {

            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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

            for (int i = 1; i < 9; i++) //���̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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

            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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

            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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

        if (koma.name.Contains("koma_3")) //p1�̋��̓���
        {

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));

        }

        if (koma.name.Contains("koma_11")) //p2�̋��̓���
        {

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));

        }

        if (koma.name.Contains("koma_4")) //p1�̋�̓���
        {

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));

        }

        if (koma.name.Contains("koma_12")) //p2�̋�̓���
        {
            //��
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            //����
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            //�E��
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }

            //����
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            //�E��
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

        if (koma.name.Contains("koma_5")) //p1�̌j�n�̓���
        {

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, 2)));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, 2)));

        }

        if (koma.name.Contains("koma_13")) //p1�̌j�n�̓���
        {

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, -2)));

            movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, -2)));

        }

        if (koma.name.Contains("koma_6")) //p1�̌j�n�̓���
        {

            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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
                else if (onTileKoma.tag == koma.tag) break;

                else
                {
                    Debug.Log("error");
                }
            }

        }

        if (koma.name.Contains("koma_14")) //p1�̌j�n�̓���
        {

            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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
                else if (onTileKoma.tag == koma.tag) break;

                else
                {
                    Debug.Log("error");
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

    public void ResetMovablePanels(List<TileObj> movableTiles) //�ړ��͈͂����Z�b�g����
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
