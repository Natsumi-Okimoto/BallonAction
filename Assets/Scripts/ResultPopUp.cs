using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ResultPopUp : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private CanvasGroup CanvasGroupRestart;
    [SerializeField]
    private Button btnTitle;

    public void SetUpResultPopUp(int score)
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1.0f, 1.0f);
        txtScore.text = score.ToString();
        CanvasGroupRestart.DOFade(0, 1.0f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        btnTitle.onClick.AddListener(OnClickRestart);
    }

    private void OnClickRestart()
    {
        canvasGroup.DOFade(0, 1.0f).SetEase(Ease.Linear);
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.0f);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
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
