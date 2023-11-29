Shader "Hidden/Test 1"
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
            #pragma vertex vert
            #pragma fragment frag

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
                float4 screenSpace : TEXCOORD1;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CameraDepthTexture;
            float2 _MainTex_TexelSize;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenSpace = ComputeScreenPos(o.vertex);
                return o;
            }


            
            float Compare(float2 uv, float2 offset){
                //read neighbor pixel
                float neighborDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,
                        uv + _MainTex_TexelSize.xy * offset);
               
                return neighborDepth;
            }
            




            fixed4 frag (v2f i) : SV_Target
            {
                float2 screenSpaceUV = i.screenSpace.xy / i.screenSpace.w;


                   

                float neighbor = Compare(screenSpaceUV, float2(1, 0));
                neighbor += Compare(screenSpaceUV, float2(-1, 0));
                neighbor += Compare(screenSpaceUV, float2(0, 1));
                neighbor += Compare(screenSpaceUV, float2(0, -1));


                float depth = 4* SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenSpaceUV);
                float outline = abs(depth-neighbor)*1000 > 0.1 ? 1 : 0;
                 

                //return fixed4(1-outline, 1-outline, 1-outline, 1);




                float4 sourceColor = tex2D(_MainTex, screenSpaceUV);

                float light_info = 1-sourceColor.r;


                float4 color = lerp(sourceColor.b*sourceColor.r, float4(light_info, light_info , light_info,1), outline);

                return color;



            }
            ENDCG
        }
    }
}
