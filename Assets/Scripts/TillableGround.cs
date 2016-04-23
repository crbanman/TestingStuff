using UnityEngine;
using System.Collections;

public class TillableGround : MonoBehaviour {

    bool obstructed = false;    // Is it covered in debris
    bool tilled = false;
    bool seeded = false;
    bool watered = false;

    public Sprite tillable, tilledDry, tilledWatered;
    private SpriteRenderer spriteRenderer;


    public void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnMouseUp() {
        if (tilled) {
            spriteRenderer.sprite = tilledWatered;
            watered = true;
        }
        else {
            spriteRenderer.sprite = tilledDry;
            tilled = true;
        }
    }
}
