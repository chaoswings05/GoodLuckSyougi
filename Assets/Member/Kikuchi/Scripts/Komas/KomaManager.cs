using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class KomaManager : KomaName
{
    //盤上の駒すべてを管理する。
    public List<Koma> komas = new List<Koma>();
    //手持ちの駒を管理する。
    public List<Koma> Motikomas = new List<Koma>();
    public List<Koma> defeatedKomas = new List<Koma>();

    public List<TileObj> tehudaTiles = new List<TileObj>();

    float p1KomaPosX = 5.5f;
    float p1KomaPosY = -1.5f;
    float p2KomaPosX = -5.3f;
    float p2KomaPosY = 1.6f;

    public Vector2Int tehudaPos;

    void Start()
    {
        tehudaPos = new Vector2Int(10,0);
        // 一致するデータ型のの子要素をすべて取得する
        GetComponentsInChildren(komas);

        #region 駒生成
        //P1
        CreateKomaObj(p1komaOu, 5, 1);
        CreateKomaObj(p1komaKaku, 2, 2);
        CreateKomaObj(p1komaHisya, 8, 2);
        CreateKomaObj(p1komaKin, 4, 1);
        CreateKomaObj(p1komaKin2, 6, 1);
        CreateKomaObj(p1komaGin, 7, 1);
        CreateKomaObj(p1komaGin2, 3, 1);
        CreateKomaObj(p1komaKeima, 2, 1);
        CreateKomaObj(p1komaKeima2, 8, 1);
        CreateKomaObj(p1komaKyousya, 1, 1);
        CreateKomaObj(p1komaKyousya2, 9, 1);
        CreateKomaObj(p1komaHohyou1, 1, 3);
        CreateKomaObj(p1komaHohyou2, 2, 3);
        CreateKomaObj(p1komaHohyou3, 3, 3);
        CreateKomaObj(p1komaHohyou4, 4, 3);
        CreateKomaObj(p1komaHohyou5, 5, 3);
        CreateKomaObj(p1komaHohyou6, 6, 3);
        CreateKomaObj(p1komaHohyou7, 7, 3);
        CreateKomaObj(p1komaHohyou8, 8, 3);
        CreateKomaObj(p1komaHohyou9, 9, 3);
        //P2
        CreateKomaObj(p2komaOu, 5, 9);
        CreateKomaObj(p2komaKaku, 8, 8);
        CreateKomaObj(p2komaHisya, 2, 8);
        CreateKomaObj(p2komaKin, 4, 9);
        CreateKomaObj(p2komaKin2, 6, 9);
        CreateKomaObj(p2komaGin, 7, 9);
        CreateKomaObj(p2komaGin2, 3, 9);
        CreateKomaObj(p2komaKeima, 2, 9);
        CreateKomaObj(p2komaKeima2, 8, 9);
        CreateKomaObj(p2komaKyousya, 1, 9);
        CreateKomaObj(p2komaKyousya2, 9, 9);
        CreateKomaObj(p2komaHohyou1, 1, 7);
        CreateKomaObj(p2komaHohyou2, 2, 7);
        CreateKomaObj(p2komaHohyou3, 3, 7);
        CreateKomaObj(p2komaHohyou4, 4, 7);
        CreateKomaObj(p2komaHohyou5, 5, 7);
        CreateKomaObj(p2komaHohyou6, 6, 7);
        CreateKomaObj(p2komaHohyou7, 7, 7);
        CreateKomaObj(p2komaHohyou8, 8, 7);
        CreateKomaObj(p2komaHohyou9, 9, 7);
        #endregion
    }

    void CreateKomaObj(string name, int x, int y) //駒を初期配置に置く。
    {
        float basex = -3.708f - 0.928f; //0に当たる場所。今回は左端の値
        float basey = -3.7146f - 0.928f; //0に当たる場所。今回は下の値

        Koma obj = GameObject.Find(name).GetComponent<Koma>();
        obj.basex = basex;
        obj.basey = basey;
        Vector3 objPos = new Vector3(basex + 0.928f * x, basey + 0.928f * y, 2);
        float posx = obj.transform.position.x;
        float posy = obj.transform.position.y;
        float roundx = (posx - basex) / 0.928f;
        float roundy = (posy - basey) / 0.928f;
        Vector2Int pos = new Vector2Int((int)roundx, (int)roundy);//ポジション正規化
        obj.positionInt = pos;
    }

    public Koma GetKoma(Vector2Int pos)
    {
        foreach (var koma in komas)
        {
            if (koma.Position == pos)
            {
                return koma;
            }
        }
        return null;
    }

    public Koma GetP1Koma(Vector2Int pos)
    {
        foreach (var koma in komas)
        {
            if (koma != null && koma.Position == pos && koma.tag == "P1Koma")
            {
                return koma;
            }
        }
        return null;
    }

    public Koma GetP2Koma(Vector2Int pos)
    {
        foreach (var koma in komas)
        {
            if (koma != null && koma.Position == pos && koma.tag == "P2Koma")
            {
                return koma;
            }
        }
        return null;
    }

    public Koma GetP1MotiKoma(Vector2Int pos)
    {
        foreach (var koma in Motikomas)
        {
            if (koma != null && koma.Position == pos && koma.tag == "P1Koma")
            {
                return koma;
            }
        }
        return null;
    }

    public Koma GetP2MotiKoma(Vector2Int pos)
    {
        foreach (var koma in Motikomas)
        {
            if (koma != null && koma.Position == pos && koma.tag == "P2Koma")
            {
                return koma;
            }
        }
        return null;
    }

    public void DeleteKoma(string deleteKoma)
    {
        foreach (var koma in komas)
        {
            if (koma.name == deleteKoma)
            {
                komas.Remove(koma);
                defeatedKomas.Add(koma);
                koma.gameObject.SetActive(false);

                break;
            }
        }
    }

    public void IncreaceGachaKoma(Koma gachaKoma ,string playerTag, int komaNum, Sprite image)
    {
        gachaKoma.gameObject.SetActive(true);
        gachaKoma.gameObject.tag = playerTag; //駒のタグ変更
        gachaKoma.UpdateKomaData(komaNum, image);
        Motikomas.Add(gachaKoma);
        defeatedKomas.Remove(gachaKoma);

        if (playerTag == "P1Koma")
        {
            gachaKoma.transform.position = new Vector3(p1KomaPosX, p1KomaPosY, 2);
            gachaKoma.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (p1KomaPosX < 7.6)
            {
                p1KomaPosX += 0.9f;
            }
            else
            {
                p1KomaPosX = 5.5f;
                p1KomaPosY -= 1;
            }
        }
        if (playerTag == "P2Koma")
        {
            gachaKoma.transform.position = new Vector3(p2KomaPosX, p2KomaPosY, 2);
            gachaKoma.transform.rotation = Quaternion.Euler(0, 0, 180);
            if (p2KomaPosX > -8)
            {
                p2KomaPosX -= 0.9f;
            }
            else
            {
                p2KomaPosX = -5.3f;
                p2KomaPosY += 1;
            }
        }

        MotiKomaSet(gachaKoma); //手札駒の場所にタイルを生成して駒を取得できるようにしておく
    }

    public void RearrangeMotiKoma()
    {
        p1KomaPosX = 5.5f;
        p1KomaPosY = -1.5f;
        p2KomaPosX = -5.3f;
        p2KomaPosY = 1.6f;

        foreach (var tile in tehudaTiles)
        {
            tehudaTiles.Remove(tile);
            Destroy(tile.gameObject);
            break;
        }

        foreach (Koma koma in Motikomas)
        {
            if (koma.CompareTag("P1Koma"))
            {
                koma.transform.position = new Vector3(p1KomaPosX, p1KomaPosY, 2);
                koma.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (p1KomaPosX < 7.6)
                {
                    p1KomaPosX += 0.9f;
                }
                else
                {
                    p1KomaPosX = 5.5f;
                    p1KomaPosY -= 1;
                }
            }
            if (koma.CompareTag("P2Koma"))
            {
                koma.transform.position = new Vector3(p2KomaPosX, p2KomaPosY, 2);
                koma.transform.rotation = Quaternion.Euler(0, 0, 180);
                if (p2KomaPosX > -8)
                {
                    p2KomaPosX -= 0.9f;
                }
                else
                {
                    p2KomaPosX = -5.3f;
                    p2KomaPosY += 1;
                }
            }
            MotiKomaSet(koma);
        }
    }

    public void MotiKomaSet(Koma setKoma) //手札タイルの生成
    {
        GameObject tehudaTile = (GameObject)Resources.Load("MapTrout"); //リソースからタイルを持ってくる。
        tehudaTile = Instantiate(tehudaTile, setKoma.gameObject.transform.position, Quaternion.identity);
        tehudaTile.GetComponent<TileObj>().positionInt = tehudaPos; //タイルが持つマスの位置管理の値を0,0(将棋盤の外の値)にする。
        tehudaTile.name = "tehuda";
        tehudaTile.tag = "TehudaTile";
        tehudaTiles.Add(tehudaTile.GetComponent<TileObj>());
        setKoma.positionInt = tehudaPos;
        if (tehudaPos.y < 19)
        {
            tehudaPos.y++;
        }
        else
        {
            tehudaPos.x++;
            tehudaPos.y = 0;
        }
    }

    public void DeleteTehudaTile(Vector2Int tilePosition)
    {
        foreach (var tile in tehudaTiles)
        {
            if (tile.positionInt == tilePosition)
            {
                tehudaTiles.Remove(tile);
                Destroy(tile.gameObject);
                break;
            }
        }
    }
}