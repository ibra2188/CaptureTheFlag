using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using  UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    public CinemachineVirtualCamera aimVirtualCamera;
    public float normalSensitivity;
    public float aimSensitivity;
    public LayerMask aimColliderLayerMask = new LayerMask();
    public Transform bulletProjectile;
    public Transform spawnBulletPosition;
    public bool shoot = false;
    public GameObject flag;

    private ThirdPersonController _thirdPersonController;
    private TeamManager _teamManager;
    private StarterAssetsInputs _starterAssetsInputs;

    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _teamManager = GetComponent<TeamManager>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_teamManager.teamID == 1)
        {
            flag = GameObject.FindGameObjectWithTag("RedFlag");
        }
        if (_teamManager.teamID == 2)
        {
            flag = GameObject.FindGameObjectWithTag("BlueFlag");
        }
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {
            mouseWorldPosition = raycastHit.point;
        }
        
        if (_starterAssetsInputs.aim)
        {
            shoot = true;
            aimVirtualCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 20;
            _thirdPersonController.SetSensitivity(aimSensitivity);
            _thirdPersonController.SetRotateOnMove(false);
            
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        
        else
        {
            shoot = false;
            aimVirtualCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 5;
            _thirdPersonController.SetSensitivity(normalSensitivity);
            _thirdPersonController.SetRotateOnMove(true);
            if (flag != null)
            {
                flag.GetComponent<Flag>().isPressed = false;
            }
        }

        if (_starterAssetsInputs.shoot)
        {
            if (shoot)
            {
                Vector3 aimDirection = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(bulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            }
        }

        if (_starterAssetsInputs.quit)
        {
            Application.Quit();
            PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        
        if (_starterAssetsInputs.pickup && flag!= null)
        {
            if (flag.transform != null)
            {
                flag.GetComponent<Flag>().isPressed = true;
            }
        }
    }
}
