Shader "Custom/Grey" {
 Properties {
 	_bwBlend ("Black & White blend", Range (0, 1)) = 0
	_MainTex ("Base (RGBA)", 2D) = "white" {}

 }
 SubShader {
    Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
    Blend SrcAlpha OneMinusSrcAlpha
    Cull Back
     LOD 200

     CGPROGRAM
     #pragma surface surf Standard vertex:vert fullforwardshadows alpha:fade
     #pragma target 3.0

     struct Input {
         float4 color : COLOR; // Vertex color stored here by vert() method
				 float4 uv; // vertex coordinate
     };

     void vert (inout appdata_full v, out Input o)
     {
         UNITY_INITIALIZE_OUTPUT(Input,o);
         o.color = v.color; // Save the Vertex Color in the Input for the surf() method
				 o.uv = v.texcoord;
     }

		 uniform fixed _bwBlend;
		 uniform sampler2D _MainTex;

     void surf (Input IN, inout SurfaceOutputStandard o)
     {
         // Albedo comes from a texture tinted by color
         fixed4 c = IN.color;

//				 fixed lum = (c.r*.3 + c.g*.59 + c.b*.11);
//				 fixed3 bw = fixed3( lum, lum, lum );

//         o.Albedo = lerp(c.rgb, bw, _bwBlend);
		     o.Albedo = c.rgb;

         o.Alpha = c.a;

     }
     ENDCG
 }
 FallBack "Diffuse"
}
