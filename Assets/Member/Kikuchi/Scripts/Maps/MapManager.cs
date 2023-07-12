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

    // ���������}�b�v���Ǘ�����B
    List<TileObj> tileObjs = new List<TileObj>();

    Koma onTileKoma = null;

    float per1xy = 0.928f;//1�}�X������̈ړ��l (��������W�͈͂̑S�̂̑傫��/��R�}�̈ړ�����)
    float basex = -3.708f; //0�ɓ�����ꏊ�B����͍��[�̒l
    float basey = -3.7146f;//0�ɓ�����ꏊ�B����͉��̒l
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
        /*if(player == 1)
        cursor.gameObject.transform.position = new Vector2(0.076f, -3.64f);
        else if(player == 2)
        cursor.gameObject.transform.position = new Vector2(0.076f, 3.78f);*/
    }

    public TileObj GetClickTileObj() //�N���b�N�����^�C�����擾����֐�
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���̂܂܂��Ƃ��܂������Ȃ��̂�Camera.main.ScreenToWorldPoint�ō��W�`����ϊ�����B
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

    public void ShowSetPanels(Koma koma ,List<TileObj> setTiles)
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

    public void ResetSetPanels(List<TileObj> setTiles) //�ړ��͈͂����Z�b�g����
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

    public void ShowMovablePanels(Koma koma , List<TileObj> movableTiles) //�ړ��͈͂�\������
    {
        //centerpos����㉺���E�̃^�C����T���B
        if(koma.name.Contains("koma_7")) //p1�̕����̓���
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

        if (koma.name.Contains("koma_15")) //p2�̕����̓���
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

        if (koma.name == ("koma_0")|| koma.name == ("koma_8")) //���̓���
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

        if (koma.name == "koma_1" || koma.name == "koma_9") //��Ԃ̓���
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
                else if (onTileKoma.tag == koma.tag)
                {
                    break;
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
                else if (onTileKoma.tag == koma.tag)
                {
                    break;
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
                else if (onTileKoma.tag == koma.tag)
                {
                    break;
                }
            }

            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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

        if (koma.name.Contains("koma_11")) //p2�̋��̓���
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

        if (koma.name.Contains("koma_4")) //p1�̋�̓���
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

        if (koma.name.Contains("koma_13")) //p2�̌j�n�̓���
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

        if (koma.name.Contains("koma_6")) //p1�̍��Ԃ̓���
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
                else break;
            }
        }

        if (koma.name.Contains("koma_14")) //p2�̍��Ԃ̓���
        {
            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��Bnull�Ώ���muvableTile�֐��ōs���B
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

        if (koma.name.Contains("Seiken")) //�K�`������̓���
        {
            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��B
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
                }
                else break;
            }

            for (int i = 1; i < 9; i++)
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
                }
                else break;
            }
        }

        if (koma.name.Contains("Ninja"))
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
        }

        if (koma.name.Contains("Kukkyou"))
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
        }

        if (koma.name.Contains("Houdai")) //�K�`������̓���
        {
            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��B
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
                }
                else break;
            }

            for (int i = 1; i < 9; i++)
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
                }
                else break;
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
