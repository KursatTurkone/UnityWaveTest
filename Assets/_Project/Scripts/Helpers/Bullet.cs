using UnityEngine;

public class Bullet : MonoBehaviour
{
   public void NotifyHit()
   {
       Destroy(gameObject);
   }
}
