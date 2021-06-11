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
    private float limitPosX = 8.28f;           // 横方向の制限値
    private float limitPosY = 4.68f;          // 縦方向の制限値
    public GameObject[] ballons;
    public int maxBallonCount;                   // バルーンを生成する最大数
    public Transform[] ballonTrans;              // バルーンの生成位置の配列
    public GameObject ballonPrefab;              // バルーンのプレファブ
    public float generateTime;                   // バルーンを生成する時間
    public bool isGenerating;                    // バルーンを生成中かどうかを判定する。false なら生成していない状態。true は生成中の状態
    public bool isFirstGenerateBallon;          // 初めてバルーンを生成したかを判定するための変数(後程外部スクリプトでも利用するためpublicで宣言する)
    public float knockbackPower;                 // 敵と接触した際に吹き飛ばされる力
    public int coinPoint;                        // コインを獲得すると増えるポイントの総数
    public UIManager uiManager;



    [SerializeField, Header("Linecast用 地面判定レイヤー")]
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
        // 地面接地  Physics2D.Linecastメソッドを実行して、Ground Layerとキャラのコライダーとが接地している距離かどうかを確認し、接地しているなら true、接地していないなら false を戻す
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);

        // Sceneビューに Physics2D.LinecastメソッドのLineを表示する
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);

        // ballons変数の最初の要素の値が空ではないなら = バルーンが１つ生成されるとこの要素に値が代入される = バルーンが１つあるなら
        if (ballons[0]!=null)
        {
            if (Input.GetButtonDown(jump))
        {    // InputManager の Jump の項目に登録されているキー入力を判定する
            Jump();
        }
        if (isGrounded == false && rb.velocity.y < 0.15f)
        {
            // 落下アニメを繰り返す
            anim.SetTrigger("Fall");
        }
        }
        else
        {
            Debug.Log("バルーンがない。ジャンプ不可");
        }

        // Velocity.y の値が 5.0f を超える場合(ジャンプを連続で押した場合)
        if (rb.velocity.y > 5.0f)
        {

            // Velocity.y の値に制限をかける(落下せずに上空で待機できてしまう現象を防ぐため)
            rb.velocity = new Vector2(rb.velocity.x, 5.0f);
        }

        if (isGrounded == true && isGenerating == false)
        {

            // Qボタンを押したら
            if (Input.GetKeyDown(KeyCode.Q))
            {

                // バルーンを１つ作成する
                StartCoroutine(GenerateBallon());
            }
        }


    }

    /// <summary>
    /// ジャンプと空中浮遊
    /// </summary>
    private void Jump()
    {

        // キャラの位置を上方向へ移動させる(ジャンプ・浮遊)
        rb.AddForce(transform.up * jumpPower);

        // Jump(Up + Mid) アニメーションを再生する
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
        // 現在の位置情報が移動範囲の制限範囲を超えていないか確認する。超えていたら、制限範囲内に収める
        float posX = Mathf.Clamp(transform.position.x, -limitPosX, limitPosX);
        float posY = Mathf.Clamp(transform.position.y, -limitPosY, limitPosY);

        // 現在の位置を更新(制限範囲を超えた場合、ここで移動の範囲を制限する)
        transform.position = new Vector2(posX, posY);

    }
    /// <summary>
    /// バルーン生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateBallon()
    {

        // すべての配列の要素にバルーンが存在している場合には、バルーンを生成しない
        if (ballons[1] != null)
        {
            yield break;
        }

        // 生成中状態にする
        isGenerating = true;

        // isFirstGenerateBallon 変数の値が false、つまり、ゲームを開始してから、まだバルーンを１回も生成していないなら
        if (isFirstGenerateBallon == false)
        {

            // 初回バルーン生成を行ったと判断し、true に変更する = 次回以降はバルーンを生成しても、if 文の条件を満たさなくなり、この処理には入らない
            isFirstGenerateBallon = true;

            Debug.Log("初回のバルーン生成");

            // startChecker 変数に代入されている StartChecker スクリプトにアクセスして、SetInitialSpeed メソッドを実行する
            startChecker.SetInitialSpeed();
        }


        // １つめの配列の要素が空なら
        if (ballons[0] == null)
        {
            // 1つ目のバルーン生成を生成して、1番目の配列へ代入
            ballons[0] = Instantiate(ballonPrefab, ballonTrans[0]);
            ballons[0].GetComponent<Ballon>().SetUpBallon(this);

        }
        else
        {
            // 2つ目のバルーン生成を生成して、2番目の配列へ代入
            ballons[1] = Instantiate(ballonPrefab, ballonTrans[1]);
            ballons[1].GetComponent<Ballon>().SetUpBallon(this);
        }

        // 生成時間分待機
        yield return new WaitForSeconds(generateTime);

        // 生成中状態終了。再度生成できるようにする
        isGenerating = false;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {

        // 接触したコライダーを持つゲームオブジェクトのTagがEnemyなら 
        if (col.gameObject.tag == "Enemy")
        {

            // キャラと敵の位置から距離と方向を計算
            Vector3 direction = (transform.position - col.transform.position).normalized;

            // 敵の反対側にキャラを吹き飛ばす
            transform.position += direction * knockbackPower;
        }
    }

    /// <summary>
    /// バルーン破壊
    /// </summary>
    public void DestroyBallon()
    {

        // TODO 後程、バルーンが破壊される際に「割れた」ように見えるアニメ演出を追加する

        if (ballons[1] != null)
        {
            Destroy(ballons[1]);
        }
        else if (ballons[0] != null)
        {
            Destroy(ballons[0]);
        }
    }
    // IsTriggerがオンのコライダーを持つゲームオブジェクトを通過した場合に呼び出されるメソッド
    private void OnTriggerEnter2D(Collider2D col)
    {

        // 通過したコライダーを持つゲームオブジェクトの Tag が Coin の場合
        if (col.gameObject.tag == "Coin")
        {

            // 通過したコインのゲームオブジェクトの持つ Coin スクリプトを取得し、point 変数の値をキャラの持つ coinPoint 変数に加算
            coinPoint += col.gameObject.GetComponent<Coin>().point;

            uiManager.UpdateDisplayScore(coinPoint);

            // 通過したコインのゲームオブジェクトを破壊する
            Destroy(col.gameObject);
        }
    }
}
