// automatically generated, do not modify

namespace FBType
{

using FlatBuffers;

public class OSysTypeData : Table {
  public static OSysTypeData GetRootAsOSysTypeData(ByteBuffer _bb, int offset) { return (new OSysTypeData()).__init(_bb.GetInt(offset) + offset, _bb); }
  public OSysTypeData __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public OgrePlane Plane() { return Plane(new OgrePlane()); }
  public OgrePlane Plane(OgrePlane obj) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public OgreCube Cube() { return Cube(new OgreCube()); }
  public OgreCube Cube(OgreCube obj) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static void StartOSysTypeData(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddPlane(FlatBufferBuilder builder, int planeOffset) { builder.AddOffset(0, planeOffset, 0); }
  public static void AddCube(FlatBufferBuilder builder, int cubeOffset) { builder.AddOffset(1, cubeOffset, 0); }
  public static int EndOSysTypeData(FlatBufferBuilder builder) { return builder.EndObject(); }
};


}
