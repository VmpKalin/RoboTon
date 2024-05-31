using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Ui.WindowSystem;
using UnityEngine;

namespace UI
{
    public class LoadingPopup : Window
    {
        [SerializeField] private Transform _throbber;
        
        private readonly Vector3 _rot = new Vector3(0, 0,360);
        
        private void Start()
        {
            _throbber.DOLocalRotate(_rot, 1).SetLoops(-1).SetEase(Ease.Linear).SetRelative();
        }
    }
}