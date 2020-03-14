Asset Creator - Vladislav Horobets (ErbGameArt).
-----------------------------------------------------

Using:

If you want to use post-effect like in the demo video:

1) Download unity free post effects 
https://assetstore.unity.com/packages/essentials/post-processing-stack-83912
2) Add "PostProcessingBehaviour.cs" on main Camera.
3) Set the "Post-effects" profile. ("\Assets\ErbGameArt\Demo scene\CC.asset")
4) You should turn on "HDR" on main camera for correct post-effects. (bloom post-effect works correctly only with HDR)
If you have forward rendering path (by default in Unity), you need disable antialiasing "edit->project settings->quality->antialiasing"
or turn of "MSAA" on main camera, because HDR does not works with msaa. If you want to use HDR and MSAA then use "MSAA of post effect". 
It's faster then default MSAA and have the same quality.


2) Shaders
2.1)The "Use depth" on the material from the custom shaders is the Soft Particle Factor.
2.2)Use "Center glow"[MaterialToggle] only with particle system. This option is used to darken the main texture with a white texture (white is visible, black is invisible).
    If you turn on this feature, you need to use "Custom vertex stream" (Uv0.Custom.xy) in tab "Render". And don't forget to use "Custom data" parameters in your PS.
2.3)The distortion shader only works with standard rendering. Delete (if exist) distortion particles from effects if you use LWRP or HDRP!
2.4)You can change the cutoff in all shaders (except Add_CenterGlow and Blend_CenterGlow ) using (Uv0.Custom.xy) in particle system.

3)Linear color space + HDRP
3.1)Dissable soft particles or uncheck checkbutton "Use Depth" in all materials if you use HDRP.
3.2)Reduce Mask clip value in all materials from Blend_TwoSides shader if you use Linear color space.
    You can find all materials by searching ts and twoSides

Contact me if you have any questions, ideas or suggestions.
My email: gorobecn2@gmail.com