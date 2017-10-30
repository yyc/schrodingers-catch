Shader "Custom/Greyscale" {
	Properties {
		_MainTex ("Base (RGBA)", 2D) = "white" {}
		_bwBlend ("Black & White blend", Range (0, 1)) = 0
	}
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma surface surf Standard alpha:fade

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _bwBlend;

			float4 frag(v2f_img i) : COLOR {
				float4 c = tex2D(_MainTex, i.uv);

				float lum = (c.r*.3 + c.g*.59 + c.b*.11) * c.a;
				float3 bw = float3( lum, lum, lum );

				float4 result = c;
				result.rgb = lerp(c.rgb, bw, _bwBlend);
				result.a = 0;
				return result;
			}
			ENDCG
		}
	}
}