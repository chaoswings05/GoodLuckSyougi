using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    //TODO �N���b�N�����ꏊ�ɃJ�[�\�����ړ�������
    //�J�[�\�����ړ���������
    //�N���b�N�����ꏊ���擾������

    private void Start()
    {
        float per1xy = 0.928f;//1�}�X������̈ړ��l (��������W�͈͂̑S�̂̑傫��/��R�}�̈ړ�����)
        float basex = -3.708f - per1xy; //0�ɓ�����ꏊ�B����͍��[�̒l
        float basey = -3.7146f - per1xy; //0�ɓ�����ꏊ�B����͉��̒l
        this.transform.position = new Vector3(11,0, 100);
    }
    public void SetPosition(Transform target)
    {
        transform.position = target.position;
    }

}
