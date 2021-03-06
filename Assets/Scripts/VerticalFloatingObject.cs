using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VerticalFloatingObject : MonoBehaviour
{
    [SerializeField]
    public float moveTime;
    [SerializeField]
    public float moveRange;

    Tweener tweener;          // DOTween の処理の代入用

    // Start is called before the first frame update
    void Start()
    {
        // DOTween による命令を実行し、それを tweener 変数に代入
        tweener = transform.DOMoveY(transform.position.y - moveRange, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnDestroy()
    {

        // DOTween の処理を破棄する(Loop 処理を解消する)
        tweener.Kill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
