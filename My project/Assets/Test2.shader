Shader "Hidden/Test2"
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

            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            //the data that's used to generate fragments and can be read by the fragment shader
            struct v2f{
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            //the vertex shader
            v2f vert(appdata v){
                v2f o;
                //convert the vertex positions from object space to clip space so they can be rendered
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }


            sampler2D _CameraNormalsTexture;

            fixed4 frag(v2f i) : SV_TARGET{
                //get depth from depth texture
                float3 world_normal = tex2D(_CameraNormalsTexture, i.uv);


                //float3 view_normal = normalize(mul(world_normal, (float3x3)unity_ViewToWorld));

                float3 view_normal = normalize(mul((float3x3)UNITY_MATRIX_V, world_normal));
                //depth as distance from camera in units
                //depth = depth * _ProjectionParams.z;

                return fixed4(view_normal, 1);
}
            ENDCG
        }
    }
}
