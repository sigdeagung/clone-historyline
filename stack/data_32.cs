using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foo {
public class FurBehavior : MonoBehaviour
{
    public Material material;


    public Vector3 pos = new Vector3(0f, 0.98f, -9.54f);


    //simple camera for use in the game
    private new Camera camera;
    //texture containing fur data
    public Texture2D furTexture;
    //effect for fur shaders
    //Effect furEffect;
    //number of layers of fur
    public int nrOfLayers = 40;
    //total length of the hair
    public float maxHairLength = 2.0f;
    //density of hair
    public float density = 0.2f;

    //[Space(20)]
    //public Vector3 dirWorldVal = new Vector3(0, -10, 0);

    void Start()
    {
        this.transform.position = new Vector3(0f, 0.98f, -9.54f);
        this.transform.rotation = Quaternion.Euler(0, 180, 0);
        Initialize();
        GenerateGeometry();
    }

    public void Update()
    {
        Draw();

    }


    void Initialize()
    {

        //Initialize the camera
        camera = Camera.main;

        //create the texture
        furTexture = new Texture2D(256, 256, TextureFormat.ARGB32, false);
        furTexture.wrapModeU = TextureWrapMode.Repeat;
        furTexture.wrapModeV = TextureWrapMode.Repeat;
        //furTexture.filterMode = FilterMode.Point;

        //fill the texture
        FillFurTexture(furTexture, density);

        /*XNA's SurfaceFormat.Color is ARGB.
        //https://gamedev.stackexchange.com/a/6442/98839*/


        if (material.mainTexture != null)
        {
            material.mainTexture.wrapModeU = TextureWrapMode.Repeat;
            material.mainTexture.wrapModeV = TextureWrapMode.Repeat;
           // material.mainTexture.filterMode = FilterMode.Point;
        }
    }

    bool firstDraw = true;

    protected void Draw()
    {
        var pos = this.transform.position;

        camera.backgroundColor = CornflowerBlue();

        Matrix4x4 worldValue = Matrix4x4.Translate(pos);
        Matrix4x4 viewValue = camera.projectionMatrix;
        // viewValue = camera.worldToCameraMatrix;
        Matrix4x4 projectionValue = camera.projectionMatrix;

        material.SetMatrix("World", worldValue);
        material.SetMatrix("View", viewValue);
        material.SetMatrix("Projection", projectionValue); //Causes object to disappear

        material.SetFloat("MaxHairLength", maxHairLength);

        //if (firstDraw)
            material.SetTexture("_MainTex", furTexture);

        //furEffect.Begin();
        for (int i = 0; i < nrOfLayers; i++)
        {
            var propertyBlock = new MaterialPropertyBlock();

            var layer = (float)i / (float)nrOfLayers;
            propertyBlock.SetFloat("CurrentLayer", layer);
            propertyBlock.SetFloat("MaxHairLength", maxHairLength);
            propertyBlock.SetColor("_TintColor", new Color(layer, layer, layer, layer));
            DrawGeometry(propertyBlock);
        }

        if (firstDraw)
        {
            material.mainTexture.wrapModeU = TextureWrapMode.Repeat;
            material.mainTexture.wrapModeV = TextureWrapMode.Repeat;
            material.mainTexture.filterMode = FilterMode.Point;
        }

        if (firstDraw)
            firstDraw = false;
    }

    void DrawGeometry(MaterialPropertyBlock props)
    {
        var rot = Quaternion.Euler(0, 180, 0);
        Graphics.DrawMesh(verticesMesh, pos, rot, material, 0, camera, 0, props);
    }

    private VertexPositionNormalTexture[] verticesPText;
    public Mesh verticesMesh;

    private void GenerateGeometry()
    {
        var UnitZ = new Vector3(0, 0, 1);
        var verticesPText = new VertexPositionNormalTexture[6];
        verticesPText[5] = new VertexPositionNormalTexture(new Vector3(-10, 0, 0),
                                                    -UnitZ,
                                                    new Vector2(0, 0));
        verticesPText[4] = new VertexPositionNormalTexture(new Vector3(10, 20, 0),
                                                    -UnitZ,
                                                    new Vector2(1, 1));
        verticesPText[3] = new VertexPositionNormalTexture(new Vector3(-10, 20, 0),
                                                    -UnitZ,
                                                    new Vector2(0, 1));

        verticesPText[2] = verticesPText[5];
        verticesPText[1] = new VertexPositionNormalTexture(new Vector3(10, 0, 0),
                                                    -UnitZ,
                                                    new Vector2(1, 0));
        verticesPText[0] = verticesPText[4];

    }

    Mesh VertexPositionNormalTextureToUnityMesh(VertexPositionNormalTexture[] vpnt)
    {
        Vector3[] vertices = new Vector3[vpnt.Length];
        Vector3[] normals = new Vector3[vpnt.Length];
        Vector2[] uvs = new Vector2[vpnt.Length];

        int[] triangles = new int[vpnt.Length];

        //Copy variables to create a mesh
        for (int i = 0; i < vpnt.Length; i++)
        {
            vertices[i] = vpnt[i].Position;
            normals[i] = vpnt[i].Normal;
            uvs[i] = vpnt[i].TextureCoordinate;

            triangles[i] = i;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;

        mesh.MarkDynamic();


        mesh.triangles = triangles;
                    mesh.UploadMeshData(false);
        return mesh;
    }

    private void FillFurTexture(Texture2D furTexture, float density)
    {
        //read the width and height of the texture
        int width = furTexture.width;
        int height = furTexture.height;
        int totalPixels = width * height;

        //an array to hold our pixels
        Color32[] colors = new Color32[totalPixels];

        //random number generator
        System.Random rand = new System.Random();

        //initialize all pixels to transparent black
        for (int i = 0; i < totalPixels; i++)
            colors[i] = TransparentBlack();

        //compute the number of opaque pixels = nr of hair strands
        int nrStrands = (int)(density * totalPixels);

        //fill texture with opaque pixels
        for (int i = 0; i < nrStrands; i++)
        {
            int x, y;
            //random position on the texture
            x = rand.Next(height);
            y = rand.Next(width);
            //put color (which has an alpha value of 255, i.e. opaque)
           // colors[x * width + y] = new Color32((byte)255, (byte)x, (byte)y, (byte)255);
           colors[x * width + y] = Gold();
        }

        //set the pixels on the texture.
        furTexture.SetPixels32(colors);
        // actually apply all SetPixels, don't recalculate mip levels
        furTexture.Apply();
    }

    Color32 TransparentBlack()
    {
        //http://www.monogame.net/documentation/?page=P_Microsoft_Xna_Framework_Color_TransparentBlack
        Color32 color = new Color32(0, 0, 0, 0);
        return color;
    }

    Color32 Gold()
    {
        //http://www.monogame.net/documentation/?page=P_Microsoft_Xna_Framework_Color_Gold
        Color32 color = new Color32(255, 215, 0, 255);
        return color;
    }

    Color32 CornflowerBlue()
    {
        //http://www.monogame.net/documentation/?page=P_Microsoft_Xna_Framework_Color_CornflowerBlue
        Color32 color = new Color32(100, 149, 237, 255);
        return color;
    }

    public static Vector3 UnitZ()
    {
        return new Vector3(0f, 0f, 1f);
    }
}
}
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 
'UnityObjectToClipPos(*)'

Shader "Programmer/Fur Shader"
{
Properties
{
    _MainTex("Texture", 2D) = "white" {}
_TintColor("Tint Color", Color) = (1,1,1,1)
}
SubShader
{
    Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
    LOD 100
    //Blend SrcAlpha One
    //Blend DstAlpha OneMinusSrcAlpha
    Blend SrcAlpha OneMinusSrcAlpha
    ZWrite Off
    Cull Off

    Pass
    {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
                // make fog work
                //#pragma multi_compile_fog

        #include "UnityCG.cginc"

        //In
        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

    //Out
        struct v2f
        {
            float2 uv : TEXCOORD0;
            UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
        };

        struct VertexShaderInput
        {
            float3 Position : POSITION0;
            float3 Normal : NORMAL0;
            float2 TexCoord : TEXCOORD0;
        };

        struct VertexShaderOutput
        {
            float4 Position : POSITION0;
            float2 TexCoord : TEXCOORD0;
            float4 Tint: COLOR1;
        };

        sampler2D _MainTex;
        float4 _MainTex_ST;

        //Test variable/delete after
        float4 _TintColor;

        //The variables
        float4x4 World;
        float4x4 View;
        float4x4 Projection;

        float CurrentLayer; //value between 0 and 1
        float MaxHairLength; //maximum hair length

        VertexShaderOutput vert(VertexShaderInput input)
        {
            VertexShaderOutput output;
            float3 pos;
            pos = input.Position + input.Normal * MaxHairLength * CurrentLayer;

            //float4 worldPosition = mul(float4(pos, 1), World);
            //float4 viewPosition = mul(worldPosition, View);
            output.Position = UnityObjectToClipPos(pos);

            output.TexCoord = input.TexCoord;
            output.Tint = float4(CurrentLayer, CurrentLayer, 0, 1);
            return output;
        }

        float4 frag(VertexShaderOutput  i) : COLOR0
        {
            float4 t = tex2D(_MainTex,  i.TexCoord) * i.Tint;
            return t;//float4(t, i.x, i.y, 1);
        }
        ENDCG
    }
}