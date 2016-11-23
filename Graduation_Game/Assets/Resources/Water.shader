// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:1,fgcg:1,fgcb:1,fgca:1,fgde:0.02,fgrn:7.2,fgrf:134.03,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33161,y:32673,varname:node_4013,prsc:2|diff-5951-RGB,alpha-557-OUT;n:type:ShaderForge.SFN_Tex2d,id:5951,x:32481,y:32674,ptovrint:False,ptlb:Diffuse Texture,ptin:_DiffuseTexture,varname:node_5951,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8e51253aca2fd95488c3c0e6e1d13311,ntxv:0,isnm:False|UVIN-2476-OUT;n:type:ShaderForge.SFN_TexCoord,id:3967,x:30978,y:32754,varname:node_3967,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:2476,x:32307,y:32724,varname:node_2476,prsc:2|A-7912-OUT,B-7276-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3361,x:31747,y:32710,ptovrint:False,ptlb:Speed U,ptin:_SpeedU,varname:node_3361,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:7643,x:31747,y:32982,ptovrint:False,ptlb:speed V,ptin:_speedV,varname:_node_3361_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Time,id:5158,x:31747,y:32530,varname:node_5158,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3706,x:31971,y:32676,varname:node_3706,prsc:2|A-5158-TSL,B-3361-OUT,C-6999-OUT;n:type:ShaderForge.SFN_Add,id:7912,x:32148,y:32676,varname:node_7912,prsc:2|A-3706-OUT,B-3690-OUT;n:type:ShaderForge.SFN_Add,id:7276,x:32148,y:32804,varname:node_7276,prsc:2|A-4707-OUT,B-1256-OUT;n:type:ShaderForge.SFN_Multiply,id:4707,x:31971,y:32804,varname:node_4707,prsc:2|A-5158-TSL,B-7643-OUT,C-6999-OUT;n:type:ShaderForge.SFN_Tex2d,id:4184,x:30586,y:32911,ptovrint:False,ptlb:Red Green Noise Texture,ptin:_RedGreenNoiseTexture,varname:node_4184,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7f2a19055422a4c47a40405ab29a11bd,ntxv:0,isnm:False|UVIN-1943-OUT;n:type:ShaderForge.SFN_Subtract,id:6332,x:30795,y:32911,varname:node_6332,prsc:2|A-4184-R,B-3783-OUT;n:type:ShaderForge.SFN_Subtract,id:1548,x:30795,y:33076,varname:node_1548,prsc:2|A-4184-G,B-3783-OUT;n:type:ShaderForge.SFN_Vector1,id:3783,x:30586,y:33124,varname:node_3783,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:3749,x:30978,y:32911,varname:node_3749,prsc:2|A-6332-OUT,B-8234-OUT,C-393-OUT;n:type:ShaderForge.SFN_Multiply,id:5121,x:30978,y:33076,varname:node_5121,prsc:2|A-1548-OUT,B-8234-OUT,C-393-OUT;n:type:ShaderForge.SFN_Add,id:3690,x:31162,y:32911,varname:node_3690,prsc:2|A-3749-OUT,B-3967-U;n:type:ShaderForge.SFN_Add,id:1256,x:31162,y:33076,varname:node_1256,prsc:2|A-5121-OUT,B-3967-V;n:type:ShaderForge.SFN_Slider,id:8234,x:30429,y:33287,ptovrint:False,ptlb:Distortion Strenght,ptin:_DistortionStrenght,varname:node_8234,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.509461,max:5;n:type:ShaderForge.SFN_Multiply,id:2216,x:27683,y:33086,varname:node_2216,prsc:2;n:type:ShaderForge.SFN_Time,id:2396,x:29803,y:32620,varname:node_2396,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:9065,x:29803,y:32800,ptovrint:False,ptlb:Noise Speed U,ptin:_NoiseSpeedU,varname:_SpeedU_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:9199,x:29803,y:32912,ptovrint:False,ptlb:Noise speed V,ptin:_NoisespeedV,varname:_speedV_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:6056,x:30027,y:32894,varname:node_6056,prsc:2|A-2396-TSL,B-9199-OUT,C-9385-OUT;n:type:ShaderForge.SFN_Multiply,id:5593,x:30027,y:32766,varname:node_5593,prsc:2|A-2396-TSL,B-9065-OUT,C-9385-OUT;n:type:ShaderForge.SFN_Append,id:1943,x:30386,y:32810,varname:node_1943,prsc:2|A-8334-OUT,B-3875-OUT;n:type:ShaderForge.SFN_Add,id:8334,x:30219,y:32766,varname:node_8334,prsc:2|A-5593-OUT,B-2882-U;n:type:ShaderForge.SFN_Multiply,id:1225,x:27963,y:33329,varname:node_1225,prsc:2;n:type:ShaderForge.SFN_Add,id:3875,x:30219,y:32894,varname:node_3875,prsc:2|A-6056-OUT,B-2882-V;n:type:ShaderForge.SFN_TexCoord,id:2882,x:30027,y:33041,varname:node_2882,prsc:2,uv:0;n:type:ShaderForge.SFN_Vector1,id:9385,x:29803,y:33002,varname:node_9385,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Vector1,id:393,x:30429,y:33206,varname:node_393,prsc:2,v1:0.0005;n:type:ShaderForge.SFN_Vector1,id:6999,x:31747,y:32804,varname:node_6999,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Slider,id:9875,x:32442,y:32917,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_9875,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:1998,x:32773,y:32874,varname:node_1998,prsc:2|A-5951-A,B-9875-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:1738,x:32110,y:33197,varname:node_1738,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7065,x:32656,y:33063,varname:node_7065,prsc:2|A-2993-OUT,B-8141-OUT;n:type:ShaderForge.SFN_Add,id:557,x:32977,y:32874,varname:node_557,prsc:2|A-1998-OUT,B-9167-OUT;n:type:ShaderForge.SFN_ViewPosition,id:5763,x:32110,y:33063,varname:node_5763,prsc:2;n:type:ShaderForge.SFN_Distance,id:9661,x:32312,y:33063,varname:node_9661,prsc:2|A-5763-XYZ,B-1738-XYZ;n:type:ShaderForge.SFN_Subtract,id:2993,x:32479,y:33063,varname:node_2993,prsc:2|A-9661-OUT,B-536-OUT;n:type:ShaderForge.SFN_ValueProperty,id:536,x:32312,y:33242,ptovrint:False,ptlb:Start,ptin:_Start,varname:node_536,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_ValueProperty,id:9585,x:32364,y:33349,ptovrint:False,ptlb:Opacity Gradient Range,ptin:_OpacityGradientRange,varname:node_9585,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:20;n:type:ShaderForge.SFN_Clamp01,id:9167,x:32812,y:33063,varname:node_9167,prsc:2|IN-7065-OUT;n:type:ShaderForge.SFN_Vector1,id:6520,x:32378,y:33465,varname:node_6520,prsc:2,v1:50;n:type:ShaderForge.SFN_Divide,id:8141,x:32532,y:33242,varname:node_8141,prsc:2|A-9585-OUT,B-6520-OUT;proporder:5951-3361-7643-4184-8234-9065-9199-9875-536-9585;pass:END;sub:END;*/

Shader "Shader Forge/Water" {
    Properties {
        _DiffuseTexture ("Diffuse Texture", 2D) = "white" {}
        _SpeedU ("Speed U", Float ) = 1
        _speedV ("speed V", Float ) = 1
        _RedGreenNoiseTexture ("Red Green Noise Texture", 2D) = "white" {}
        _DistortionStrenght ("Distortion Strenght", Range(0, 5)) = 0.509461
        _NoiseSpeedU ("Noise Speed U", Float ) = 1
        _NoisespeedV ("Noise speed V", Float ) = 1
        _Opacity ("Opacity", Range(0, 1)) = 1
        _Start ("Start", Float ) = 10
        _OpacityGradientRange ("Opacity Gradient Range", Float ) = 20
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
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseTexture; uniform float4 _DiffuseTexture_ST;
            uniform float _SpeedU;
            uniform float _speedV;
            uniform sampler2D _RedGreenNoiseTexture; uniform float4 _RedGreenNoiseTexture_ST;
            uniform float _DistortionStrenght;
            uniform float _NoiseSpeedU;
            uniform float _NoisespeedV;
            uniform float _Opacity;
            uniform float _Start;
            uniform float _OpacityGradientRange;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_5158 = _Time + _TimeEditor;
                float node_6999 = 0.01;
                float4 node_2396 = _Time + _TimeEditor;
                float node_9385 = 0.01;
                float2 node_1943 = float2(((node_2396.r*_NoiseSpeedU*node_9385)+i.uv0.r),((node_2396.r*_NoisespeedV*node_9385)+i.uv0.g));
                float4 _RedGreenNoiseTexture_var = tex2D(_RedGreenNoiseTexture,TRANSFORM_TEX(node_1943, _RedGreenNoiseTexture));
                float node_3783 = 0.5;
                float node_393 = 0.0005;
                float2 node_2476 = float2(((node_5158.r*_SpeedU*node_6999)+(((_RedGreenNoiseTexture_var.r-node_3783)*_DistortionStrenght*node_393)+i.uv0.r)),((node_5158.r*_speedV*node_6999)+(((_RedGreenNoiseTexture_var.g-node_3783)*_DistortionStrenght*node_393)+i.uv0.g)));
                float4 _DiffuseTexture_var = tex2D(_DiffuseTexture,TRANSFORM_TEX(node_2476, _DiffuseTexture));
                float3 diffuseColor = _DiffuseTexture_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,((_DiffuseTexture_var.a*_Opacity)+saturate(((distance(_WorldSpaceCameraPos,i.posWorld.rgb)-_Start)*(_OpacityGradientRange/50.0)))));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseTexture; uniform float4 _DiffuseTexture_ST;
            uniform float _SpeedU;
            uniform float _speedV;
            uniform sampler2D _RedGreenNoiseTexture; uniform float4 _RedGreenNoiseTexture_ST;
            uniform float _DistortionStrenght;
            uniform float _NoiseSpeedU;
            uniform float _NoisespeedV;
            uniform float _Opacity;
            uniform float _Start;
            uniform float _OpacityGradientRange;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_5158 = _Time + _TimeEditor;
                float node_6999 = 0.01;
                float4 node_2396 = _Time + _TimeEditor;
                float node_9385 = 0.01;
                float2 node_1943 = float2(((node_2396.r*_NoiseSpeedU*node_9385)+i.uv0.r),((node_2396.r*_NoisespeedV*node_9385)+i.uv0.g));
                float4 _RedGreenNoiseTexture_var = tex2D(_RedGreenNoiseTexture,TRANSFORM_TEX(node_1943, _RedGreenNoiseTexture));
                float node_3783 = 0.5;
                float node_393 = 0.0005;
                float2 node_2476 = float2(((node_5158.r*_SpeedU*node_6999)+(((_RedGreenNoiseTexture_var.r-node_3783)*_DistortionStrenght*node_393)+i.uv0.r)),((node_5158.r*_speedV*node_6999)+(((_RedGreenNoiseTexture_var.g-node_3783)*_DistortionStrenght*node_393)+i.uv0.g)));
                float4 _DiffuseTexture_var = tex2D(_DiffuseTexture,TRANSFORM_TEX(node_2476, _DiffuseTexture));
                float3 diffuseColor = _DiffuseTexture_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * ((_DiffuseTexture_var.a*_Opacity)+saturate(((distance(_WorldSpaceCameraPos,i.posWorld.rgb)-_Start)*(_OpacityGradientRange/50.0)))),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
