using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.WindowSystem
{
    public class RouterCloseAllPrevious : RouterDontCloseAnyPrevious
    {
        public RouterCloseAllPrevious(IEnumerable<Window> windowsList) : base(windowsList)
        {
        }

        public override Window Show(string windowIdentity, object infoToShow = null, Action callback = null)
        {
            foreach (var (identity,window) in _windows)
            {
                if(identity == windowIdentity) continue;
                window.Hide();
            }
            return base.Show(windowIdentity, infoToShow, callback);
        }
    }
}