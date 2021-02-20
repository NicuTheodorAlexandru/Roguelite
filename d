[33mcommit b983bfd019c78bc997d687c59fb5ba1abb3d2e4a[m[33m ([m[1;36mHEAD -> [m[1;32mmaster[m[33m, [m[1;31morigin/master[m[33m)[m
Author: Nicu Theodor Alexandru <nicu.andru@yahoo.com>
Date:   Sat Feb 20 11:03:40 2021 +0200

    Revert "Initial commit"
    
    This reverts commit b9e041d2f472d4cbe3dbc3b515a00d20955d38ab.

[1mdiff --git a/.gitattributes b/.gitattributes[m
[1mdeleted file mode 100644[m
[1mindex c8cb4a6..0000000[m
[1m--- a/.gitattributes[m
[1m+++ /dev/null[m
[36m@@ -1,86 +0,0 @@[m
[31m-* text=auto[m
[31m-[m
[31m-# Unity files[m
[31m-*.meta -text merge=unityyamlmerge diff[m
[31m-*.unity -text merge=unityyamlmerge diff[m
[31m-*.asset -text merge=unityyamlmerge diff[m
[31m-*.prefab -text merge=unityyamlmerge diff[m
[31m-*.mat -text merge=unityyamlmerge diff[m
[31m-*.anim -text merge=unityyamlmerge diff[m
[31m-*.controller -text merge=unityyamlmerge diff[m
[31m-*.overrideController -text merge=unityyamlmerge diff[m
[31m-*.physicMaterial -text merge=unityyamlmerge diff[m
[31m-*.physicsMaterial2D -text merge=unityyamlmerge diff[m
[31m-*.playable -text merge=unityyamlmerge diff[m
[31m-*.mask -text merge=unityyamlmerge diff[m
[31m-*.brush -text merge=unityyamlmerge diff[m
[31m-*.flare -text merge=unityyamlmerge diff[m
[31m-*.fontsettings -text merge=unityyamlmerge diff[m
[31m-*.guiskin -text merge=unityyamlmerge diff[m
[31m-*.giparams -text merge=unityyamlmerge diff[m
[31m-*.renderTexture -text merge=unityyamlmerge diff[m
[31m-*.spriteatlas -text merge=unityyamlmerge diff[m
[31m-*.terrainlayer -text merge=unityyamlmerge diff[m
[31m-*.mixer -text merge=unityyamlmerge diff[m
[31m-*.shadervariants -text merge=unityyamlmerge diff[m
[31m-[m
[31m-# Image formats[m
[31m-*.psd filter=lfs diff=lfs merge=lfs -text[m
[31m-*.jpg filter=lfs diff=lfs merge=lfs -text[m
[31m-*.png filter=lfs diff=lfs merge=lfs -text[m
[31m-*.gif filter=lfs diff=lfs merge=lfs -text[m
[31m-*.bmp filter=lfs diff=lfs merge=lfs -text[m
[31m-*.tga filter=lfs diff=lfs merge=lfs -text[m
[31m-*.tiff filter=lfs diff=lfs merge=lfs -text[m
[31m-*.tif filter=lfs diff=lfs merge=lfs -text[m
[31m-*.iff filter=lfs diff=lfs merge=lfs -text[m
[31m-*.pict filter=lfs diff=lfs merge=lfs -text[m
[31m-*.dds filter=lfs diff=lfs merge=lfs -text[m
[31m-*.xcf filter=lfs diff=lfs merge=lfs -text[m
[31m-[m
[31m-# Audio formats[m
[31m-*.mp3 filter=lfs diff=lfs merge=lfs -text[m
[31m-*.ogg filter=lfs diff=lfs merge=lfs -text[m
[31m-*.wav filter=lfs diff=lfs merge=lfs -text[m
[31m-*.aiff filter=lfs diff=lfs merge=lfs -text[m
[31m-*.aif filter=lfs diff=lfs merge=lfs -text[m
[31m-*.mod filter=lfs diff=lfs merge=lfs -text[m
[31m-*.it filter=lfs diff=lfs merge=lfs -text[m
[31m-*.s3m filter=lfs diff=lfs merge=lfs -text[m
[31m-*.xm filter=lfs diff=lfs merge=lfs -text[m
[31m-[m
[31m-# Video formats[m
[31m-*.mov filter=lfs diff=lfs merge=lfs -text[m
[31m-*.avi filter=lfs diff=lfs merge=lfs -text[m
[31m-*.asf filter=lfs diff=lfs merge=lfs -text[m
[31m-*.mpg filter=lfs diff=lfs merge=lfs -text[m
[31m-*.mpeg filter=lfs diff=lfs merge=lfs -text[m
[31m-*.mp4 filter=lfs diff=lfs merge=lfs -text[m
[31m-[m
[31m-# 3D formats[m
[31m-*.fbx filter=lfs diff=lfs merge=lfs -text[m
[31m-*.obj filter=lfs diff=lfs merge=lfs -text[m
[31m-*.max filter=lfs diff=lfs merge=lfs -text[m
[31m-*.blend filter=lfs diff=lfs merge=lfs -text[m
[31m-*.dae filter=lfs diff=lfs merge=lfs -text[m
[31m-*.mb filter=lfs diff=lfs merge=lfs -text[m
[31m-*.ma filter=lfs diff=lfs merge=lfs -text[m
[31m-*.3ds filter=lfs diff=lfs merge=lfs -text[m
[31m-*.dfx filter=lfs diff=lfs merge=lfs -text[m
[31m-*.c4d filter=lfs diff=lfs merge=lfs -text[m
[31m-*.lwo filter=lfs diff=lfs merge=lfs -text[m
[31m-*.lwo2 filter=lfs diff=lfs merge=lfs -text[m
[31m-*.abc filter=lfs diff=lfs merge=lfs -text[m
[31m-*.3dm filter=lfs diff=lfs merge=lfs -text[m
[31m-[m
[31m-# Build[m
[31m-*.dll filter=lfs diff=lfs merge=lfs -text[m
[31m-*.pdb filter=lfs diff=lfs merge=lfs -text[m
[31m-*.mdb filter=lfs diff=lfs merge=lfs -text[m
[31m-[m
[31m-# Packaging[m
[31m-*.zip filter=lfs diff=lfs merge=lfs -text[m
[31m-*.7z filter=lfs diff=lfs merge=lfs -text[m
[31m-*.gz filter=lfs diff=lfs merge=lfs -text[m
[31m-*.rar filter=lfs diff=lfs merge=lfs -text[m
[31m-*.tar filter=lfs diff=lfs merge=lfs -text[m
[1mdiff --git a/.gitignore b/.gitignore[m
[1mdeleted file mode 100644[m
[1mindex 2538b1d..0000000[m
[1m--- a/.gitignore[m
[1m+++ /dev/null[m
[36m@@ -1,48 +0,0 @@[m
[31m-[Ll]ibrary/[m
[31m-[Tt]emp/[m
[31m-[Oo]bj/[m
[31m-[Bb]uild/[m
[31m-[Bb]uilds/[m
[31m-[Ll]ogs/[m
[31m-[m
[31m-# Uncomment this line if you wish to ignore the asset store tools plugin[m
[31m-# [Aa]ssets/AssetStoreTools*[m
[31m-[m
[31m-# Visual Studio cache directory[m
[31m-.vs/[m
[31m-[m
[31m-# Gradle cache directory[m
[31m-.gradle/[m
[31m-[m
[31m-# Autogenerated VS/MD/Consulo solution and project files[m
[31m-ExportedObj/[m
[31m-.consulo/[m
[31m-*.csproj[m
[31m-*.unityproj[m
[31m-*.sln[m
[31m-*.suo[m
[31m-*.tmp[m
[31m-*.user[m
[31m-*.userprefs[m
[31m-*.pidb[m
[31m-*.booproj[m
[31m-*.svd[m
[31m-*.pdb[m
[31m-*.mdb[m
[31m-*.opendb[m
[31m-*.VC.db[m
[31m-[m
[31m-# Unity3D generated meta files[m
[31m-*.pidb.meta[m
[31m-*.pdb.meta[m
[31m-*.mdb.meta[m
[31m-[m
[31m-# Unity3D generated file on crash reports[m
[31m-sysinfo.txt[m
[31m-[m
[31m-# Builds[m
[31m-*.apk[m
[31m-*.unitypackage[m
[31m-[m
[31m-# Crashlytics generated file[m
[31m-crashlytics-build.properties[m
[1mdiff --git a/Assets/.gitignore b/Assets/.gitignore[m
[1mdeleted file mode 100644[m
[1mindex e69de29..0000000[m
