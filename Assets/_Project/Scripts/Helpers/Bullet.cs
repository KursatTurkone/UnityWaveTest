using UnityEngine;

public class Bullet : MonoBehaviour
{
   public void NotifyHit()
   {
       ObjectPoolManager.Despawn(gameObject);
   }
}
