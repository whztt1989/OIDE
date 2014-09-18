// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Trigger : Table {
  public static Trigger GetRootAsTrigger(ByteBuffer _bb, int offset) { return (new Trigger()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Trigger __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }


  public static void StartTrigger(FlatBufferBuilder builder) { builder.StartObject(0); }
  public static int EndTrigger(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
