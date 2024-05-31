using System.Collections.Generic;
using Ui.WindowSystem;
using UnityEngine;

namespace UI
{
    public class PopupRouter: MonoBehaviour
    {
        public static PopupRouter Instance { get; private set; }
        
        [SerializeField] private List<Window> _popups;

        
        public RouterDontCloseAnyPrevious Router { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Router = new(_popups);
        }
    }
}