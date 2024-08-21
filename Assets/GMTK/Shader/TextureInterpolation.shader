Shader "Custom/TextureInterpolation"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _SecondaryTex ("Secondary Texture", 2D) = "black" {}
        _Blend ("Blend Factor", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _SecondaryTex;
            float _Blend;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col1 = tex2D(_MainTex, i.texcoord);
                fixed4 col2 = tex2D(_SecondaryTex, i.texcoord);
                return lerp(col1, col2, _Blend);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
