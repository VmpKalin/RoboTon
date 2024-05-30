using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.WindowSystem
{
    public class MenuRouter : MonoBehaviour
    {
        [SerializeField] private List<Window> _windows;
        [SerializeField] private Window _firstWindowToShow;

        
        private RouterCloseAllPrevious _router;

        private void Awake()
        {
            _router = new RouterCloseAllPrevious(_windows);
            _router.Show(RouterDontCloseAnyPrevious.WindowIdentity(_firstWindowToShow));
        }
    }
}