using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;        // txtScore ゲームオブジェクトの持つ Text コンポーネントをインスペクターからアサインする

    [SerializeField]
    private Text txtInfo;
    [SerializeField]
    private CanvasGroup canvasGroupInfo;
    [SerializeField]
    private ResultPopUp resultPopUpPrefab;

    [SerializeField]
    private Transform canvasTran;

    [SerializeField]
    private Button btnInfo;

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

        btnInfo.onClick.AddListener(RestartGame);
    }

    /// <summary>
    /// ResultPopUpの生成
    /// </summary>
    public void GenerateResultPopUp(int score)
    {
        // ResultPopUp を生成
        ResultPopUp resultPopUp = Instantiate(resultPopUpPrefab, canvasTran, false);

        // ResultPopUp の設定を行う
        resultPopUp.SetUpResultPopUp(score);
    }

    public void RestartGame()
    {
        btnInfo.onClick.RemoveAllListeners();
        string sceneName = SceneManager.GetActiveScene().name;
        canvasGroupInfo.DOFade(0f, 1.0f).OnComplete(() =>
        {
            Debug.Log("Restart");
            SceneManager.LoadScene(sceneName);
        });

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
