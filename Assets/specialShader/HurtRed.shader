Shader "Hurt/Red" {
    Properties {
        _MainTex ("Diffuse Color", 2D) = "white" {}
        _RimPower ("Alpha Amount", Range(0.0,1)) = 0.1
        _AlphaMultiplier( "Alpha Multiplier" , Range( 0,100 )) = 12
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
    
        CGPROGRAM
        #pragma surface surf Lambert alpha
       
        struct Input {
            float2 uv_MainTex;
            float3 viewDir;
        };

        sampler2D _MainTex;
        float _RimPower;//_RimPower
        float _AlphaMultiplier;


       
        void surf (Input IN, inout SurfaceOutput o) {
       fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * (1,0,0,0.25);

            o.Albedo = c.rgb;
            half VN = saturate(dot (normalize(IN.viewDir), o.Normal));
            half rim = pow (1.0f - VN, (0.1*8));//_RimPower == 0.1
            o.Alpha = c.a - (rim * 12);//_AlphaMultiplier = 12
            
        }
        ENDCG
    }
 
    FallBack "Transparent/VertexLit"
}