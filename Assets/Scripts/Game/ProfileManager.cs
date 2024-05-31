using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private UserInfo _currentUserInfo;
    private const string PROFILES_URL_FORMAT = "http://ec2-18-157-169-245.eu-central-1.compute.amazonaws.com:7569/profile/Profiles?userId={0}";
    public static ProfileManager Instance { get; private set; }

    public UserInfo CurrentUserInfo
    {
        get => _currentUserInfo;
        private set => _currentUserInfo = value;
    }

    public string Username { get; private set; }
    public string Id { get; private set; }
    
    [Serializable]
    public struct UserInfo
    {
        [JsonProperty(PropertyName = "user_id")]
        public string user_id;
        public int HighScore;
        public int Coins;
        public string TonWallet;
        public string Username;
        public bool ShowInLeaderBoard;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Debug.LogWarning("Start method was executed");

        Username = URLParameters.GetSearchParameters().GetValueOrDefault("username", "username");
        Id = URLParameters.GetSearchParameters().GetValueOrDefault("id", "id");
        Debug.LogWarning($"Username: {Username}");
        Debug.LogWarning($"Id: {Id}");

        AddTestData();
        StartCoroutine(GetProfile(Id));
    }

    private void AddTestData()
    {
        StartCoroutine(AddOrUpdateProfile(new UserInfo()
        {
            user_id = Id,
            Username = Username,
            Coins = 69,
            TonWallet = "unity wallet"
        }));
        /*
         * {"isSuccess":false,"statusCode":400,"errors":["User can update only his profile"]}
         */
    }

    private IEnumerator GetProfile(string userId) 
    {
        var url = string.Format(PROFILES_URL_FORMAT, userId);
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            Debug.LogWarning("GetProfile, before yield");

            yield return request.SendWebRequest();

            Debug.LogWarning("GetProfile, after yield");

            if (request.result == UnityWebRequest.Result.Success)
            {
                var json = request.downloadHandler.text;
                CurrentUserInfo = string.IsNullOrEmpty(json) ? new UserInfo() : JsonUtility.FromJson<UserInfo>(json);
            }
            else
            {
                Debug.LogError(request.error);
            }
        }
    }
    
    private IEnumerator AddOrUpdateProfile(UserInfo userInfo) 
    {
        var url = string.Format(PROFILES_URL_FORMAT, userInfo.user_id);
        
        string jsonData = JsonUtility.ToJson(userInfo);
        using (UnityWebRequest request = UnityWebRequest.Post(url, jsonData, "application/json"))
        {
            Debug.LogWarning("AddOrUpdateProfile, before yield");

            yield return request.SendWebRequest();

            Debug.LogWarning("AddOrUpdateProfile, after yield");
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
        }
    }
    
}
