using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Ui.WindowSystem
{
    public class WindowAnimatron : MonoBehaviour
    {
        public virtual Tween AnimationSequence => DOTween.Sequence();
        public virtual void SetBeforeAnimationState() { }

        public Sequence AppendAnimation(Sequence sequence)
        {
            sequence.AppendCallback(SetBeforeAnimationState);
            sequence.Append(AnimationSequence);
            return sequence;
        }
    }
}
