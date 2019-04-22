Shader "Unlit Transparent Vertex Colored" 
{
    Properties 
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    }

    Category 
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        //Alphatest Greater 0
        Blend SrcAlpha OneMinusSrcAlpha 
        Fog { Color(0,0,0,0) }
        Lighting Off
        Cull Off //we can turn backface culling off because we know nothing will be facing backwards

        BindChannels 
        {
            Bind "Vertex", vertex
            Bind "texcoord", texcoord 
            Bind "Color", color 
        }

        SubShader   
        {
            Pass 
            {
                SetTexture [_MainTex] 
                {
                    Combine texture * primary
                }
            }
        } 
    }
}

private void Awake()
{
    DontDestroyOnLoad(this);
    m_Instance = this;
    var shader = Shader.Find("Unlit Transparent Vertex Colored"); // <= it's some kind of best practise to put shaders in separate files, and load them like this
    m_Material = new Material(shader);
}

private void DrawQuad(Color aColor, float aAlpha)
{
    aColor.a = aAlpha;
    Texture2D texture = new Texture2D(1, 1);
    texture.SetPixel(0, 0, aColor);
    texture.Apply();
    GUI.skin.box.normal.background = texture;
    GUI.Box(new Rect(0, 0, Screen.width, Screen.height), GUIContent.none);
} 