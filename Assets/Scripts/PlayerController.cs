using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string horizontal = "Horizontal";
    private Rigidbody2D rb;
    public float moveSpeed;
    private float scale;
    private Animator anim;
    private string jump = "Jump";
    public float jumpPower;
    public bool isGrounded;
    private float limitPosX = 8.28f;           // �������̐����l
    private float limitPosY = 4.68f;          // �c�����̐����l
    public GameObject[] ballons;
    public int maxBallonCount;                   // �o���[���𐶐�����ő吔
    public Transform[] ballonTrans;              // �o���[���̐����ʒu�̔z��
    public GameObject ballonPrefab;              // �o���[���̃v���t�@�u
    public float generateTime;                   // �o���[���𐶐����鎞��
    public bool isGenerating;                    // �o���[���𐶐������ǂ����𔻒肷��Bfalse �Ȃ琶�����Ă��Ȃ���ԁBtrue �͐������̏��
    public bool isFirstGenerateBallon;          // ���߂ăo���[���𐶐��������𔻒肷�邽�߂̕ϐ�(����O���X�N���v�g�ł����p���邽��public�Ő錾����)


    [SerializeField, Header("Linecast�p �n�ʔ��背�C���[")]
    private LayerMask groundLayer;
    [SerializeField]
    private StartChecker startChecker;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
        anim = GetComponent<Animator>();
        ballons = new GameObject[maxBallonCount];

    }


    void Update()
    {
        // �n�ʐڒn  Physics2D.Linecast���\�b�h�����s���āAGround Layer�ƃL�����̃R���C�_�[�Ƃ��ڒn���Ă��鋗�����ǂ������m�F���A�ڒn���Ă���Ȃ� true�A�ڒn���Ă��Ȃ��Ȃ� false ��߂�
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);

        // Scene�r���[�� Physics2D.Linecast���\�b�h��Line��\������
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);

        // ballons�ϐ��̍ŏ��̗v�f�̒l����ł͂Ȃ��Ȃ� = �o���[�����P���������Ƃ��̗v�f�ɒl���������� = �o���[�����P����Ȃ�
        if (ballons[0]!=null)
        {
            if (Input.GetButtonDown(jump))
        {    // InputManager �� Jump �̍��ڂɓo�^����Ă���L�[���͂𔻒肷��
            Jump();
        }
        if (isGrounded == false && rb.velocity.y < 0.15f)
        {
            // �����A�j�����J��Ԃ�
            anim.SetTrigger("Fall");
        }
        }
        else
        {
            Debug.Log("�o���[�����Ȃ��B�W�����v�s��");
        }

        // Velocity.y �̒l�� 5.0f �𒴂���ꍇ(�W�����v��A���ŉ������ꍇ)
        if (rb.velocity.y > 5.0f)
        {

            // Velocity.y �̒l�ɐ�����������(���������ɏ��őҋ@�ł��Ă��܂����ۂ�h������)
            rb.velocity = new Vector2(rb.velocity.x, 5.0f);
        }

        if (isGrounded == true && isGenerating == false)
        {

            // Q�{�^������������
            if (Input.GetKeyDown(KeyCode.Q))
            {

                // �o���[�����P�쐬����
                StartCoroutine(GenerateBallon());
            }
        }


    }

    /// <summary>
    /// �W�����v�Ƌ󒆕��V
    /// </summary>
    private void Jump()
    {

        // �L�����̈ʒu��������ֈړ�������(�W�����v�E���V)
        rb.AddForce(transform.up * jumpPower);

        // Jump(Up + Mid) �A�j���[�V�������Đ�����
        anim.SetTrigger("Jump");

      
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {

        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis(horizontal);

        if (x != 0)
        {
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
            Vector3 temp = transform.localScale;
            temp.x = x;
            if (temp.x > 0)
            {
                temp.x = scale;
            }
            else
            {
                temp.x = -scale;
            }
            transform.localScale = temp;

            anim.SetBool("Idle", false);
            anim.SetFloat("Run", 0.5f);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            anim.SetFloat("Run", 0.0f);
            anim.SetBool("Idle", true);
        }
        // ���݂̈ʒu��񂪈ړ��͈͂̐����͈͂𒴂��Ă��Ȃ����m�F����B�����Ă�����A�����͈͓��Ɏ��߂�
        float posX = Mathf.Clamp(transform.position.x, -limitPosX, limitPosX);
        float posY = Mathf.Clamp(transform.position.y, -limitPosY, limitPosY);

        // ���݂̈ʒu���X�V(�����͈͂𒴂����ꍇ�A�����ňړ��͈̔͂𐧌�����)
        transform.position = new Vector2(posX, posY);

    }
    /// <summary>
    /// �o���[������
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateBallon()
    {

        // ���ׂĂ̔z��̗v�f�Ƀo���[�������݂��Ă���ꍇ�ɂ́A�o���[���𐶐����Ȃ�
        if (ballons[1] != null)
        {
            yield break;
        }

        // ��������Ԃɂ���
        isGenerating = true;

        // isFirstGenerateBallon �ϐ��̒l�� false�A�܂�A�Q�[�����J�n���Ă���A�܂��o���[�����P����������Ă��Ȃ��Ȃ�
        if (isFirstGenerateBallon == false)
        {

            // ����o���[���������s�����Ɣ��f���Atrue �ɕύX���� = ����ȍ~�̓o���[���𐶐����Ă��Aif ���̏����𖞂����Ȃ��Ȃ�A���̏����ɂ͓���Ȃ�
            isFirstGenerateBallon = true;

            Debug.Log("����̃o���[������");

            // startChecker �ϐ��ɑ������Ă��� StartChecker �X�N���v�g�ɃA�N�Z�X���āASetInitialSpeed ���\�b�h�����s����
            startChecker.SetInitialSpeed();
        }


        // �P�߂̔z��̗v�f����Ȃ�
        if (ballons[0] == null)
        {
            // 1�ڂ̃o���[�������𐶐����āA1�Ԗڂ̔z��֑��
            ballons[0] = Instantiate(ballonPrefab, ballonTrans[0]);
        }
        else
        {
            // 2�ڂ̃o���[�������𐶐����āA2�Ԗڂ̔z��֑��
            ballons[1] = Instantiate(ballonPrefab, ballonTrans[1]);
        }

        // �������ԕ��ҋ@
        yield return new WaitForSeconds(generateTime);

        // ��������ԏI���B�ēx�����ł���悤�ɂ���
        isGenerating = false;
    }
}
