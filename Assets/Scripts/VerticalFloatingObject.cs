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

    Tweener tweener;          // DOTween ‚Ìˆ—‚Ì‘ã“ü—p

    // Start is called before the first frame update
    void Start()
    {
        // DOTween ‚É‚æ‚é–½—ß‚ğÀs‚µA‚»‚ê‚ğ tweener •Ï”‚É‘ã“ü
        tweener = transform.DOMoveY(transform.position.y - moveRange, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnDestroy()
    {

        // DOTween ‚Ìˆ—‚ğ”jŠü‚·‚é(Loop ˆ—‚ğ‰ğÁ‚·‚é)
        tweener.Kill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
