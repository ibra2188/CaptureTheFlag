using System;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public int health;
    public GameObject flagCheck;
    public Vector3 blueFlagPos;
    public Quaternion blueFlagRot;
    public Vector3 redFlagPos;
    public Quaternion redFlagRot;
    private void Start()
    {
        playerPosition = transform.position;
        playerRotation = transform.rotation;
        health = gameObject.GetComponent<Health>().maxHealth;
        flagCheck = gameObject.GetComponent<FlagHolder>().flagHolder;
        blueFlagPos = GameObject.FindGameObjectWithTag("BlueFlag").transform.position;
        blueFlagRot = GameObject.FindGameObjectWithTag("BlueFlag").transform.rotation;
        redFlagPos = GameObject.FindGameObjectWithTag("RedFlag").transform.position;
        redFlagRot = GameObject.FindGameObjectWithTag("RedFlag").transform.rotation;
    }

    public void roundEnded()
    {
        gameObject.transform.position = playerPosition;
        gameObject.transform.rotation = playerRotation;
        gameObject.GetComponent<Health>().addHealth(health);
        if (flagCheck.transform.childCount > 0)
        {
            flagCheck.transform.GetChild(0).transform.parent = null;
            if (gameObject.GetComponent<TeamManager>().teamID == 1)
            {
                GameObject.FindGameObjectWithTag("RedFlag").transform.position = redFlagPos;
                GameObject.FindGameObjectWithTag("RedFlag").transform.rotation = redFlagRot;
            }
            if (gameObject.GetComponent<TeamManager>().teamID == 2)
            {
                GameObject.FindGameObjectWithTag("BlueFlag").transform.position = blueFlagPos;
                GameObject.FindGameObjectWithTag("BlueFlag").transform.rotation = blueFlagRot;
            }
        }
    }
}
