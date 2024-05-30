using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.WindowSystem
{
    public class RouterDontCloseAnyPrevious
    {
        protected readonly Dictionary<string, Window> _windows = new();

        public RouterDontCloseAnyPrevious(IEnumerable<Window> windowsList)
        {
            AddWindows(windowsList);
        }

        public void AddWindows(IEnumerable<Window> windowsList)
        {
            foreach (var window in windowsList)
            {
                AddWindow(window);
            }
        }
        
        public void AddWindow(Window window)
        {
            var windowIdentity = WindowIdentity(window);
            if (!_windows.TryAdd(windowIdentity, window))
            {
                Debug.LogError($"{windowIdentity} already registered", window);
            }
            window.gameObject.SetActive(false);
        }

        public static string WindowIdentity(Window window)
        {
            return window.GetType().Name;
        }

        public virtual T Show<T>(object infoToShow = null, Action callback = null) where T : Window
        {
            return (T)Show(typeof(T).Name, infoToShow, callback);
        }

        public virtual Window Show(string windowIdentity, object infoToShow = null, Action callback = null)
        {
            if (_windows.TryGetValue(windowIdentity, out Window foundWindow))
            {
                foundWindow.Show(infoToShow, callback);
                return foundWindow;
            }

            Debug.LogError($"{windowIdentity} not registered");
            return null;
        }

        public virtual T Hide<T>(Action callback = null) where T : Window
        {
            return (T)Hide(typeof(T).Name);
        }

        public virtual Window Hide(string windowIdentity, Action callback = null)
        {
            if (_windows.TryGetValue(windowIdentity, out Window foundWindow))
            {
                foundWindow.Hide(callback);
                return foundWindow;
            }
            else
            {
                Debug.LogError($"{windowIdentity} not registered");
                return null;
            }
        }

        public virtual T Get<T>() where T : Window
        {
            if (_windows.TryGetValue(typeof(T).Name, out Window foundWindow))
            {
                return (T)foundWindow;
            }
            return null;
        }
    }
}