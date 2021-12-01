using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Kyle Allen
 * 
 * Description: This code will help players with jumping collisions 
 * and stop them from landing on each others head.
 *
 */

public class P1JumpCollisonHelper : MonoBehaviour
{
    public GameObject player1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P2SpaceDetector"))
        {
            if (Player1Movement.isFacingRightP1)
            {
                player1.transform.Translate(0.8f, 0, 0);
            }
            else if (Player1Movement.isFacingLeftP1)
            {
                player1.transform.Translate(-0.8f, 0, 0);
            }
        }
    }

}
