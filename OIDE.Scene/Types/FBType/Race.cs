// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Race : Table {
  public static Race GetRootAsRace(ByteBuffer _bb, int offset) { return (new Race()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Race __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Name() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public string Description() { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartRace(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddName(FlatBufferBuilder builder, int nameOffset) { builder.AddOffset(0, nameOffset, 0); }
  public static void AddDescription(FlatBufferBuilder builder, int descriptionOffset) { builder.AddOffset(1, descriptionOffset, 0); }
  public static int EndRace(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
