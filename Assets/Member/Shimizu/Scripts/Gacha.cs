using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    private float[] item2;

    private int[] item;
    //
    [Header("�e���A���e�B�̏d��")]
    [SerializeField]
    private int ssrWeight = 5;
    [SerializeField]
    private int srWeight = 20;
    [SerializeField]
    private int rWeight = 100;
    [Header("��̎�ނƊe���A���e�B�̌�")]
    [SerializeField]
    private int pieceQuantity = 7;
    [SerializeField]
    private int ssrQuantity = 1;
    [SerializeField]
    private int srQuantity = 2;
    [SerializeField]
    private int rQuantity = 4;
    private void Start()
    {
        //���`�F�b�N
        while(pieceQuantity != (ssrQuantity + srQuantity + rQuantity))
        {
            //�����̕�������������R�𑝂₷
            if (pieceQuantity > (ssrQuantity + srQuantity + rQuantity))
            {
                rQuantity++;
                Debug.Log("�����Ɗe���A���e�B���v�����Ⴄ����R�̌��𑝂₵����");
            }
            //���Z�̕�������������R�����炵�A����0�ȉ��ɂȂ�����G���[��f��
            else
            {
                rQuantity--;
                Debug.Log("�����Ɗe���A���e�B���v�����Ⴄ����R�̌������炵����");
                if (rQuantity < 0)
                {
#if UNITY_EDITOR
                    Debug.LogError("�K�`���̌��ݒ肨��������!!");
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
        }
        //�������̏d�݂�؂�グ�Ōv�Z
        var ssrPerOne = Mathf.CeilToInt(ssrWeight / ssrQuantity);
        var srPerOne = Mathf.CeilToInt(srWeight / srQuantity);
        var rPerOne = Mathf.CeilToInt(rWeight / rQuantity);
        //�z��̏�����
        item = new int[pieceQuantity];
        //�z��̒l(�d��)�̐ݒ�
        for(int i = 0; i < pieceQuantity; i++)
        {
            if (i < ssrQuantity)
            {
                item[i] = ssrPerOne;
            }
            else if( i< srQuantity + ssrQuantity)
            {
                item[i] = srPerOne;
            }
            else
            {
                item[i] = rPerOne;
            }
        }
        var result = Choose(item);
        //Debug.Log(result);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GachaMethod();
        }
    }
    //�K�`�����������\�b�h
    public string GachaMethod()
    {
        //���`�F�b�N
        while (pieceQuantity != (ssrQuantity + srQuantity + rQuantity))
        {
            //�����̕�������������R�𑝂₷
            if (pieceQuantity > (ssrQuantity + srQuantity + rQuantity))
            {
                rQuantity++;
                Debug.Log("�����Ɗe���A���e�B���v�����Ⴄ����R�̌��𑝂₵����");
            }
            //���Z�̕�������������R�����炵�A����0�ȉ��ɂȂ�����G���[��f��
            else
            {
                rQuantity--;
                Debug.Log("�����Ɗe���A���e�B���v�����Ⴄ����R�̌������炵����");
                if (rQuantity < 0)
                {
#if UNITY_EDITOR
                    Debug.LogError("�K�`���̌��ݒ肨��������!!");
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
        }
        //�������̏d�݂�؂�グ�Ōv�Z
        var ssrPerOne = Mathf.CeilToInt(ssrWeight / ssrQuantity);
        var srPerOne = Mathf.CeilToInt(srWeight / srQuantity);
        var rPerOne = Mathf.CeilToInt(rWeight / rQuantity);
        //�z��̏�����
        item = new int[pieceQuantity];
        //�z��̒l(�d��)�̐ݒ�
        for (int i = 0; i < pieceQuantity; i++)
        {
            if (i < ssrQuantity)
            {
                item[i] = ssrPerOne;
            }
            else if (i < srQuantity + ssrQuantity)
            {
                item[i] = srPerOne;
            }
            else
            {
                item[i] = rPerOne;
            }
        }
        int result = Choose(item);
        string resultRarity = null;
        //���ʂ��烌�A���e�B����
        if (result < ssrQuantity)
        {
            resultRarity = "SSR";
        }
        else if (result < ssrQuantity + srQuantity)
        {
            resultRarity = "SR";
        }
        else
        {
            resultRarity = "R";
        }

        return resultRarity;
    }

    //���I���\�b�h
    int Choose(int[] probs)
    {

        float total = 0;

        //�z��̗v�f�������ďd�݂̌v�Z
        foreach (float elem in probs)
        {
            total += elem;
        }

        //�d�݂̑�����0����1.0�̗����������Ē��I
        float randomPoint = Random.value * total;

        //i���z��̍ő�v�f���ɂȂ�܂ŌJ��Ԃ�
        for (int i = 0; i < probs.Length; i++)
        {
            //�����_���|�C���g���d�݂�菬�����Ȃ�
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                //�����_���|�C���g���d�݂��傫���Ȃ炻�̒l�������Ď��̗v�f��
                randomPoint -= probs[i];
            }
        }

        //�������P�̎��A�z�񐔂�-�P���v�f�̍Ō�̒l��Choose�z��ɖ߂��Ă���
        return probs.Length - 1;
    }
    public int PieceQuantity()
    {
        return pieceQuantity;
    }
}
