﻿using System;
using UniGLTF;

namespace VCI
{
    [Serializable]
    public sealed class glTFCubemapFaceTextureInfo : glTFTextureInfo
    {
        public override glTFTextureTypes TextureType => glTFTextureTypes.Unknown;

        public glTFCubemapFaceTextureInfo()
        {
        }
    }
}
