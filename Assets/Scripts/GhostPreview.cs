using UnityEngine;

public class GhostPreview : MonoBehaviour
{
    private bool isValid = true;
    public SpriteRenderer spriteRenderer;

    public void SetPosition(Vector3 position, bool canBuild)
    {
        transform.position = position;

        if (isValid != canBuild)
        {
            isValid = canBuild;

            if (isValid)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            }
            else
            {
                spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);
            }
        }
    }
}