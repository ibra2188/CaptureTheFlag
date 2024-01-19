using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
   [SerializeField]  TMP_Text _text;

   public RoomInfo info;
   
   public void SetUp(RoomInfo _info)
   {
      info = _info;
      _text.text = _info.Name;
   }

   public void OnClick()
   {
      Launcher.Instance.JoinRoom(info);
   }
}
