using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviourPun
{
    private PhotonView PV;
    public int team;
    public int team1score;
    public int team2score;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        PlayerPrefs.SetInt("BlueTeam", 0);
        PlayerPrefs.SetInt("RedTeam", 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<TeamManager>().teamID == team)
            {
                if (other.GetComponentInChildren<Transform>().parent.GetChild(1).GetComponentInChildren<FlagHolder>()
                    .flagHolder.transform.childCount > 0)
                {
                    if (team == 1)
                    {
                        Debug.Log("Blue Team Won");
                        PV.RPC("RoundEnder", RpcTarget.All, 1);
                    }

                    if (team == 2)
                    {
                        Debug.Log("Red Team Won");
                        PV.RPC("RoundEnder", RpcTarget.All, 2);
                    }
                    
                }
            }
        }
    }

    public void Update()
    {
        team1score = PlayerPrefs.GetInt("BlueTeam");
        team2score = PlayerPrefs.GetInt("RedTeam");
        if ((team1score == 10) || (team2score == 10))
        {
            PV.RPC("MatchEnder", RpcTarget.All);
        }
    }

    [PunRPC]
    void RoundEnder(int winner)
    {
        if (winner == 1)
        {
            int temp = PlayerPrefs.GetInt("BlueTeam");
            PlayerPrefs.SetInt("BlueTeam", temp + 1);
        }
        
        if (winner == 2)
        {
            int temp = PlayerPrefs.GetInt("RedTeam");
            PlayerPrefs.SetInt("RedTeam", temp + 1);
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject other in players)
        {
            other.GetComponent<RoundManager>().roundEnded();
        }
    }

    [PunRPC]
    void MatchEnder()
    {
        SceneManager.LoadScene("Scoreboard");
    }
}
