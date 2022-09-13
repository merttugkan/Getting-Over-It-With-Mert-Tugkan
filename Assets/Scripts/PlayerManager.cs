using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    public PhotonView me;
    

    // Start is called before the first frame update
    void Start()
    {
        if (me.IsMine)
        {
            MenuManager.me.myPlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        }
    }

}
