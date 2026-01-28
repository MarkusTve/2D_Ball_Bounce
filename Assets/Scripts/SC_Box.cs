using Unity.VisualScripting;
using UnityEngine;

public class SC_Box : MonoBehaviour
{
    private Vector3 position = Vector3.zero;
    private int hitAmount = 10;

    private SpriteRenderer spriteRenderer;

    public Vector3 Position { get => position; set => position = value; }
    public int HitAmount { get => hitAmount; set => hitAmount = value; }


    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            print(hitAmount);
            ChangeHitAmount(-other.gameObject.GetComponent<SC_Ball>().HitAmount);
        }

    }
    private void ChangeHitAmount(int amount)
    {
        hitAmount += amount;
    }

}
