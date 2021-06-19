using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;        // txtScore �Q�[���I�u�W�F�N�g�̎��� Text �R���|�[�l���g���C���X�y�N�^�[����A�T�C������

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
    [SerializeField]
    private Button btnTitle;

    [SerializeField]
    private Text lblStart;

    [SerializeField]
    private CanvasGroup canvasGroupTitle;

    private Tweener tweener;


    /// <summary>
    /// �X�R�A�\�����X�V
    /// </summary>
    /// <param name="score">�_���̏��</param>
    public void UpdateDisplayScore(int score)
    {
        txtScore.text = score.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    public void DisplayGameOverInfo()
    {
        // InfoBackGround �Q�[���I�u�W�F�N�g�̎��� CanvasGroup �R���|�[�l���g�� Alpha �̒l���A1�b������ 1 �ɕύX���āA�w�i�ƕ�������ʂɌ�����悤�ɂ���
        canvasGroupInfo.DOFade(1.0f, 1.0f);
        // ��������A�j���[�V���������ĕ\��
        txtInfo.DOText("GameOver...", 1.0f);

        btnInfo.onClick.AddListener(RestartGame);
    }

    /// <summary>
    /// ResultPopUp�̐���
    /// </summary>
    public void GenerateResultPopUp(int score)
    {
        // ResultPopUp �𐶐�
        ResultPopUp resultPopUp = Instantiate(resultPopUpPrefab, canvasTran, false);

        // ResultPopUp �̐ݒ���s��
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
        SwitchDisplayTitle(true, 1.0f);
        btnTitle.onClick.AddListener(OnClickTitle);
    }

    public void SwitchDisplayTitle(bool isSwitch,float alpha)
    {
        if (isSwitch) canvasGroupTitle.alpha = 0;
        canvasGroupTitle.DOFade(alpha, 1.0f).SetEase(Ease.Linear).OnComplete(() =>
        {
            lblStart.gameObject.SetActive(isSwitch);
        });

        if (tweener == null)
        {
            tweener = lblStart.gameObject.GetComponent<CanvasGroup>().DOFade(0, 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            tweener.Kill();
        }
    }

    private void OnClickTitle()
    {
        btnTitle.onClick.RemoveAllListeners();
        SwitchDisplayTitle(false, 0.0f);
        StartCoroutine(DisplayGameStartInfo());
    }

    public IEnumerator DisplayGameStartInfo()
    {
        yield return new WaitForSeconds(0.5f);
        canvasGroupInfo.alpha = 0;
        canvasGroupInfo.DOFade(1.0f, 0.5f);
        txtInfo.text = "Game Start!";

        yield return new WaitForSeconds(1.0f);
        canvasGroupInfo.DOFade(0f, 0.5f);

        canvasGroupTitle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
