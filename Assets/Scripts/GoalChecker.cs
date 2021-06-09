using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    public float moveSpeed = 0.01f;      // �ړ����x

    private float stopPos = 6.5f;        // ��~�n�_�B��ʂ̉E�[�ŃX�g�b�v������

    private bool isGoal;                 // �S�[���̏d������h�~�p�B��x�S�[�����肵���� true �ɂ��āA�S�[���̔���͂P�񂾂������s��Ȃ��悤�ɂ���
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > stopPos)
        {
            transform.position += new Vector3(-moveSpeed, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        // �ڐG����(�S�[������)�ۂɂP�񂾂����肷��
        if (col.gameObject.tag == "Player" && isGoal == false)
        {

            // �Q��ڈȍ~�̓S�[��������s��Ȃ��悤�ɂ��邽�߂ɁAtrue �ɕύX����
            isGoal = true;

            Debug.Log("�Q�[���N���A");
        }
    }
}