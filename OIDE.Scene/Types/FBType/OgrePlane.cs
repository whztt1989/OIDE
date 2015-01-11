// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class OgrePlane : Table {
  public static OgrePlane GetRootAsOgrePlane(ByteBuffer _bb) { return (new OgrePlane()).__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb); }
  public OgrePlane __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Vec3f Normal() { return Normal(new Vec3f()); }
  public Vec3f Normal(Vec3f obj) { int o = __offset(4); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public float Constant() { int o = __offset(6); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float Width() { int o = __offset(8); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float Height() { int o = __offset(10); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public uint Xsegments() { int o = __offset(12); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }
  public uint Ysegments() { int o = __offset(14); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }
  public byte Normals() { int o = __offset(16); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; }
  public uint NumTexCoordSets() { int o = __offset(18); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }
  public float XTile() { int o = __offset(20); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float YTile() { int o = __offset(22); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public Vec3f UpVector() { return UpVector(new Vec3f()); }
  public Vec3f UpVector(Vec3f obj) { int o = __offset(24); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }

  public static void StartOgrePlane(FlatBufferBuilder builder) { builder.StartObject(11); }
  public static void AddNormal(FlatBufferBuilder builder, int normalOffset) { builder.AddStruct(0, normalOffset, 0); }
  public static void AddConstant(FlatBufferBuilder builder, float constant) { builder.AddFloat(1, constant, 0); }
  public static void AddWidth(FlatBufferBuilder builder, float width) { builder.AddFloat(2, width, 0); }
  public static void AddHeight(FlatBufferBuilder builder, float height) { builder.AddFloat(3, height, 0); }
  public static void AddXsegments(FlatBufferBuilder builder, uint xsegments) { builder.AddUint(4, xsegments, 0); }
  public static void AddYsegments(FlatBufferBuilder builder, uint ysegments) { builder.AddUint(5, ysegments, 0); }
  public static void AddNormals(FlatBufferBuilder builder, byte normals) { builder.AddByte(6, normals, 0); }
  public static void AddNumTexCoordSets(FlatBufferBuilder builder, uint numTexCoordSets) { builder.AddUint(7, numTexCoordSets, 0); }
  public static void AddXTile(FlatBufferBuilder builder, float xTile) { builder.AddFloat(8, xTile, 0); }
  public static void AddYTile(FlatBufferBuilder builder, float yTile) { builder.AddFloat(9, yTile, 0); }
  public static void AddUpVector(FlatBufferBuilder builder, int upVectorOffset) { builder.AddStruct(10, upVectorOffset, 0); }
  public static int EndOgrePlane(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
