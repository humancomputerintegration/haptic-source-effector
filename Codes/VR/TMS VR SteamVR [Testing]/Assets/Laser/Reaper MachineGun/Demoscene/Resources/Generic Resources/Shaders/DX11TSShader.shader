// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:31986,y:33252|diff-66-OUT,spec-38-OUT,gloss-46-OUT,normal-56-OUT,emission-73-OUT,lwrap-92-OUT,amdfl-148-OUT,disp-110-OUT,tess-115-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33208,y:32586,ptlb:Base,ptin:_Base,tex:bf3843d8207f80c40b7c41393e2fe0eb,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3,x:32245,y:32770,ptlb:Ambient Oclussion,ptin:_AmbientOclussion,tex:1049de424d2e09b4688703cabe94fc19,ntxv:0,isnm:False|MIP-260-OUT;n:type:ShaderForge.SFN_Tex2d,id:5,x:33881,y:32885,ptlb:Normal,ptin:_Normal,tex:34ab863a17291d74897e7493962f2955,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:7,x:33393,y:32811,ptlb:Specular,ptin:_Specular,tex:a4cdca73d61814d33ac1587f6c163bca,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9,x:33393,y:32995,ptlb:Gloss,ptin:_Gloss,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:11,x:33211,y:33311,ptlb:Reflection Mask,ptin:_ReflectionMask,tex:c081210c51f6283449277244f659ec13,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:13,x:33018,y:33621,ptlb:Trans Mask,ptin:_TransMask,tex:72f052122502345f1b365b37455a8e3d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:15,x:32706,y:33866,ptlb:Displace,ptin:_Displace,tex:7f6ec34214e4ec44cb6b8d9856b2dfdd,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Cubemap,id:16,x:33417,y:33318,ptlb:Reflection Map,ptin:_ReflectionMap,cube:a596436b21c6d484bb9b3b6385e3e666,pvfc:0;n:type:ShaderForge.SFN_Lerp,id:17,x:32010,y:32742|A-3-A,B-20-OUT,T-21-OUT;n:type:ShaderForge.SFN_Vector1,id:18,x:32647,y:32641,v1:0;n:type:ShaderForge.SFN_Vector1,id:20,x:32647,y:32588,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:21,x:32245,y:32960,ptlb:AO Burn,ptin:_AOBurn,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:22,x:32245,y:33057,ptlb:AO Level,ptin:_AOLevel,glob:False,v1:0.5;n:type:ShaderForge.SFN_SwitchProperty,id:24,x:32245,y:32608,ptlb:AO Enable,ptin:_AOEnable,on:True|A-20-OUT,B-220-OUT;n:type:ShaderForge.SFN_Multiply,id:25,x:33050,y:32603|A-2-RGB,B-27-OUT;n:type:ShaderForge.SFN_Tex2d,id:26,x:33208,y:32392,ptlb:Mask Map,ptin:_MaskMap,tex:a4cdca73d61814d33ac1587f6c163bca,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:27,x:33037,y:32392|IN-26-A;n:type:ShaderForge.SFN_Color,id:29,x:33013,y:32213,ptlb:Color Tint,ptin:_ColorTint,glob:False,c1:0.02205884,c2:1,c3:0.5808825,c4:1;n:type:ShaderForge.SFN_Multiply,id:30,x:32840,y:32350|A-29-RGB,B-26-A;n:type:ShaderForge.SFN_Add,id:31,x:32877,y:32541|A-30-OUT,B-25-OUT;n:type:ShaderForge.SFN_Multiply,id:32,x:32877,y:32691|A-24-OUT,B-168-OUT;n:type:ShaderForge.SFN_Multiply,id:38,x:33177,y:32811|A-7-A,B-39-OUT,C-24-OUT,D-176-RGB;n:type:ShaderForge.SFN_ValueProperty,id:39,x:33177,y:32950,ptlb:Specular Level,ptin:_SpecularLevel,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:46,x:32954,y:33019|A-47-OUT,B-48-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:47,x:33087,y:33155,ptlb:Custom Gloss,ptin:_CustomGloss,on:False|A-20-OUT,B-9-A;n:type:ShaderForge.SFN_Slider,id:48,x:33099,y:33039,ptlb:Shinniness,ptin:_Shinniness,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Vector3,id:54,x:33881,y:33057,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_ValueProperty,id:55,x:33881,y:33177,ptlb:Normal Smooth,ptin:_NormalSmooth,glob:False,v1:0;n:type:ShaderForge.SFN_Lerp,id:56,x:33670,y:33029|A-5-RGB,B-54-OUT,T-55-OUT;n:type:ShaderForge.SFN_Multiply,id:62,x:33029,y:33311|A-16-RGB,B-11-A;n:type:ShaderForge.SFN_Color,id:63,x:32912,y:33191,ptlb:Reflection Tint,ptin:_ReflectionTint,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:64,x:32912,y:33393,ptlb:Reflection Power,ptin:_ReflectionPower,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:65,x:32755,y:33191|A-63-RGB,B-62-OUT,C-64-OUT;n:type:ShaderForge.SFN_Blend,id:66,x:32682,y:32892,blmd:5,clmp:True|SRC-32-OUT,DST-65-OUT;n:type:ShaderForge.SFN_ValueProperty,id:72,x:32771,y:33393,ptlb:Reflection Emission,ptin:_ReflectionEmission,glob:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:73,x:32608,y:33271|A-65-OUT,B-72-OUT;n:type:ShaderForge.SFN_ValueProperty,id:80,x:33018,y:33804,ptlb:Trans Power,ptin:_TransPower,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:81,x:32807,y:33614|A-13-A,B-80-OUT,C-285-RGB;n:type:ShaderForge.SFN_SwitchProperty,id:92,x:32618,y:33566,ptlb:Translucency,ptin:_Translucency,on:False|A-18-OUT,B-81-OUT;n:type:ShaderForge.SFN_Lerp,id:108,x:32525,y:33957|A-20-OUT,B-15-A,T-109-OUT;n:type:ShaderForge.SFN_ValueProperty,id:109,x:32706,y:34067,ptlb:Disp Burn,ptin:_DispBurn,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:110,x:32283,y:33710|A-113-OUT,B-112-OUT,C-121-OUT;n:type:ShaderForge.SFN_Max,id:111,x:32332,y:33898|A-18-OUT,B-108-OUT;n:type:ShaderForge.SFN_Min,id:112,x:32113,y:33898|A-20-OUT,B-111-OUT;n:type:ShaderForge.SFN_ValueProperty,id:113,x:32706,y:33784,ptlb:Disp Power,ptin:_DispPower,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:115,x:32087,y:33791,ptlb:Tesellation,ptin:_Tesellation,glob:False,v1:1;n:type:ShaderForge.SFN_NormalVector,id:121,x:32535,y:33740,pt:False;n:type:ShaderForge.SFN_Multiply,id:137,x:32306,y:34452|A-138-OUT,B-140-OUT,C-141-RGB;n:type:ShaderForge.SFN_Fresnel,id:138,x:32576,y:34331|EXP-139-OUT;n:type:ShaderForge.SFN_ValueProperty,id:139,x:32752,y:34349,ptlb:Rim Fresnel,ptin:_RimFresnel,glob:False,v1:3;n:type:ShaderForge.SFN_ValueProperty,id:140,x:32576,y:34486,ptlb:Rim Power,ptin:_RimPower,glob:False,v1:1;n:type:ShaderForge.SFN_Color,id:141,x:32576,y:34578,ptlb:Rim Color,ptin:_RimColor,glob:False,c1:0.9264706,c2:0.5858564,c3:0.5858564,c4:1;n:type:ShaderForge.SFN_Tex2d,id:142,x:32576,y:34761,ptlb:Rim Mask,ptin:_RimMask,tex:02d3f3b528aa60d4ea869d616c7fe3b5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Blend,id:143,x:32306,y:34604,blmd:0,clmp:True|SRC-145-OUT,DST-142-A;n:type:ShaderForge.SFN_Max,id:145,x:32130,y:34452|A-18-OUT,B-137-OUT;n:type:ShaderForge.SFN_Multiply,id:146,x:32260,y:34836|A-149-RGB,B-150-OUT;n:type:ShaderForge.SFN_Add,id:148,x:31977,y:34706|A-143-OUT,B-146-OUT;n:type:ShaderForge.SFN_AmbientLight,id:149,x:32595,y:34945;n:type:ShaderForge.SFN_ValueProperty,id:150,x:32595,y:35100,ptlb:Ambiental Power,ptin:_AmbientalPower,glob:False,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:168,x:32678,y:32774,ptlb:Color Mask,ptin:_ColorMask,on:False|A-2-RGB,B-31-OUT;n:type:ShaderForge.SFN_Color,id:176,x:33007,y:32852,ptlb:Spec Color,ptin:_SpecColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Min,id:197,x:31850,y:32899|A-243-OUT,B-20-OUT;n:type:ShaderForge.SFN_Max,id:220,x:31650,y:32899|A-197-OUT,B-18-OUT;n:type:ShaderForge.SFN_Blend,id:243,x:32033,y:32960,blmd:10,clmp:True|SRC-17-OUT,DST-22-OUT;n:type:ShaderForge.SFN_Slider,id:260,x:32130,y:33157,ptlb:AO Detail,ptin:_AODetail,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Color,id:285,x:32857,y:33477,ptlb:Trans Color,ptin:_TransColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:2-168-29-26-176-39-7-48-47-9-5-55-24-3-21-22-260-63-64-16-11-72-92-80-285-13-115-113-109-15-141-140-139-142-150;pass:END;sub:END;*/

Shader "DLNK/DX11TSShader" {
    Properties {
        _Base ("Base", 2D) = "white" {}
        [MaterialToggle] _ColorMask ("Color Mask", Float ) = 0.1607843
        _ColorTint ("Color Tint", Color) = (0.02205884,1,0.5808825,1)
        _MaskMap ("Mask Map", 2D) = "white" {}
        _SpecColor ("Spec Color", Color) = (0.5,0.5,0.5,1)
        _SpecularLevel ("Specular Level", Float ) = 1
        _Specular ("Specular", 2D) = "white" {}
        _Shinniness ("Shinniness", Range(0, 1)) = 0.5
        [MaterialToggle] _CustomGloss ("Custom Gloss", Float ) = 1
        _Gloss ("Gloss", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _NormalSmooth ("Normal Smooth", Float ) = 0
        [MaterialToggle] _AOEnable ("AO Enable", Float ) = 1
        _AmbientOclussion ("Ambient Oclussion", 2D) = "white" {}
        _AOBurn ("AO Burn", Float ) = 0
        _AOLevel ("AO Level", Float ) = 0.5
        _AODetail ("AO Detail", Range(0, 10)) = 0
        _ReflectionTint ("Reflection Tint", Color) = (1,1,1,1)
        _ReflectionPower ("Reflection Power", Float ) = 1
        _ReflectionMap ("Reflection Map", Cube) = "_Skybox" {}
        _ReflectionMask ("Reflection Mask", 2D) = "white" {}
        _ReflectionEmission ("Reflection Emission", Float ) = 0
        [MaterialToggle] _Translucency ("Translucency", Float ) = 0
        _TransPower ("Trans Power", Float ) = 1
        _TransColor ("Trans Color", Color) = (0.5,0.5,0.5,1)
        _TransMask ("Trans Mask", 2D) = "white" {}
        _Tesellation ("Tesellation", Float ) = 1
        _DispPower ("Disp Power", Float ) = 1
        _DispBurn ("Disp Burn", Float ) = 1
        _Displace ("Displace", 2D) = "white" {}
        _RimColor ("Rim Color", Color) = (0.9264706,0.5858564,0.5858564,1)
        _RimPower ("Rim Power", Float ) = 1
        _RimFresnel ("Rim Fresnel", Float ) = 3
        _RimMask ("Rim Mask", 2D) = "white" {}
        _AmbientalPower ("Ambiental Power", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 5.0
            #pragma glsl
            uniform float4 _LightColor0;
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform sampler2D _AmbientOclussion; uniform float4 _AmbientOclussion_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform sampler2D _Gloss; uniform float4 _Gloss_ST;
            uniform sampler2D _ReflectionMask; uniform float4 _ReflectionMask_ST;
            uniform sampler2D _TransMask; uniform float4 _TransMask_ST;
            uniform sampler2D _Displace; uniform float4 _Displace_ST;
            uniform samplerCUBE _ReflectionMap;
            uniform float _AOBurn;
            uniform float _AOLevel;
            uniform fixed _AOEnable;
            uniform sampler2D _MaskMap; uniform float4 _MaskMap_ST;
            uniform float4 _ColorTint;
            uniform float _SpecularLevel;
            uniform fixed _CustomGloss;
            uniform float _Shinniness;
            uniform float _NormalSmooth;
            uniform float4 _ReflectionTint;
            uniform float _ReflectionPower;
            uniform float _ReflectionEmission;
            uniform float _TransPower;
            uniform fixed _Translucency;
            uniform float _DispBurn;
            uniform float _DispPower;
            uniform float _Tesellation;
            uniform float _RimFresnel;
            uniform float _RimPower;
            uniform float4 _RimColor;
            uniform sampler2D _RimMask; uniform float4 _RimMask_ST;
            uniform float _AmbientalPower;
            uniform fixed _ColorMask;
            uniform float4 _SpecColor;
            uniform float _AODetail;
            uniform float4 _TransColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float node_20 = 1.0;
                    float node_18 = 0.0;
                    float2 node_298 = v.texcoord0;
                    v.vertex.xyz +=  (_DispPower*min(node_20,max(node_18,lerp(node_20,tex2Dlod(_Displace,float4(TRANSFORM_TEX(node_298.rg, _Displace),0.0,0)).a,_DispBurn)))*v.normal);
                }
                float Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    return _Tesellation;
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o;
                    float ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts;
                    o.edge[1] = ts;
                    o.edge[2] = ts;
                    o.inside = ts;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_298 = i.uv0;
                float3 normalLocal = lerp(UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_298.rg, _Normal))).rgb,float3(0,0,1),_NormalSmooth);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float node_18 = 0.0;
                float3 w = lerp( node_18, (tex2D(_TransMask,TRANSFORM_TEX(node_298.rg, _TransMask)).a*_TransPower*_TransColor.rgb), _Translucency )*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 diffuse = forwardLight * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
////// Emissive:
                float3 node_65 = (_ReflectionTint.rgb*(texCUBE(_ReflectionMap,viewReflectDirection).rgb*tex2D(_ReflectionMask,TRANSFORM_TEX(node_298.rg, _ReflectionMask)).a)*_ReflectionPower);
                float3 emissive = (node_65*_ReflectionEmission);
///////// Gloss:
                float node_20 = 1.0;
                float gloss = (lerp( node_20, tex2D(_Gloss,TRANSFORM_TEX(node_298.rg, _Gloss)).a, _CustomGloss )*_Shinniness);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float node_24 = lerp( node_20, max(min(saturate(( _AOLevel > 0.5 ? (1.0-(1.0-2.0*(_AOLevel-0.5))*(1.0-lerp(tex2Dlod(_AmbientOclussion,float4(TRANSFORM_TEX(node_298.rg, _AmbientOclussion),0.0,_AODetail)).a,node_20,_AOBurn))) : (2.0*_AOLevel*lerp(tex2Dlod(_AmbientOclussion,float4(TRANSFORM_TEX(node_298.rg, _AmbientOclussion),0.0,_AODetail)).a,node_20,_AOBurn)) )),node_20),node_18), _AOEnable );
                float3 specularColor = (tex2D(_Specular,TRANSFORM_TEX(node_298.rg, _Specular)).a*_SpecularLevel*node_24*_SpecColor.rgb);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                diffuseLight += (saturate(min(max(node_18,(pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimFresnel)*_RimPower*_RimColor.rgb)),tex2D(_RimMask,TRANSFORM_TEX(node_298.rg, _RimMask)).a))+(UNITY_LIGHTMODEL_AMBIENT.rgb*_AmbientalPower)); // Diffuse Ambient Light
                float4 node_2 = tex2D(_Base,TRANSFORM_TEX(node_298.rg, _Base));
                float4 node_26 = tex2D(_MaskMap,TRANSFORM_TEX(node_298.rg, _MaskMap));
                finalColor += diffuseLight * saturate(max((node_24*lerp( node_2.rgb, ((_ColorTint.rgb*node_26.a)+(node_2.rgb*(1.0 - node_26.a))), _ColorMask )),node_65));
                finalColor += specular;
                finalColor += emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 5.0
            #pragma glsl
            uniform float4 _LightColor0;
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform sampler2D _AmbientOclussion; uniform float4 _AmbientOclussion_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform sampler2D _Gloss; uniform float4 _Gloss_ST;
            uniform sampler2D _ReflectionMask; uniform float4 _ReflectionMask_ST;
            uniform sampler2D _TransMask; uniform float4 _TransMask_ST;
            uniform sampler2D _Displace; uniform float4 _Displace_ST;
            uniform samplerCUBE _ReflectionMap;
            uniform float _AOBurn;
            uniform float _AOLevel;
            uniform fixed _AOEnable;
            uniform sampler2D _MaskMap; uniform float4 _MaskMap_ST;
            uniform float4 _ColorTint;
            uniform float _SpecularLevel;
            uniform fixed _CustomGloss;
            uniform float _Shinniness;
            uniform float _NormalSmooth;
            uniform float4 _ReflectionTint;
            uniform float _ReflectionPower;
            uniform float _ReflectionEmission;
            uniform float _TransPower;
            uniform fixed _Translucency;
            uniform float _DispBurn;
            uniform float _DispPower;
            uniform float _Tesellation;
            uniform fixed _ColorMask;
            uniform float4 _SpecColor;
            uniform float _AODetail;
            uniform float4 _TransColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float node_20 = 1.0;
                    float node_18 = 0.0;
                    float2 node_299 = v.texcoord0;
                    v.vertex.xyz +=  (_DispPower*min(node_20,max(node_18,lerp(node_20,tex2Dlod(_Displace,float4(TRANSFORM_TEX(node_299.rg, _Displace),0.0,0)).a,_DispBurn)))*v.normal);
                }
                float Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    return _Tesellation;
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o;
                    float ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts;
                    o.edge[1] = ts;
                    o.edge[2] = ts;
                    o.inside = ts;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_299 = i.uv0;
                float3 normalLocal = lerp(UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_299.rg, _Normal))).rgb,float3(0,0,1),_NormalSmooth);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float node_18 = 0.0;
                float3 w = lerp( node_18, (tex2D(_TransMask,TRANSFORM_TEX(node_299.rg, _TransMask)).a*_TransPower*_TransColor.rgb), _Translucency )*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                float3 diffuse = forwardLight * attenColor;
///////// Gloss:
                float node_20 = 1.0;
                float gloss = (lerp( node_20, tex2D(_Gloss,TRANSFORM_TEX(node_299.rg, _Gloss)).a, _CustomGloss )*_Shinniness);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float node_24 = lerp( node_20, max(min(saturate(( _AOLevel > 0.5 ? (1.0-(1.0-2.0*(_AOLevel-0.5))*(1.0-lerp(tex2Dlod(_AmbientOclussion,float4(TRANSFORM_TEX(node_299.rg, _AmbientOclussion),0.0,_AODetail)).a,node_20,_AOBurn))) : (2.0*_AOLevel*lerp(tex2Dlod(_AmbientOclussion,float4(TRANSFORM_TEX(node_299.rg, _AmbientOclussion),0.0,_AODetail)).a,node_20,_AOBurn)) )),node_20),node_18), _AOEnable );
                float3 specularColor = (tex2D(_Specular,TRANSFORM_TEX(node_299.rg, _Specular)).a*_SpecularLevel*node_24*_SpecColor.rgb);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_2 = tex2D(_Base,TRANSFORM_TEX(node_299.rg, _Base));
                float4 node_26 = tex2D(_MaskMap,TRANSFORM_TEX(node_299.rg, _MaskMap));
                float3 node_65 = (_ReflectionTint.rgb*(texCUBE(_ReflectionMap,viewReflectDirection).rgb*tex2D(_ReflectionMask,TRANSFORM_TEX(node_299.rg, _ReflectionMask)).a)*_ReflectionPower);
                finalColor += diffuseLight * saturate(max((node_24*lerp( node_2.rgb, ((_ColorTint.rgb*node_26.a)+(node_2.rgb*(1.0 - node_26.a))), _ColorMask )),node_65));
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 5.0
            #pragma glsl
            uniform sampler2D _Displace; uniform float4 _Displace_ST;
            uniform float _DispBurn;
            uniform float _DispPower;
            uniform float _Tesellation;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
                float3 normalDir : TEXCOORD6;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float node_20 = 1.0;
                    float node_18 = 0.0;
                    float2 node_300 = v.texcoord0;
                    v.vertex.xyz +=  (_DispPower*min(node_20,max(node_18,lerp(node_20,tex2Dlod(_Displace,float4(TRANSFORM_TEX(node_300.rg, _Displace),0.0,0)).a,_DispBurn)))*v.normal);
                }
                float Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    return _Tesellation;
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o;
                    float ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts;
                    o.edge[1] = ts;
                    o.edge[2] = ts;
                    o.inside = ts;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 5.0
            #pragma glsl
            uniform sampler2D _Displace; uniform float4 _Displace_ST;
            uniform float _DispBurn;
            uniform float _DispPower;
            uniform float _Tesellation;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float node_20 = 1.0;
                    float node_18 = 0.0;
                    float2 node_301 = v.texcoord0;
                    v.vertex.xyz +=  (_DispPower*min(node_20,max(node_18,lerp(node_20,tex2Dlod(_Displace,float4(TRANSFORM_TEX(node_301.rg, _Displace),0.0,0)).a,_DispBurn)))*v.normal);
                }
                float Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    return _Tesellation;
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o;
                    float ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts;
                    o.edge[1] = ts;
                    o.edge[2] = ts;
                    o.inside = ts;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
