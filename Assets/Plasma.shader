// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:5556,x:32923,y:32640,varname:node_5556,prsc:2|emission-8165-OUT,alpha-7333-OUT;n:type:ShaderForge.SFN_TexCoord,id:2825,x:31935,y:32954,varname:node_2825,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:7141,x:32619,y:33237,ptovrint:False,ptlb:node_7141,ptin:_node_7141,varname:node_7141,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e45260796e4f60a49b9cae1240e50476,ntxv:0,isnm:False|UVIN-4651-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6772,x:32163,y:33008,varname:node_6772,prsc:2|A-2825-UVOUT,B-4581-OUT;n:type:ShaderForge.SFN_Vector2,id:4581,x:31874,y:33177,varname:node_4581,prsc:2,v1:1,v2:0.25;n:type:ShaderForge.SFN_Multiply,id:8165,x:32709,y:32680,varname:node_8165,prsc:2|A-762-OUT,B-7141-RGB;n:type:ShaderForge.SFN_Vector1,id:762,x:32412,y:32553,varname:node_762,prsc:2,v1:5;n:type:ShaderForge.SFN_Multiply,id:7333,x:32653,y:32912,varname:node_7333,prsc:2|A-762-OUT,B-7141-R;n:type:ShaderForge.SFN_Vector2,id:6515,x:31974,y:33434,varname:node_6515,prsc:2,v1:1,v2:0.75;n:type:ShaderForge.SFN_Add,id:4634,x:32163,y:33356,varname:node_4634,prsc:2|A-6772-OUT,B-6515-OUT;n:type:ShaderForge.SFN_Panner,id:4651,x:32351,y:33381,varname:node_4651,prsc:2,spu:1,spv:0|UVIN-4634-OUT,DIST-6974-T;n:type:ShaderForge.SFN_Time,id:6974,x:32106,y:33550,varname:node_6974,prsc:2;proporder:7141;pass:END;sub:END;*/

Shader "Custom/Fireball" {
    Properties {
        _node_7141 ("node_7141", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_7141; uniform float4 _node_7141_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_762 = 5.0;
                float4 node_6974 = _Time + _TimeEditor;
                float2 node_4651 = (((i.uv0*float2(1,0.25))+float2(1,0.75))+node_6974.g*float2(1,0));
                float4 _node_7141_var = tex2D(_node_7141,TRANSFORM_TEX(node_4651, _node_7141));
                float3 emissive = (node_762*_node_7141_var.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(node_762*_node_7141_var.r));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
