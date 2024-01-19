using System;
using UnityEngine;
using Photon.Pun;
using Photon;

public class TeamManager : MonoBehaviour{

    PhotonView PV;
    public int teamID = 1;
    public int view;
    
    
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    
    private void Start()
    {
        if(PV.IsMine)
        {
            if (PlayerPrefs.GetInt("MyTeamChoice") != 0)
            {
                teamID = PlayerPrefs.GetInt("MyTeamChoice");
            }
            else
            {
                teamID = 1;
            }
            SetTeam(PV.ViewID, teamID);
        }
    }

    public void SetTeam(int viewID, int team)
    {
        view = viewID;
        PV.RPC("RPC_SetTeam", RpcTarget.All, viewID, team);
    }

    [PunRPC]
    void RPC_SetTeam(int viewID, int team)
    {

        if (PV.ViewID == viewID)
        {
            gameObject.GetComponent<TeamManager>().view = viewID;
            gameObject.GetComponent<TeamManager>().teamID = team;
        }
    }
}
