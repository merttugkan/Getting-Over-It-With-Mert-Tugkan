using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MasterUI : MonoBehaviour
{
    public static MasterUI me;

    public GameObject Loading, JoinOrCreate, Lobby, Error;

    [Header("Prefab")]
    public GameObject userUIPrefab;

    [Header("LobbyParent")]
    public GameObject LobbyParent;

    // Start is called before the first frame update
    void Start()
    {
        me = this;
        nickname.text = PlayerPrefs.GetString("myname", "Alfred");
        SetNickname();
    }


    [Header("Nickname")]
    public TMP_InputField nickname;

    [Header("Game Id Input")]
    public TMP_InputField gameid;

    [Header("Game Name Button")]
    public TMP_Text gamename;

    public void SetNickname() 
    {
        if (string.IsNullOrEmpty(nickname.text) || string.IsNullOrWhiteSpace(nickname.text))
        {
            return;
        }
        PhotonNetwork.NickName = nickname.text;
        PlayerPrefs.SetString("myname", nickname.text);
    }

    public void CloseError() 
    {
        Error.SetActive(false);
        if (PhotonNetwork.IsConnected)
        {
            Loading.SetActive(false);
        }
        else
        {
            Loading.SetActive(true);
        }
    }

    string roomCode;

    public void Create() 
    {
        int a = Random.Range(100, 999);
        string GameName = a.ToString();
        MenuManager.me.CreateRoom(GameName);
        roomCode = GameName;
        gamename.text = roomCode;
    }

    public void Join()
    {
        if (string.IsNullOrEmpty(gameid.text) || string.IsNullOrWhiteSpace(gameid.text))
        {
            return;
        }
        MenuManager.me.JoinRoom(gameid.text);
        roomCode = gameid.text;
        gamename.text = roomCode;
    }
    public void Leave()
    {
        MenuManager.me.LeaveRoom();
        Lobby.SetActive(false);
        JoinOrCreate.SetActive(true);
    }

    public TMP_Text howManyPlayers;

    public void InsPlayerName() 
    {
        if (!Lobby.activeSelf)
        {
            return;
        }
        howManyPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    public void CopyGameID() 
    {
        GUIUtility.systemCopyBuffer = roomCode;
        //CopiedTheCode.SetActive(true);
        Invoke("CloseNotification", 3f);
    }

    public GameObject CopiedTheCode;

    public void StartGame() 
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void CloseNotification() 
    {
        CopiedTheCode.SetActive(false);
    }


    public TMP_Text playerCount;
    private void FixedUpdate()
    {
        InsPlayerName();

        playerCount.text = PhotonNetwork.CountOfPlayers.ToString();

    }
}
