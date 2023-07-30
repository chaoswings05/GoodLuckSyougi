using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public CursorController cursor;
    [SerializeField] BaseMap baseMap;
    [SerializeField] KomaManager komaManager;

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
        if(koma.name.Contains("koma_7")) //P1歩
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
        }

        if (koma.name.Contains("koma_15")) //P2歩
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
        }

        if (koma.name == ("koma_0")|| koma.name == ("koma_8")) //P1玉将P2玉将
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);

            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
        }

        if (koma.name == "koma_1" || koma.name == "koma_9") //P1飛車P2飛車
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, -i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, 0)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, 0)));
                    }
                    break;
                }
                else if (onTileKoma.tag == koma.tag)
                {
                    break;
                }
            }
        }

        if (koma.name == ("koma_2") || koma.name == ("koma_10")) //P1角P2角
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, -i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, -i)));
                    }
                    break;
                }
                else if (onTileKoma.tag == koma.tag) break;
                else
                {
                    Debug.Log("error");
                }
            }
        }

        if (koma.name.Contains("koma_3")) //P1金
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
        }

        if (koma.name.Contains("koma_11")) //P2金
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
        }

        if (koma.name.Contains("koma_4")) //P1銀
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            //����
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            //�E��
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }

            //����
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }

            //�E��
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
        }

        if (koma.name.Contains("koma_12")) //P2銀
        {
            //��
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            //����
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            //�E��
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }

            //����
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            //�E��
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
        }

        if (koma.name.Contains("koma_5")) //P1桂馬
        {
            onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(1, 2));
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, 2)));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, 2)));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(-1, 2));
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, 2)));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, 2)));
            }
        }

        if (koma.name.Contains("koma_13")) //P2桂馬
        {
            onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(1, -2));
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, -2)));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(1, -2)));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(-1, -2));
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, -2)));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-1, -2)));
            }
        }

        if (koma.name.Contains("koma_6")) //P1香車
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                    }
                    break;
                }
                else break;
            }
        }

        if (koma.name.Contains("koma_14")) //P2香車
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, -i)));
                    }
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        if (koma.name == "RK07" || koma.name == "RK06" || koma.name == "RK05" || koma.name == "RK04") //P1と金、成香、成桂、成銀
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
        }

        if (koma.name == "RK15" || koma.name == "RK14" || koma.name == "RK13" || koma.name == "RK12") //P2と金、成香、成桂、成銀
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
        }

        if (koma.name == "RK02" || koma.name == "RK10") //P1P2馬
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, -i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, -i)));
                    }
                    break;
                }
                else if (onTileKoma.tag == koma.tag) break;
                else
                {
                    Debug.Log("error");
                }
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
        }

        if (koma.name == "RK01" || koma.name == "RK09") //P1P2龍
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, -i)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, 0)));
                    }
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
                    if (onTileKoma.name != "Kukkyou")
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, 0)));
                    }
                    break;
                }
                else if (onTileKoma.tag == koma.tag)
                {
                    break;
                }
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.left));
            }

            //�E��
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up + Vector2Int.right));
            }

            //����
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.left));
            }

            //�E��
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
        }

        if (koma.name.Contains("Seiken")) //聖剣
        {
            Koma onTileKoma = null;
            onTileKoma = komaManager.GetKoma(new Vector2Int(koma.Position.x, 9));
            if (onTileKoma != null && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == new Vector2Int(koma.Position.x, 9)));
            }
            onTileKoma = komaManager.GetKoma(new Vector2Int(koma.Position.x, 1));
            if (onTileKoma != null && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == new Vector2Int(koma.Position.x, 1)));
            }
        }

        if (koma.name.Contains("Ninja")) //忍者
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
        }

        if (koma.name.Contains("Kukkyou")) //屈強
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
        }

        if (koma.name.Contains("Hikyo")) //卑怯
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
        }

        if (koma.name.Contains("Fugo")) //富豪
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
        }

        if (koma.name.Contains("Houdai")) //砲台
        {
            bool IsFirstKoma = false;
            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��B
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(0, i));
                if (onTileKoma == null)
                {
                    if (!IsFirstKoma)
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                    }
                }
                else if (onTileKoma != null)
                {
                    if (!IsFirstKoma)
                    {
                        IsFirstKoma = true;
                    }
                    else if (IsFirstKoma)
                    {
                        if (onTileKoma.tag != koma.tag)
                        {
                            if (onTileKoma.name != "Kukkyou")
                            {
                                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else break;
            }
            IsFirstKoma = false;
            for (int i = 1; i < 9; i++)
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(0, -i));
                if (onTileKoma == null)
                {
                    if (!IsFirstKoma)
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, -i)));
                    }
                }
                else if (onTileKoma != null)
                {
                    if (!IsFirstKoma)
                    {
                        IsFirstKoma = true;
                    }
                    else if (IsFirstKoma)
                    {
                        if (onTileKoma.tag != koma.tag)
                        {
                            if (onTileKoma.name != "Kukkyou")
                            {
                                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else break;
            }
            IsFirstKoma = false;
            for (int i = 1; i < 9; i++) //��̒l�ő�܂Ŏ��B
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(i, 0));
                if (onTileKoma == null)
                {
                    if (!IsFirstKoma)
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(i, 0)));
                    }
                }
                else if (onTileKoma != null)
                {
                    if (!IsFirstKoma)
                    {
                        IsFirstKoma = true;
                    }
                    else if (IsFirstKoma)
                    {
                        if (onTileKoma.tag != koma.tag)
                        {
                            if (onTileKoma.name != "Kukkyou")
                            {
                                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else break;
            }
            IsFirstKoma = false;
            for (int i = 1; i < 9; i++)
            {
                Koma onTileKoma = null;
                onTileKoma = komaManager.GetKoma(koma.Position + new Vector2Int(-i, 0));
                if (onTileKoma == null)
                {
                    if (!IsFirstKoma)
                    {
                        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(-i, 0)));
                    }
                }
                else if (onTileKoma != null)
                {
                    if (!IsFirstKoma)
                    {
                        IsFirstKoma = true;
                    }
                    else if (IsFirstKoma)
                    {
                        if (onTileKoma.tag != koma.tag)
                        {
                            if (onTileKoma.name != "Kukkyou")
                            {
                                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + new Vector2Int(0, i)));
                            }
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else break;
            }
        }

        if (koma.name.Contains("Kinniku")) //筋肉
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

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down + Vector2Int.right));
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
        }

        if (koma.name.Contains("Haiyu")) //俳優
        {
            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.up);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.up));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.down);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.down));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.right);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.right));
            }

            onTileKoma = komaManager.GetKoma(koma.Position + Vector2Int.left);
            if (onTileKoma == null)
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
            }
            else if (onTileKoma.tag != koma.tag && onTileKoma.name != "Kukkyou")
            {
                movableTiles.Add(tileObjs.Find(tile => tile.positionInt == koma.Position + Vector2Int.left));
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
