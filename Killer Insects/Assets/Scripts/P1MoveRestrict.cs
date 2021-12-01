using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Kyle Allen
 * 
 * Description: This code will stop the character form being able 
 * to push each other and will stop all movement when certain 
 * colliders hit each other
 */
public class P1MoveRestrict : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("P2LeftBarrier"))
        {
            Player1Movement.walkRightP1 = false;
        }
        else if (other.gameObject.CompareTag("P2RightBarrier"))
        {
            Player1Movement.walkLeftP1 = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("P2LeftBarrier"))
        {
            Player1Movement.walkRightP1 = true;
        }
        else if (other.gameObject.CompareTag("P2RightBarrier"))
        {
            Player1Movement.walkLeftP1 = true;
        }
    }
}
