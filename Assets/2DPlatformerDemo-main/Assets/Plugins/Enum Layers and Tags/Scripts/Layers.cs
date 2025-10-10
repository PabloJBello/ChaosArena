public static class Layers
{
    public const int Default = 0;
    public const int TransparentFX = 1;
    public const int IgnoreSCRaycast = 2;
    public const int Water = 3;
    public const int UI = 4;

    public static int ToLayerMask(int layer)
    {
        return 1 << layer;
    }
}