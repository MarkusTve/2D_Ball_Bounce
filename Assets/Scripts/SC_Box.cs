using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SC_Box : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI tmpText = null;

    private int hitAmount = 10;
    public int HitAmount { get => hitAmount; set => hitAmount = value; }

    private void Awake()
    {
        tmpText = spriteRenderer.GetComponent<TextMeshProUGUI>();

        Canvas canvas = spriteRenderer.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        UpdateText(hitAmount.ToString());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Ball"))
            return;

        hitAmount--;

        UpdateText(hitAmount.ToString());

        if (hitAmount <= 0)
            Destroy(this.gameObject);
    }

    private void UpdateText(string newText)
    {
        tmpText.text = newText;
    }

}
