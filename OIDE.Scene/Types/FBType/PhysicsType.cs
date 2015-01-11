// automatically generated, do not modify

namespace XFBType
{

public class PhysicsType
{
  public static readonly sbyte PT_NO_COLLISION = 0;
  public static readonly sbyte PT_STATIC = 1;
  public static readonly sbyte PT_DYNAMIC = 2;
  public static readonly sbyte PT_RIGID = 3;
  public static readonly sbyte PT_SOFT = 4;
  public static readonly sbyte PT_SENSOR = 5;
  public static readonly sbyte PT_NAVMESH = 6;
  public static readonly sbyte PT_CHARACTER = 7;

  private static readonly string[] names = { "PT_NO_COLLISION", "PT_STATIC", "PT_DYNAMIC", "PT_RIGID", "PT_SOFT", "PT_SENSOR", "PT_NAVMESH", "PT_CHARACTER", };

  public static string Name(int e) { return names[e]; }
};


}
