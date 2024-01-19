using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(PV.IsMine)
        {
            CreateController(PlayerPrefs.GetInt("MyChoice"), PlayerPrefs.GetInt("MyTeamChoice"));
            PlayerPrefs.Save();
        }
    }

    void CreateController(int playerNumber, int teamNumber)
    {
        String characterPre = "Maximov";
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPointTeam1();
            
        if (playerNumber == 0)
        {
            characterPre = "Maximov";
        }
        if (playerNumber == 1)
        {
            characterPre = "Medo";
        }
        if (playerNumber == 2)
        {
            characterPre = "KillBane";
        }
        if(playerNumber == 3)
        {
            characterPre = "SpaceJumper";
        }

        if (teamNumber == 2)
        {
            spawnPoint = SpawnManager.Instance.GetSpawnPointTeam2();
            
        }
        
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", characterPre), spawnPoint.position, Quaternion.identity);

        GameObject cinemachine = GameObject.FindGameObjectWithTag("CinemachineTarget");

        //Normal camera view
        GameObject follow = GameObject.FindGameObjectWithTag("PlayerFollowCamera");
        CinemachineVirtualCamera normalFollow;
        normalFollow = follow.GetComponent<CinemachineVirtualCamera>();
        normalFollow.Follow = cinemachine.transform;

        /*Aim down camera view
        GameObject aim = GameObject.FindGameObjectWithTag("PlayerAimCamera");
        CinemachineVirtualCamera aimFollow;
        aimFollow = aim.GetComponent<CinemachineVirtualCamera>();
        aimFollow.Follow = cinemachine.transform;

        //Switching between normal and aim down camera view 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ThirdPersonShooterController shooter = player.GetComponent<ThirdPersonShooterController>();
        shooter.aimVirtualCamera = aimFollow;*/
    }
}