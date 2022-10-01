Shader "UI/LDJAM51/UpgradeBar"
{
  Properties
  {
    _MainTex ("Texture", 2D) = "black" {}
    _BarValue ("Bar Value", Range(0,1)) = 0
    _OnColor ("On Color", Color) = (0,0,0,0)
    _OffColor ("Off Color", Color) = (0,0,0,0)
  }
  SubShader
  {
    Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
    LOD 100

    Pass
    {
      HLSLPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

      struct Attributes
      {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct Varyings
      {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
      };

      TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
      float4 _MainTex_ST;
      float _BarValue;
      float4 _OnColor;
      float4 _OffColor;

      Varyings vert (Attributes v)
      { 
        Varyings o; // could be "= (Varyings)0;" for other TEXCOORDX's 
        VertexPositionInputs vertexInputs = GetVertexPositionInputs(v.vertex.xyz);
        o.vertex = vertexInputs.positionCS;
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        return o;
      }

      float4 frag (Varyings i) : SV_Target
      {
        float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
        if(i.uv.x > _BarValue) {
          return 1;
        }

        return lerp(_OffColor, _OnColor, _BarValue);

      }
      ENDHLSL
    }
  }
}
