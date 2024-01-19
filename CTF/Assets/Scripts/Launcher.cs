using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Random = UnityEngine.Random;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static Launcher Instance;
    private PhotonView PV;
    private int map = 1;
    public int nextPlayerTeam;
    


    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject mapGameButton;
    [SerializeField] GameObject teamGameButton;

    void Awake()
   {
       Instance = this;
   }

    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("Title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("Room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        mapGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        mapGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("Error");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(map);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Loading");
    }
    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void playerSelector()
    {
        PlayerPrefs.SetInt("MyChoice", 0);
    }
    public void playerSelector1()
    {
        PlayerPrefs.SetInt("MyChoice", 1);
    }
    public void playerSelector2()
    {
        PlayerPrefs.SetInt("MyChoice", 2);
    }
    public void playerSelector3()
    {
        PlayerPrefs.SetInt("MyChoice", 3);
    }
    
    public void playerMap1()
    {
        map = 1;
    }
    public void playerMap2()
    {
        map = 2;
    }
    
    public void teamSelector1()
    {
        PlayerPrefs.SetInt("MyTeamChoice", 1);
        Debug.Log("Team 1 Selected");
        
    }
    public void teamSelector2()
    {
        PlayerPrefs.SetInt("MyTeamChoice", 2);
        Debug.Log("Team 2 Selected");
    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        
    }
}
