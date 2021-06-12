using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;        // txtScore ゲームオブジェクトの持つ Text コンポーネントをインスペクターからアサインする

    [SerializeField]
    private Text txtInfo;
    [SerializeField]
    private CanvasGroup canvasGroupInfo;

    /// <summary>
    /// スコア表示を更新
    /// </summary>
    /// <param name="score">点数の情報</param>
    public void UpdateDisplayScore(int score)
    {
        txtScore.text = score.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    public void DisplayGameOverInfo()
    {
        // InfoBackGround ゲームオブジェクトの持つ CanvasGroup コンポーネントの Alpha の値を、1秒かけて 1 に変更して、背景と文字が画面に見えるようにする
        canvasGroupInfo.DOFade(1.0f, 1.0f);
        // 文字列をアニメーションさせて表示
        txtInfo.DOText("GameOver...", 1.0f);
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
