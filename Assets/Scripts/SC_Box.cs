using UnityEngine;

public class SC_Box : MonoBehaviour
{
    private Vector3 position = Vector3.zero;
    private int hitAmount = 0;

    private SpriteRenderer spriteRenderer;
    public Vector3 Position { get => position; set => position = value; }
    public int HitAmount { get => hitAmount; set => hitAmount = value; }


    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
    }
}
