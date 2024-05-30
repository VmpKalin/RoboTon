using DG.Tweening;
using System;
using UnityEngine;

namespace Ui.WindowSystem
{
    public class Window : MonoBehaviour
    {
        public WindowAnimatron OnShow;
        public WindowAnimatron OnHide;
        private Sequence _currentSequence;

        public bool IsActive => gameObject.activeInHierarchy;
        
        public virtual void Show(object infoToShow = null, Action callback = null)
        {
            if (IsActive) return;
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence()
                .AppendCallback(() => OnSetInfoToShow(infoToShow))
                .AppendCallback(OnShowing)
                .AppendCallback(ActivateObject);
            if (OnShow) OnShow.AppendAnimation(_currentSequence);
            
            _currentSequence.OnComplete(() =>
                {
                    callback?.Invoke();
                    OnShown();
                })
                .Play();
        }

        public virtual void Hide(Action callback = null)
        {
            if (!IsActive) return;
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence()
                .AppendCallback(OnHiding);
            
            if (OnHide) OnHide.AppendAnimation(_currentSequence);
            _currentSequence.AppendCallback(DeactivateObject)
                .OnComplete(() =>
                {
                    callback?.Invoke();
                    OnHidden();
                })
                .Play();
        }

        private void OnDestroy()
        {
            if(IsActive) Hide();
        }

        protected virtual void DeactivateObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void ActivateObject()
        {
            gameObject.SetActive(true);
        }


        protected virtual void OnSetInfoToShow(object infoToShow)
        {
        }
        
        protected virtual void OnShowing() {}
        protected virtual void OnShown() {}
        protected virtual void OnHiding() {}
        protected virtual void OnHidden() {}
        
    }
}
