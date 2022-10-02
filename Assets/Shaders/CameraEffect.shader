Shader "Deniz/Unlit/CameraEffect"
{
  Properties
  {
    _MainTex ("Texture", 2D) = "black" {}
    _CustomTexture ("Custom Texture", 2D) = "black"
    _NoiseTex ("Noise Texture", 2D) = "black" {}
    _NoiseSpeed("Light Flicker Speed", Float) = 10.0
    _LightFlicker("Noise Speed", Float) = 0.1
    [HDR]_NoiseColor("Noise Color", Color) = (1,1,1,1)
    _PowerInt("Power Int", Range(-1.0,1.0)) = 0.5
    _Idktf("Idktf", Float) = -1.0
    _Effect("Effect Value",Range(0,25)) = 0
  }
  SubShader
  {
    Tags{"RenderPipeline"="UniversalPipeline" "RenderType"="Opaque"}
    Pass
    {
      //ZWrite Off

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
      TEXTURE2D(_NoiseTex); SAMPLER(sampler_NoiseTex);
      float4 _NoiseTex_ST;
      TEXTURE2D(_CustomTexture); SAMPLER(sampler_CustomTexture);
      float4 _CustomTexture_ST;

      float _LightFlicker;
      float _NoiseSpeed;
      float4 _NoiseColor;
      float _PowerInt;
      float _Idktf;
      float _Effect;
      




      



      Varyings vert (Attributes v)
      { 
        Varyings o;
        VertexPositionInputs vertexInputs = GetVertexPositionInputs(v.vertex.xyz);
        o.vertex = vertexInputs.positionCS;
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        return o;
      }

      float4 frag (Varyings i) : SV_Target
      {
        float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
        float4 noise = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, i.uv + (_Time.y * _LightFlicker));
        float4 noise2 = SAMPLE_TEXTURE2D(_CustomTexture, sampler_NoiseTex, i.uv + (_Time.x * -_NoiseSpeed));
        float4 temp = noise * _NoiseColor + 2;
        noise = noise * _NoiseColor;
        noise2 = noise2 * _NoiseColor * _Idktf;
        noise = noise * temp;
        float4 bok = abs(noise / noise2);
        noise = pow(bok, _PowerInt);


        col = noise;

        return noise;  
      }
      ENDHLSL
    }
  }
}
