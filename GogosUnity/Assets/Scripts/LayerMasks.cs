using UnityEngine;

namespace Gogos
{
    public enum Layer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        PostProcessing = 8,
        Trigger = 9,
        Triggerable = 10,
    }

    public static class LayerMasks
    {
        public static readonly LayerMask Default = GetMask(Layer.Default);
        public static readonly LayerMask TransparentFX = GetMask(Layer.TransparentFX);
        public static readonly LayerMask IgnoreRaycast = GetMask(Layer.IgnoreRaycast);
        public static readonly LayerMask Water = GetMask(Layer.Water);
        public static readonly LayerMask UI = GetMask(Layer.UI);
        public static readonly LayerMask PostProcessing = GetMask(Layer.PostProcessing);
        public static readonly LayerMask Trigger = GetMask(Layer.Trigger);
        public static readonly LayerMask Triggerable = GetMask(Layer.Triggerable);

        private static LayerMask GetMask(Layer layer)
        {
            return LayerMask.GetMask(layer.ToString());
        }
    }
}
