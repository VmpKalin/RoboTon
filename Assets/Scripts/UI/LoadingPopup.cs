using DG.Tweening;
using Ui.WindowSystem;
using UnityEngine;

namespace UI
{
    public class LoadingPopup : Window
    {
        [SerializeField] private Transform _throbber;
        
        private readonly Vector3 _rot = new(0, 0,360);
        
        private void Start()
        {
            _throbber.DOLocalRotate(_rot, 1).SetLoops(-1).SetEase(Ease.Linear).SetRelative();
        }
    }
}