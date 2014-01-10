$colFX::greenMul = 1;
$colFX::redMul = 1;
$colFX::blueMul = 1;

singleton ShaderData( PFX_colFXShader )  
{     
   DXVertexShaderFile   = "shaders/common/postFx/postFxV.hlsl";  //as we don't need a specialized vertex shader, we use the bare-bones postFxV.hlsl
   DXPixelShaderFile    = "shaders/custom/colTest.hlsl";  //our new pixel shader
        
   samplerNames[0] = "$inputTex";  
     
   pixVersion = 3.0;  
};  

singleton PostEffect(colFX)  
{  
   renderTime = "PFXAfterDiffuse";  
        
   shader = PFX_colFXShader;  
   stateBlock = PFX_DefaultStateBlock;  
   texture[0] = "$backbuffer";  
};

function colFX::setShaderConsts( %this )
{
	%this.setShaderConst( "$greenMul", $colFX::greenMul );	//here we set the constants for the shaders
	%this.setShaderConst( "$redMul", $colFX::redMul );	//you could substitute "$ZoomFX::samples" with "getRandom(1,50)", if you want goofy results...
	%this.setShaderConst( "$blueMul", $colFX::blueMul );
}

colFX.enable();