using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] int hits = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Hit")
        {
            hits++;
        }
        
    }
}
