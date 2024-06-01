using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ui.WindowSystem;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
// ReSharper disable ConvertToUsingDeclaration

namespace UI
{
    public class LeaderboardWindow: Window
    {
        [SerializeField] private LeaderboardItem[] _items;
        [SerializeField] private float _itemHeightPercent;
        private LeaderboardEntry[] _currentLeaderboardData;

        private const string LEADERBOARD_URL = "https://sleepy-springs-46766-d3e2e40d6cdf.herokuapp.com/api/profile/Profiles/Leaderboards";
        protected override void OnShown()
        {
            base.OnShowing();
            StartCoroutine(GetLeaderboardData(OnLeaderBoardDataReady));
            PopupRouter.Instance.Router.Show<LoadingPopup>();
        }

        private void OnLeaderBoardDataReady(bool success)
        {
            PopupRouter.Instance.Router.Hide<LoadingPopup>();
            if (!success)
            {
                Hide();
                return;
            }
            
            var rectTransform = transform as RectTransform;
            var itemVerticalSize = rectTransform.rect.height * _itemHeightPercent;
            
            for (int i = 0; i < _items.Length; i++)
            {
                var hasData = _currentLeaderboardData != null && i < _currentLeaderboardData.Length;
                _items[i].gameObject.SetActive(hasData);
                _items[i].transform.SetSiblingIndex(i);
                if(hasData) _items[i].Setup(_currentLeaderboardData[i].username, _currentLeaderboardData[i].score);
                var itemRectTransform = _items[i].transform as RectTransform;
                var delta = itemRectTransform.sizeDelta;
                delta.y = itemVerticalSize;
                itemRectTransform.sizeDelta = delta;
            }
        }

        public void ClosePopup()
        {
            Hide();
        }
        
        private IEnumerator GetLeaderboardData(Action<bool> callback = null) 
        {
            using (UnityWebRequest request = UnityWebRequest.Get(LEADERBOARD_URL))
            {
                request.SetRequestHeader("Access-Control-Allow-Origin", "*");
                yield return request.SendWebRequest();


                if (request.result == UnityWebRequest.Result.Success)
                {
                    var response = JsonConvert.DeserializeObject<Response>(request.downloadHandler.text);
                    if (response.isSuccess)
                    {
                        if (response.body != null)
                        {
                            _currentLeaderboardData  = response.body.OrderByDescending(x=>x.score).Take(10).ToArray();
                            callback?.Invoke(true);
                        }
                        else
                        {
                            callback?.Invoke(false);
                        }
                    }
                    else
                    {
                        var errors = response.errors is null ? "" : string.Join(" ", response.errors);
                        Debug.LogError($"Failed {nameof(GetLeaderboardData)}. Status: {response.statusCode}. Errors: {errors}");
                        callback?.Invoke(false);
                    
                        PopupRouter.Instance.Router.Show<ErrorPopup>();
                    }
                }
                else
                {
                    Debug.LogError(request.error);
                    callback?.Invoke(false);
                    PopupRouter.Instance.Router.Show<ErrorPopup>();
                }
            }
        }
        
        [Serializable]
        public struct LeaderboardEntry
        {
            public string username;
            public int score;
        }
        
        [Serializable]
        public struct Response
        {
            public LeaderboardEntry[] body;
            public bool isSuccess;
            public int statusCode;
            public string[] errors;
        }
    }
}