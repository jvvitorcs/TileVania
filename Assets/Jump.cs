using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] Player player;

    

    private void Update()
    {
       
            player = FindObjectOfType<Player>();
        
    }

    public void playerJump()
    {

        player.Jump();

    }
}
