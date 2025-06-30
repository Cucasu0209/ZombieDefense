using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class TestCutSprites : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> Renderers;
    [SerializeField] private float HoldeRadius = 0.4f;
    private List<Texture2D> Textures;
    private List<Sprite> Sprites;
    private void Start()
    {
        Textures = new List<Texture2D>();
        Sprites = new List<Sprite>();
        for (int i = 0; i < Renderers.Count; i++)
        {
            Sprites.Add(Renderers[i].sprite);
            Texture2D editableTexture = Instantiate(Sprites[i].texture);
            editableTexture.Apply();
            Textures.Add(editableTexture);


            Rect rect = Renderers[i].sprite.rect;
            Vector2 pivot = Renderers[i].sprite.pivot / Renderers[i].sprite.rect.size;
            Renderers[i].sprite = Sprite.Create(editableTexture, rect, pivot, Renderers[i].sprite.pixelsPerUnit);
        }
    }
    private void Update()
    {
        for (int i = 0; i < Renderers.Count; i++)
        {
            CheckClickToRenderer(i);
        }
    }

    // Call this to "punch" a hole in alpha at a world position
    public void PunchHole(int index, Vector3 worldPos)
    {

        Vector2 localPos = Renderers[index].transform.InverseTransformPoint(worldPos);
        float ppu = Sprites[index].pixelsPerUnit;

        Rect rect = Sprites[index].rect;
        Vector2 pivot = Sprites[index].pivot;

        // Vị trí tâm trong rect của sprite con
        int centerX = Mathf.FloorToInt(localPos.x * ppu + pivot.x);
        int centerY = Mathf.FloorToInt(localPos.y * ppu + pivot.y);

        int radiusPx = Mathf.CeilToInt(HoldeRadius * ppu);

        for (int y = -radiusPx; y <= radiusPx; y++)
        {
            for (int x = -radiusPx; x <= radiusPx; x++)
            {
                float dist = Mathf.Sqrt(x * x + y * y);
                if (dist > radiusPx) continue;

                int px = centerX + x + (int)rect.x;
                int py = centerY + y + (int)rect.y;

                if (px < 0 || px >= Textures[index].width || py < 0 || py >= Textures[index].height)
                    continue;

                Color color = Textures[index].GetPixel(px, py);
                color.a = 0f;
                Textures[index].SetPixel(px, py, color);
            }
        }

        Textures[index].Apply();
    }


    private void CheckClickToRenderer(int index)
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Renderers[index].transform.position.z;

            if (!Renderers[index].bounds.Contains(worldPoint))
                return;

            Sprite sprite = Renderers[index].sprite;
            Texture2D tex = sprite.texture;

            Vector2 localPos = Renderers[index].transform.InverseTransformPoint(worldPoint);

            Rect rect = sprite.rect;
            Vector2 pivot = sprite.pivot;
            float ppu = sprite.pixelsPerUnit;

            Vector2 texCoord = new Vector2(
                localPos.x * ppu + pivot.x,
                localPos.y * ppu + pivot.y
            );

            int x = Mathf.FloorToInt(texCoord.x + rect.x);
            int y = Mathf.FloorToInt(texCoord.y + rect.y);

            if (x >= 0 && x < tex.width && y >= 0 && y < tex.height)
            {
                Color pixel = tex.GetPixel(x, y);

                PunchHole(index, worldPoint);

            }
        }
    }
}
