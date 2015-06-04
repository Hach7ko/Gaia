Shader "WILEz/LiquidumMask" {
    Properties {
       _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
       _Color ("Main Color", Color) = (0,0,0,0)
       _Mask("Mask Texture (RGB)", 2D) = "white" {}
    }
    Subshader {
       Tags {
         "Queue"="Transparent"
         "IgnoreProjector"="true"
         "RenderType"="Transparent"
       }
 
      Lighting Off 
 
       CGPROGRAM
       #pragma surface surf Lambert alpha
 
         struct Input {
          float2 uv_MainTex;
          float2 uv_Mask;
         };
 
         half4 _Color;
         sampler2D _MainTex;
         sampler2D _Mask ;
 
           void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            half4 m = tex2D(_Mask, IN.uv_Mask);
            o.Emission = c.rgb;
            c.a-=m.a;
            o.Alpha = c.a  ;
        }
       ENDCG
    }
}