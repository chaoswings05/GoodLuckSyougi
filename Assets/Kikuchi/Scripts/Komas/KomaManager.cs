using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class KomaManager : MonoBehaviour
{
    //�Տ�̋�ׂĂ��Ǘ�����B
    public List<Koma> komas = new List<Koma>();
    //�莝���̋���Ǘ�����B
    public List<Koma> Motikomas = new List<Koma>();

    public List<TileObj> tehudaTiles = new List<TileObj>();

    TileObj tehudaScript;

    float p1KomaPosX = -8.4f;
    float p1KomaPosY = 2;
    float p2KomaPosX = 8.4f;
    float p2KomaPosY = 2;

    public Vector2Int tehudaPos;
    void Start()
    {
        tehudaPos = new Vector2Int(10,0);
        // ��v����f�[�^�^�̂̎q�v�f�����ׂĎ擾����
        GetComponentsInChildren(komas);
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
                Motikomas.Add(koma);
                if (koma.tag == "P1Koma")
                {
                    IncreaceP2Koma(koma);
                }
                else if (koma.tag == "P2Koma")
                {
                    IncreaceP1Koma(koma);
                }
                else
                {
                    Debug.Log("��ł͂Ȃ����̂�DeleteKoma�ɑ����Ă��܂�");
                }

                break;
            }
        }
    }

    public void IncreaceP1Koma(Koma p2Koma)
    {
        
        p2Koma.tag = "P1Koma";//��̃^�O�ύX
        p2Koma.gameObject.transform.position = new Vector3(p1KomaPosX, p1KomaPosY, 2);
        p2Koma.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //��D�̃|�W�V�����Ɉړ�
        if (p1KomaPosX < -6)//���̋�̐����ʒu�����炷�B
        {
            p1KomaPosX += 1;
        }
        else
        {
            p1KomaPosX = -8.4f;
            p1KomaPosY -= 1;
        }
        MotiKomaSet(p2Koma);//��D��̏ꏊ�Ƀ^�C���𐶐����ċ���擾�ł���悤�ɂ��Ă���
        

        if(p2Koma.name == "koma_9")
        {
            p2Koma.name = "koma_1";
        }
        if (p2Koma.name == "koma_10")
        {
            p2Koma.name = "koma_2";
        }
        if (p2Koma.name.Contains("koma_11"))
        {
            p2Koma.name = "koma_3";
        }
        if (p2Koma.name.Contains("koma_12"))
        {
            p2Koma.name = "koma_4";
        }
        if (p2Koma.name.Contains("koma_13"))
        {
            p2Koma.name = "koma_5";
        }
        if (p2Koma.name.Contains("koma_14"))
        {
            p2Koma.name = "koma_6";
        }
        if (p2Koma.name.Contains("koma_15"))
        {
            p2Koma.gameObject.name = "koma_7";
            
        }
    }

    public void IncreaceP2Koma(Koma p1Koma)
    {
        p1Koma.tag = "P2Koma";
        p1Koma.gameObject.transform.position = new Vector3(p2KomaPosX, p2KomaPosY, 2);
        p1Koma.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        if (p2KomaPosX > 6)
        {
            p2KomaPosX -= 1;
        }
        else
        {
            p2KomaPosX = 8.4f;
            p2KomaPosY -= 1;
        }

        MotiKomaSet(p1Koma);//��D��̏ꏊ�Ƀ^�C���𐶐����ċ���擾�ł���悤�ɂ��Ă���

        if (p1Koma.name == "koma_1")
        {
            p1Koma.name = "koma_9";
        }
        if (p1Koma.name == "koma_2")
        {
            p1Koma.name = "koma_10";
        }
        if (p1Koma.name.Contains("koma_3"))
        {
            p1Koma.name = "koma_11";
        }
        if (p1Koma.name.Contains("koma_4"))
        {
            p1Koma.name = "koma_12";
        }
        if (p1Koma.name.Contains("koma_5"))
        {
            p1Koma.name = "koma_13";
        }
        if (p1Koma.name.Contains("koma_6"))
        {
            p1Koma.name = "koma_14";
        }
        if (p1Koma.name.Contains("koma_7"))
        {
            p1Koma.gameObject.name = "koma_15";

        }


    }

    public void MotiKomaSet(Koma setKoma)//��D�^�C���̐���
    {
        GameObject tehudaTile = (GameObject)Resources.Load("MapTrout");//���\�[�X����^�C���������Ă���B
        tehudaTile.name = "tehuda";
        tehudaTile.tag = "TehudaTile";
        tehudaTile.GetComponent<TileObj>().positionInt = tehudaPos;//�^�C�������}�X�̈ʒu�Ǘ��̒l��0,0(�����Ղ̊O�̒l)�ɂ���B
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
        tehudaTile = Instantiate(tehudaTile, setKoma.transform.position, Quaternion.identity);
        tehudaTiles.Add(tehudaTile.GetComponent<TileObj>());
        
        
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
