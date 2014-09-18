// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class Node : Table {
  public static Node GetRootAsNode(ByteBuffer _bb, int offset) { return (new Node()).__init(_bb.GetInt(offset) + offset, _bb); }
  public Node __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public TransformStateData Transform() { return Transform(new TransformStateData()); }
  public TransformStateData Transform(TransformStateData obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public byte Visible() { int o = __offset(6); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; }
  public byte Enabled() { int o = __offset(8); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; }

  public static void StartNode(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddTransform(FlatBufferBuilder builder, int transformOffset) { builder.AddOffset(0, transformOffset, 0); }
  public static void AddVisible(FlatBufferBuilder builder, byte visible) { builder.AddByte(1, visible, 0); }
  public static void AddEnabled(FlatBufferBuilder builder, byte enabled) { builder.AddByte(2, enabled, 0); }
  public static int EndNode(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
