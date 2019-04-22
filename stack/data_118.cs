public void Update ()
{
     changeCount = changeCount - 0.05;

     textureObject.renderer.material.SetFloat( "_Blend", changeCount );
      if(changeCount <= 0) {
           triggerChange = false;
           changeCount = 1.0;
           textureObject.renderer.material.SetTexture ("_Texture2", newTexture);
           textureObject.renderer.material.SetFloat( "_Blend", 1);
      }

 }
}