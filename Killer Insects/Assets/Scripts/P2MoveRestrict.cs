using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Kyle Allen
 * 
 * Description: This code will stop the character form being able 
 * to push each other and will stop all movement when certain 
 * colliders hit each other
 */
public class P2MoveRestrict : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P1LeftBarrier"))
        {
            Player2Movement.walkRightP2 = false;
        }
        else if (other.gameObject.CompareTag("P1RightBarrier"))
        {
            Player2Movement.walkLeftP2 = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("P1LeftBarrier"))
        {
            Player2Movement.walkRightP2 = true;
        }
        else if (other.gameObject.CompareTag("P1RightBarrier"))
        {
            Player2Movement.walkLeftP2 = true;
        }
    }
}
