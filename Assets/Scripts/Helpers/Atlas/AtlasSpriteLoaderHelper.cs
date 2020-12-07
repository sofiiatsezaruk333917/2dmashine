using UnityEngine;
using UnityEngine.U2D;

public class AtlasStriteLoaderHelper : MonoBehaviour
{
    public SpriteAtlas spriteAtlas;

    public string spriteName;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spriteAtlas.GetSprite(spriteName);
    }
}
