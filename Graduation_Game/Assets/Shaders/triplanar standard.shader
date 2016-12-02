// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:True,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33384,y:32787,varname:node_4013,prsc:2|diff-4435-OUT,emission-2655-OUT,transm-7878-OUT,lwrap-7878-OUT;n:type:ShaderForge.SFN_NormalVector,id:106,x:31694,y:33483,prsc:2,pt:False;n:type:ShaderForge.SFN_FragmentPosition,id:318,x:31365,y:32687,varname:node_318,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:6382,x:32143,y:32714,ptovrint:False,ptlb:Texture X-axis,ptin:_TextureXaxis,varname:node_6382,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9077-OUT;n:type:ShaderForge.SFN_Append,id:8650,x:31662,y:32714,varname:node_8650,prsc:2|A-318-Y,B-318-Z;n:type:ShaderForge.SFN_Append,id:8227,x:31662,y:32574,varname:node_8227,prsc:2|A-318-X,B-318-Z;n:type:ShaderForge.SFN_Append,id:2730,x:31662,y:32857,varname:node_2730,prsc:2|A-318-X,B-318-Y;n:type:ShaderForge.SFN_Tex2d,id:4740,x:32143,y:32542,ptovrint:False,ptlb:Texture Y-axis,ptin:_TextureYaxis,varname:node_4740,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-6283-OUT;n:type:ShaderForge.SFN_Tex2d,id:7687,x:32143,y:32887,ptovrint:False,ptlb:Texture Z-axis,ptin:_TextureZaxis,varname:node_7687,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8157-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7642,x:31662,y:32514,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_7642,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.25;n:type:ShaderForge.SFN_Multiply,id:9077,x:31915,y:32714,varname:node_9077,prsc:2|A-8650-OUT,B-7642-OUT;n:type:ShaderForge.SFN_Multiply,id:6283,x:31915,y:32574,varname:node_6283,prsc:2|A-8227-OUT,B-7642-OUT;n:type:ShaderForge.SFN_Multiply,id:8157,x:31915,y:32846,varname:node_8157,prsc:2|A-2730-OUT,B-7642-OUT;n:type:ShaderForge.SFN_Power,id:4555,x:32265,y:33497,varname:node_4555,prsc:2|VAL-4949-OUT,EXP-382-OUT;n:type:ShaderForge.SFN_Abs,id:4949,x:31890,y:33493,varname:node_4949,prsc:2|IN-106-OUT;n:type:ShaderForge.SFN_Smoothstep,id:9304,x:32467,y:33366,varname:node_9304,prsc:2|A-2880-OUT,B-3184-OUT,V-4555-OUT;n:type:ShaderForge.SFN_ChannelBlend,id:3993,x:32772,y:32703,varname:node_3993,prsc:2,chbt:1|M-9304-OUT,R-9604-OUT,G-6326-OUT,B-4322-OUT,BTM-9604-OUT;n:type:ShaderForge.SFN_ValueProperty,id:382,x:32028,y:33558,ptovrint:False,ptlb:Texture Blend Power,ptin:_TextureBlendPower,varname:node_382,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3.44;n:type:ShaderForge.SFN_Slider,id:2880,x:31933,y:33297,ptovrint:False,ptlb:Min,ptin:_Min,varname:node_2880,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.281,max:1;n:type:ShaderForge.SFN_Slider,id:3184,x:31933,y:33404,ptovrint:False,ptlb:Max,ptin:_Max,varname:node_3184,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.344,max:1;n:type:ShaderForge.SFN_Multiply,id:6326,x:32488,y:32557,varname:node_6326,prsc:2|A-4740-RGB,B-4517-RGB;n:type:ShaderForge.SFN_Color,id:4517,x:32303,y:32602,ptovrint:False,ptlb:Texture Y Color,ptin:_TextureYColor,varname:node_4517,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:2562,x:32107,y:33675,varname:node_2562,prsc:2|EXP-398-OUT;n:type:ShaderForge.SFN_Vector1,id:269,x:32107,y:33831,varname:node_269,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:398,x:31641,y:33678,ptovrint:False,ptlb:Fresnel Power,ptin:_FresnelPower,varname:node_398,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.21,max:10;n:type:ShaderForge.SFN_Slider,id:4541,x:31960,y:33927,ptovrint:False,ptlb:Fresnel Intensity,ptin:_FresnelIntensity,varname:node_4541,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:5872,x:32349,y:33675,varname:node_5872,prsc:2|A-2562-OUT,B-269-OUT,T-4541-OUT;n:type:ShaderForge.SFN_Color,id:2462,x:32349,y:33871,ptovrint:False,ptlb:Fresnel Color,ptin:_FresnelColor,varname:node_2462,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3529412,c2:0.572549,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:1418,x:32576,y:33675,varname:node_1418,prsc:2|A-5872-OUT,B-2462-RGB;n:type:ShaderForge.SFN_Multiply,id:9604,x:32488,y:32714,varname:node_9604,prsc:2|A-6382-RGB,B-4999-RGB;n:type:ShaderForge.SFN_Multiply,id:4322,x:32488,y:32878,varname:node_4322,prsc:2|A-7687-RGB,B-4999-RGB;n:type:ShaderForge.SFN_Color,id:4999,x:32303,y:32814,ptovrint:False,ptlb:Texture X and Z Color,ptin:_TextureXandZColor,varname:node_4999,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.627451,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:3011,x:32804,y:33024,ptovrint:False,ptlb:LightWrap / Transmission,ptin:_LightWrapTransmission,varname:node_3011,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2784314,c2:0.4313726,c3:0.7960785,c4:1;n:type:ShaderForge.SFN_FragmentPosition,id:5374,x:32191,y:32068,varname:node_5374,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:3052,x:32179,y:32230,ptovrint:False,ptlb:Fade Scale,ptin:_FadeScale,varname:node_3052,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:9564,x:32426,y:32086,varname:node_9564,prsc:2|A-5374-Y,B-3052-OUT;n:type:ShaderForge.SFN_Add,id:3782,x:32612,y:32086,varname:node_3782,prsc:2|A-150-OUT,B-9564-OUT;n:type:ShaderForge.SFN_ValueProperty,id:150,x:32373,y:32011,ptovrint:False,ptlb:Fade Offset,ptin:_FadeOffset,varname:node_150,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;n:type:ShaderForge.SFN_Clamp,id:6138,x:32795,y:32081,varname:node_6138,prsc:2|IN-3782-OUT,MIN-7165-OUT,MAX-9899-OUT;n:type:ShaderForge.SFN_Vector1,id:7165,x:32597,y:32293,varname:node_7165,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:9899,x:32597,y:32360,varname:node_9899,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:4866,x:32747,y:32460,ptovrint:False,ptlb:Fade Color,ptin:_FadeColor,varname:node_4866,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:4435,x:33077,y:32409,varname:node_4435,prsc:2|A-4866-RGB,B-3993-OUT,T-6138-OUT;n:type:ShaderForge.SFN_Lerp,id:7878,x:33026,y:33005,varname:node_7878,prsc:2|A-4866-RGB,B-3011-RGB,T-6138-OUT;n:type:ShaderForge.SFN_Lerp,id:2655,x:33072,y:33285,varname:node_2655,prsc:2|A-4866-RGB,B-1418-OUT,T-6138-OUT;proporder:7642-4517-4740-4999-6382-7687-382-2880-3184-2462-398-4541-3011-3052-150-4866;pass:END;sub:END;*/

Shader "Shader Forge/triplanar standard" {
    Properties {
        _Scale ("Scale", Float ) = 0.25
        _TextureYColor ("Texture Y Color", Color) = (1,1,1,1)
        _TextureYaxis ("Texture Y-axis", 2D) = "white" {}
        _TextureXandZColor ("Texture X and Z Color", Color) = (0,0.627451,1,1)
        _TextureXaxis ("Texture X-axis", 2D) = "white" {}
        _TextureZaxis ("Texture Z-axis", 2D) = "white" {}
        _TextureBlendPower ("Texture Blend Power", Float ) = 3.44
        _Min ("Min", Range(0, 1)) = 0.281
        _Max ("Max", Range(0, 1)) = 0.344
        _FresnelColor ("Fresnel Color", Color) = (0.3529412,0.572549,1,1)
        _FresnelPower ("Fresnel Power", Range(0, 10)) = 1.21
        _FresnelIntensity ("Fresnel Intensity", Range(0, 1)) = 0
        _LightWrapTransmission ("LightWrap / Transmission", Color) = (0.2784314,0.4313726,0.7960785,1)
        _FadeScale ("Fade Scale", Float ) = 1
        _FadeOffset ("Fade Offset", Float ) = 0.8
        _FadeColor ("Fade Color", Color) = (0,0,0,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _TextureXaxis; uniform float4 _TextureXaxis_ST;
            uniform sampler2D _TextureYaxis; uniform float4 _TextureYaxis_ST;
            uniform sampler2D _TextureZaxis; uniform float4 _TextureZaxis_ST;
            uniform float _Scale;
            uniform float _TextureBlendPower;
            uniform float _Min;
            uniform float _Max;
            uniform float4 _TextureYColor;
            uniform float _FresnelPower;
            uniform float _FresnelIntensity;
            uniform float4 _FresnelColor;
            uniform float4 _TextureXandZColor;
            uniform float4 _LightWrapTransmission;
            uniform float _FadeScale;
            uniform float _FadeOffset;
            uniform float4 _FadeColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float node_6138 = clamp((_FadeOffset+(i.posWorld.g*_FadeScale)),0.0,1.0);
                float3 node_7878 = lerp(_FadeColor.rgb,_LightWrapTransmission.rgb,node_6138);
                float3 w = node_7878*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * node_7878;
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight)*(0.5-max(w.r,max(w.g,w.b))*0.5) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 node_9304 = smoothstep( float3(_Min,_Min,_Min), float3(_Max,_Max,_Max), pow(abs(i.normalDir),_TextureBlendPower) );
                float2 node_9077 = (float2(i.posWorld.g,i.posWorld.b)*_Scale);
                float4 _TextureXaxis_var = tex2D(_TextureXaxis,TRANSFORM_TEX(node_9077, _TextureXaxis));
                float3 node_9604 = (_TextureXaxis_var.rgb*_TextureXandZColor.rgb);
                float2 node_6283 = (float2(i.posWorld.r,i.posWorld.b)*_Scale);
                float4 _TextureYaxis_var = tex2D(_TextureYaxis,TRANSFORM_TEX(node_6283, _TextureYaxis));
                float2 node_8157 = (float2(i.posWorld.r,i.posWorld.g)*_Scale);
                float4 _TextureZaxis_var = tex2D(_TextureZaxis,TRANSFORM_TEX(node_8157, _TextureZaxis));
                float3 diffuseColor = lerp(_FadeColor.rgb,(lerp( lerp( lerp( node_9604, node_9604, node_9304.r ), (_TextureYaxis_var.rgb*_TextureYColor.rgb), node_9304.g ), (_TextureZaxis_var.rgb*_TextureXandZColor.rgb), node_9304.b )),node_6138);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = lerp(_FadeColor.rgb,(lerp(pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower),0.0,_FresnelIntensity)*_FresnelColor.rgb),node_6138);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _TextureXaxis; uniform float4 _TextureXaxis_ST;
            uniform sampler2D _TextureYaxis; uniform float4 _TextureYaxis_ST;
            uniform sampler2D _TextureZaxis; uniform float4 _TextureZaxis_ST;
            uniform float _Scale;
            uniform float _TextureBlendPower;
            uniform float _Min;
            uniform float _Max;
            uniform float4 _TextureYColor;
            uniform float _FresnelPower;
            uniform float _FresnelIntensity;
            uniform float4 _FresnelColor;
            uniform float4 _TextureXandZColor;
            uniform float4 _LightWrapTransmission;
            uniform float _FadeScale;
            uniform float _FadeOffset;
            uniform float4 _FadeColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float node_6138 = clamp((_FadeOffset+(i.posWorld.g*_FadeScale)),0.0,1.0);
                float3 node_7878 = lerp(_FadeColor.rgb,_LightWrapTransmission.rgb,node_6138);
                float3 w = node_7878*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * node_7878;
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight)*(0.5-max(w.r,max(w.g,w.b))*0.5) * attenColor;
                float3 node_9304 = smoothstep( float3(_Min,_Min,_Min), float3(_Max,_Max,_Max), pow(abs(i.normalDir),_TextureBlendPower) );
                float2 node_9077 = (float2(i.posWorld.g,i.posWorld.b)*_Scale);
                float4 _TextureXaxis_var = tex2D(_TextureXaxis,TRANSFORM_TEX(node_9077, _TextureXaxis));
                float3 node_9604 = (_TextureXaxis_var.rgb*_TextureXandZColor.rgb);
                float2 node_6283 = (float2(i.posWorld.r,i.posWorld.b)*_Scale);
                float4 _TextureYaxis_var = tex2D(_TextureYaxis,TRANSFORM_TEX(node_6283, _TextureYaxis));
                float2 node_8157 = (float2(i.posWorld.r,i.posWorld.g)*_Scale);
                float4 _TextureZaxis_var = tex2D(_TextureZaxis,TRANSFORM_TEX(node_8157, _TextureZaxis));
                float3 diffuseColor = lerp(_FadeColor.rgb,(lerp( lerp( lerp( node_9604, node_9604, node_9304.r ), (_TextureYaxis_var.rgb*_TextureYColor.rgb), node_9304.g ), (_TextureZaxis_var.rgb*_TextureXandZColor.rgb), node_9304.b )),node_6138);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
