using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UI;
using UnityEngine;
using UnityEngine.Networking;
// ReSharper disable ConvertToUsingDeclaration

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private UserInfo _currentUserInfo;
    private const string PROFILES_URL_FORMAT = "https://sleepy-springs-46766-d3e2e40d6cdf.herokuapp.com/api/profile/Profiles?userId=%7B0%7D";
    public static ProfileManager Instance { get; private set; }

    public UserInfo CurrentUserInfo
    {
        get => _currentUserInfo;
        private set => _currentUserInfo = value;
    }

    [Serializable]
    public struct UserInfo
    {
        public string user_id;
        public int HighScore;
        public int Coins;
        public string TonWallet;
        public string Username;
    }
    
    [Serializable]
    public struct Response
    {
        public UserInfo body;
        public bool isSuccess;
        public int statusCode;
        public string[] errors;
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var username = URLParameters.GetSearchParameters().GetValueOrDefault("username", "username");
        var id = URLParameters.GetSearchParameters().GetValueOrDefault("id", "id");
        
        _currentUserInfo = new UserInfo() { user_id = id, Username = username };
        
        var loadingPopup = PopupRouter.Instance.Router.Show<LoadingPopup>();
        StartCoroutine(GetProfile(_currentUserInfo.user_id, (x)=> loadingPopup.Hide()));
    }

    public void UpdateHighScore(int newHighScore)
    {
        var currentUserInfoToUpdate = CurrentUserInfo;
        currentUserInfoToUpdate.HighScore = newHighScore;
        CurrentUserInfo = currentUserInfoToUpdate;
        StartCoroutine(AddOrUpdateProfile(currentUserInfoToUpdate));
    }

    public void UpdateWalletInfo(string newWallet)
    {
    }
    
    private IEnumerator GetProfile(string userId, Action<bool> callback = null) 
    {
        var url = string.Format(PROFILES_URL_FORMAT, userId);
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return request.SendWebRequest();


            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonConvert.DeserializeObject<Response>(request.downloadHandler.text);
                if (response.isSuccess)
                {
                    response.body.user_id = CurrentUserInfo.user_id;
                    if (!string.IsNullOrEmpty(response.body.Username))
                    {
                        CurrentUserInfo = response.body;
                        callback?.Invoke(true);
                    }
                }
                else
                {
                    var errors = response.errors is null ? "" : string.Join(" ", response.errors);
                    Debug.LogError($"Failed {nameof(GetProfile)}. Status: {response.statusCode}. Errors: {errors}");
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
    
    private IEnumerator AddOrUpdateProfile(UserInfo userInfo, Action<bool> callback = null) 
    {
        var url = string.Format(PROFILES_URL_FORMAT, userInfo.user_id);
        
        string jsonData = JsonUtility.ToJson(userInfo);
        using (UnityWebRequest request = UnityWebRequest.Post(url, jsonData, "application/json"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                callback?.Invoke(false);
                
                PopupRouter.Instance.Router.Show<ErrorPopup>();
            }
            else
            {
                var response = JsonConvert.DeserializeObject<Response>(request.downloadHandler.text);
                if (!response.isSuccess)
                {
                    var errors = response.errors is null ? "" : string.Join(" ", response.errors);
                    Debug.LogError($"Failed {nameof(GetProfile)}. Status: {response.statusCode}. Errors: {errors}");
                    callback?.Invoke(false);
                    
                    PopupRouter.Instance.Router.Show<ErrorPopup>();
                }
                else
                {
                    callback?.Invoke(true);
                }
            }
        }
    }
}
