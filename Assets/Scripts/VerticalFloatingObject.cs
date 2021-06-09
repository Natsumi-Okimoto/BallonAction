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

    Tweener tweener;          // DOTween �̏����̑���p

    // Start is called before the first frame update
    void Start()
    {
        // DOTween �ɂ�閽�߂����s���A����� tweener �ϐ��ɑ��
        tweener = transform.DOMoveY(transform.position.y - moveRange, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnDestroy()
    {

        // DOTween �̏�����j������(Loop ��������������)
        tweener.Kill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
