using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class LoadingScript : MonoBehaviour
{
    private const float DURATION = 2.0f;
    
    void Start()
    {
        //imageの取得
        Image[] circles = GetComponentsInChildren<Image>();
        var size = (int)Mathf.Sqrt(circles.Length);
        for (var i = 0; i < circles.Length; i++)
        {
            circles[i].rectTransform.anchoredPosition = new Vector2((i - circles.Length / 2) * 10f, 30);
            Sequence sequence = DOTween.Sequence()
                .SetLoops(-1, LoopType.Restart)
                .SetDelay((DURATION / 1) * ((float)i / circles.Length))
                .Append(circles[i].DOFade(0f, DURATION / 4))
                .Append(circles[i].DOFade(1f, DURATION / 4))
                .AppendInterval((DURATION / 2) * ((float)(1 - i) / circles.Length));
            sequence.Play();
        }
    }

}
