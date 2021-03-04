// Simplified Additive Particle shader. Differences from regular Additive Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

// 这是为了解决Material xxx doesn‘t have _Stencil property问题而制作的shader。
// 起作用的是//MASK SUPPORT ADD 和 //MASK SUPPORT END之间的部分
// 那个bug的原因貌似是因为对象在Scroll view内部，然后和mask什么的有关系
// 所以任何在scroll view内部的图标，只要加上这两部分，都能解决那个问题

Shader "Custom/Sp_addtive" {
Properties {
    _MainTex ("Particle Texture", 2D) = "white" {}

        //MASK SUPPORT ADD
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        //MASK SUPPORT END
}

Category {
    Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

        //MASK SUPPORT ADD
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp] 
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        ColorMask [_ColorMask]
        //MASK SUPPORT END

    Blend SrcAlpha One
    Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
    
    BindChannels {
        Bind "Color", color
        Bind "Vertex", vertex
        Bind "TexCoord", texcoord
    }
    
    SubShader {
        Pass {
            SetTexture [_MainTex] {
                combine texture * primary
            }
        }
    }
}
}