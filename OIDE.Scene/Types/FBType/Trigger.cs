// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Trigger : Table {
  public static Trigger GetRootAsTrigger(ByteBuffer _bb) { return (new Trigger()).__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb); }
  public Trigger __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }


  public static void StartTrigger(FlatBufferBuilder builder) { builder.StartObject(0); }
  public static int EndTrigger(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
