using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SC_Box : MonoBehaviour
{
    private Vector3 position = Vector3.zero;
    private int hitAmount = 10;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    TextMeshProUGUI tmpText = null;

    public Vector3 Position { get => position; set => position = value; }
    public int HitAmount { get => hitAmount; set => hitAmount = value; }


    private void Awake()
    {
        Canvas canvas = spriteRenderer.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        tmpText = spriteRenderer.GetComponent<TextMeshProUGUI>();

        UpdateText(hitAmount.ToString());

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ChangeHitAmount(-other.gameObject.GetComponent<SC_Ball>().HitAmount);
            UpdateText(hitAmount.ToString());
        }

    }
    private void ChangeHitAmount(int amount)
    {
        hitAmount += amount;
    }

    private void UpdateText(string newText)
    {
      tmpText.text = newText;
    }

}
