// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class AI : Table {
  public static AI GetRootAsAI(ByteBuffer _bb, int offset) { return (new AI()).__init(_bb.GetInt(offset) + offset, _bb); }
  public AI __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string ScriptName() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }

  public static void StartAI(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddScriptName(FlatBufferBuilder builder, int scriptNameOffset) { builder.AddOffset(0, scriptNameOffset, 0); }
  public static int EndAI(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
