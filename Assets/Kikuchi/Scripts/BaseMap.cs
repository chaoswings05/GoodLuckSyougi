using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string komaOu = "koma_0";
        string komaHisya = "koma_1";
        string komaKaku = "koma_2";

        float per1x = 0.928f;
        float basex = 11.793f - per1x; //���[�̒l - (��������W�͈͂̑S�̂̑傫��/��R�}�̈ړ�����)
        Transform komaOuTrans = transform.Find(komaOu).gameObject.transform;

        komaOuTrans.position = new Vector3(basex + per1x * 6, 7.379f, 2);//���[�̒l�ɉE�ɂ����̕��̒l�Ɉ�R�}�̈ړ������������đ����B
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
