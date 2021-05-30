using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float restartPosition;
    public float stopPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ��ʂ̍������ɂ��̃Q�[���I�u�W�F�N�g(�w�i)�̈ʒu���ړ�����
        transform.Translate(-scrollSpeed, 0, 0);

        // ���̃Q�[���I�u�W�F�N�g�̈ʒu��stopPosition�ɓ��B������
        if (transform.position.x < stopPosition)
        {

            // �Q�[���I�u�W�F�N�g�̈ʒu���ăX�^�[�g�n�_�ֈړ�����
            transform.position = new Vector2(restartPosition, 0);
        }
    }
}
