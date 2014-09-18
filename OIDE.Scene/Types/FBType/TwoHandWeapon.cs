// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class TwoHandWeapon : Table {
  public static TwoHandWeapon GetRootAsTwoHandWeapon(ByteBuffer _bb, int offset) { return (new TwoHandWeapon()).__init(_bb.GetInt(offset) + offset, _bb); }
  public TwoHandWeapon __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Name() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public string Mesh() { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartTwoHandWeapon(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddName(FlatBufferBuilder builder, int nameOffset) { builder.AddOffset(0, nameOffset, 0); }
  public static void AddMesh(FlatBufferBuilder builder, int meshOffset) { builder.AddOffset(1, meshOffset, 0); }
  public static int EndTwoHandWeapon(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
