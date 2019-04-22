// your textures to combine
// !! after importing as sprite change to advance mode and enable read and write property !!
public Sprite[] textures;
// just to see on editor nothing to add from editor
public Texture2D atlas;
public Material testMaterial;
public SpriteRenderer testSpriteRenderer;

int textureWidthCounter = 0;
int width,height;
void Start () {
    // determine your size from sprites
    width = 0;
    height = 0;
    foreach(Sprite t in textures)
    {
        width += t.texture.width;
        // determine the height
        if(t.texture.height > height)height = t.texture.height;
    }

    // make your new texture
    atlas = new Texture2D(width,height,TextureFormat.RGBA32,false);
    // loop through your textures
    for(int i= 0; i<textures.Length; i++)
    {
        int y = 0;
        while (y < atlas.height) {
            int x = 0;
            while (x < textures[i].texture.width ){
                if(y < textures[i].texture.height){
                    // fill your texture
                    atlas.SetPixel(x + textureWidthCounter, y, textures[i].texture.GetPixel(x,y));
                }
                else {
                    // add transparency
                    atlas.SetPixel(x + textureWidthCounter, y,new Color(0f,0f,0f,0f));
                }
                ++x;
            }
            ++y;
        }
        atlas.Apply();
        textureWidthCounter +=  textures[i].texture.width;
    }
    // for normal renderers
    if(testMaterial != null)testMaterial.mainTexture = atlas;
    // for sprite renderer just make  a sprite from texture
    Sprite s = Sprite.Create(atlas,new Rect(0f,0f,atlas.width,atlas.height),new Vector2(0.5f, 0.5f));
    testSpriteRenderer.sprite = s;
    // add your polygon collider
    testSpriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
}