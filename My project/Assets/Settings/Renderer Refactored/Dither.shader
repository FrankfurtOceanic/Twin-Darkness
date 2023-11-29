Shader "Hidden/Dither"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
            #pragma vertex vert
            #pragma fragment frag

            //Dither Settings
            float _Spread =1;
            int _ColorCount =2;

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            static const int bayer_n = 4;
            float bayer_matrix_4x4[][bayer_n] = {
                {    -0.5,       0,  -0.375,   0.125 },
                {    0.25,   -0.25,   0.375, -0.125 },
                { -0.3125,  0.1875, -0.4375,  0.0625 },
                {  0.4375, -0.0625,  0.3125, -0.1875 },
            };

            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, i.uv);
                int x = i.uv.x * _MainTex_TexelSize.z;
                int y = i.uv.y * _MainTex_TexelSize.w;

                float bayer_value = bayer_matrix_4x4[sy % bayer_n][sx % bayer_n];

                float output_color = orig_color + (_Spread * bayer_value);


                output_color.r = floor((_ColorCount - 1.0f) * output_color.r + 0.5) / (_ColorCount - 1.0f);
                output_color.g = floor((_ColorCount - 1.0f) * output_color.g + 0.5) / (_ColorCount - 1.0f);
                output_color.b = floor((_ColorCount - 1.0f) * output_color.b + 0.5) / (_ColorCount - 1.0f);

                
                return output_color;
            }
            ENDCG
        }
    }
}
