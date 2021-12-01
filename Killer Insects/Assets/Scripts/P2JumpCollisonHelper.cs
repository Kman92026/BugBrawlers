using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Kyle Allen
 * 
 * Description: This code will help players with jumping collisions 
 * and stop them from landing on each others head.
 *
 */

public class P2JumpCollisonHelper : MonoBehaviour
{
    public GameObject player2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P1SpaceDetector"))
        {
            if (Player2Movement.isFacingRightP2)
            {
                player2.transform.Translate(0.8f, 0, 0);
            }
            else if (Player2Movement.isFacingLeftP2)
            {
                player2.transform.Translate(-0.8f, 0, 0);
            }
        }
    }

}
