using System;
using System.Collections.Generic;
using Ui.WindowSystem;
using UnityEngine;

namespace UI
{
    public class MenuRouter : MonoBehaviour
    {
        public static MenuRouter Instance { get; private set; }
        
        [SerializeField] private List<Window> _windows;
        [SerializeField] private Window _firstWindowToShow;

        
        public RouterCloseAllPrevious Router { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Router = new(_windows);
            Router.Show(RouterDontCloseAnyPrevious.WindowIdentity(_firstWindowToShow));
        }
    }
}