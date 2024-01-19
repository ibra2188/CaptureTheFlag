using System;
using Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class Flag : MonoBehaviourPun
{
    PhotonView PV;
    public int flag;
    public GameObject _base;
    public GameObject player;
    [SerializeField] GameObject child1;
    [SerializeField] GameObject child2;
    public bool isPressed;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (PV.IsMine)
        {
            if (other.tag == "Player")
            {
                player = other.GetComponentInChildren<Transform>().parent.GetChild(1)
                    .GetComponentInChildren<FlagHolder>()
                    .flagHolder;
            }

            if (other.tag == "Player")
            {
                if (flag == 1)
                {
                    if (other.GetComponent<TeamManager>().teamID == 1)
                    {
                        if (transform.parent == null)
                        {
                            PV.RPC("ResetBlueFlagPosition", RpcTarget.All);
                        }
                    }

                    if (other.GetComponent<TeamManager>().teamID == 2  && isPressed)
                    {
                        other.GetComponent<ThirdPersonShooterController>().flag = this.gameObject;
                        if (isPressed)
                        {
                            int viewID = other.GetComponentInChildren<PhotonView>().ViewID;
                            PV.RPC("CaptureBlueFlagPosition", RpcTarget.All, viewID);
                        }
                    }
                }

                if (flag == 2)
                {
                    if (other.GetComponent<TeamManager>().teamID == 2)
                    {
                        if (transform.parent == null)
                        {
                            PV.RPC("ResetRedFlagPosition", RpcTarget.All);
                        }
                    }

                    if (other.GetComponent<TeamManager>().teamID == 1 && isPressed)
                    {
                        other.GetComponent<ThirdPersonShooterController>().flag = this.gameObject;
                        if (isPressed)
                        {
                            int viewID = other.GetComponentInChildren<PhotonView>().ViewID;
                            PV.RPC("CaptureRedFlagPosition", RpcTarget.All, viewID);
                        }
                    }
                }
            }
        }
    }

    [PunRPC]
    void ResetBlueFlagPosition()
    {
        GameObject stage1 = GameObject.FindGameObjectWithTag("BlueTeam");
        gameObject.transform.position = stage1.transform.position;
    }
    
    [PunRPC]
    void ResetRedFlagPosition()
    {
        GameObject stage2 = GameObject.FindGameObjectWithTag("Red Team");
        gameObject.transform.position = stage2.transform.position;
    }

    [PunRPC]
    void CaptureBlueFlagPosition(int viewID)
    {
        Debug.Log(viewID);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject other in players)
        {
            Debug.Log(other.GetComponent<PhotonView>().ViewID);
            if (other.GetComponent<PhotonView>().ViewID == viewID)
            {
                child1 = other.GetComponentInChildren<Transform>().parent.GetChild(1)
                    .GetComponentInChildren<FlagHolder>()
                    .flagHolder;
                gameObject.transform.SetParent(child1.transform);
                Destroy(_base);
            }
        }
    }

    [PunRPC]
    void CaptureRedFlagPosition(int viewID)
    {
        Debug.Log(viewID);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject other in players)
        {
            Debug.Log(other.GetComponent<PhotonView>().ViewID);
            if (other.GetComponent<PhotonView>().ViewID == viewID)
            {
                child2 = other.GetComponentInChildren<Transform>().parent.GetChild(1)
                    .GetComponentInChildren<FlagHolder>()
                    .flagHolder;
                gameObject.transform.SetParent(child2.transform);
                Destroy(_base);
            }
        }
    }
}
