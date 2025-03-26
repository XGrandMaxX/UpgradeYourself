using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;

public class UIFader
{
    private static UIFader _instance;
    private UIFader() { }

    public static UIFader Instance => _instance ??= new UIFader();

    public UniTask FadeIn(Image image, float duration) => Fade(image, 1, duration);
    public UniTask FadeOut(Image image, float duration) => Fade(image, 0, duration);

    private UniTask Fade(Image image, float targetAlpha, float duration)
    {
        //if (DOTween.IsTweening(image))
        //    return UniTask.CompletedTask;

        image.DOKill();

        var tcs = new UniTaskCompletionSource();

        image.DOFade(targetAlpha, duration)
            .OnComplete(() => tcs.TrySetResult());

        return tcs.Task;
    }
}
