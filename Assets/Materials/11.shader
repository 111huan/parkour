// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/11"
{
    SubShader{
      Pass  {
          CGPROGRAM

          #pragma  vertex  vert
          #pragma  fragment  frag
          #include "UnityCG.cginc"

          fixed4 _Color;

          struct a2v {
              float4 vertex:POSITION;
              float3 normal:NORMAL;
              float4 texcoord:TEXCOORD0;
            
          };

          struct v2f {
              float pos : SV_POSITION;
              fixed color : COLOR0;
          };

          float4  vert(float4  v  :  POSITION) : SV_POSITION  {
                  return  UnityObjectToClipPos(v);
              }

          fixed4  frag() : SV_Target  {
                  return  fixed4(1.0,  1.0,  1.0,  1.0);
              }

          ENDCG
          }
        }
}
