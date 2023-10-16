Shader "Unlit/ScoreBar"
{
    Properties
    {
       [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
       _Score ("Score", Range(0,1)) = 1
       _BarColor1 ("BarColor1", Color) = (1,1,1,1)
       _BarColor2 ("BarColor2", Color) = (0,0,0,1)
       
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" } // Convert this to Opaque or Transparent
        // Blend SrcAlpha OneMinusSrcAlpha // Delete this line

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Score;
            float4 _BarColor1;
            float4 _BarColor2;

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float3 scoreBarColor1 = _BarColor1.rgb;
                float3 scoreBarColor2 = _BarColor2.rgb;

                float3 bgColor = float3(0, 0, 0);
                float scoreMask = _Score > i.uv.y;
                float3 scoreColor = lerp(scoreBarColor1, scoreBarColor2, i.uv.y);
                
                float3 outColor = lerp(bgColor, scoreColor, scoreMask);
                return float4(outColor, 0); // instead of scoreMask, make it 0
            }
            ENDCG
        }
    }
}
