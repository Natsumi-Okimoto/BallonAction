using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject aerialFloorPrefab;  // �v���t�@�u�ɂ��� AerialFloor_Mid �Q�[���I�u�W�F�N�g���C���X�y�N�^�[����A�T�C������
    [SerializeField]
    private Transform generateTran;  // �v���t�@�u�̃N���[���𐶐�����ʒu�̐ݒ�
    [Header("�����܂ł̑ҋ@����")]
    public float waitTime;  // �P�񐶐�����܂ł̑ҋ@���ԁB�ǂ̈ʂ̊Ԋu�Ŏ����������s�����ݒ�
    private float timer; // �ҋ@���Ԃ̌v���p
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            timer = 0;
            GenerateFloor();
        }
    }
    /// <summary>
    /// �v���t�@�u�����ɃN���[���̃Q�[���I�u�W�F�N�g�𐶐�
    /// </summary>
    public void GenerateFloor()
    {
        GameObject obj = Instantiate(aerialFloorPrefab, generateTran);
        float randomPosY = Random.Range(-4.0f, 4.0f);
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);
    }
}
