// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class Mesh : Table {
  public static Mesh GetRootAsMesh(ByteBuffer _bb) { return GetRootAsMesh(_bb, new Mesh()); }
  public static Mesh GetRootAsMesh(ByteBuffer _bb, Mesh obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public Mesh __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public string Name() { int o = __offset(4); return o != 0 ? __string(o + bb_pos) : null; }
  public string RessGrp() { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; }
  public OgrePlane Plane() { return Plane(new OgrePlane()); }
  public OgrePlane Plane(OgrePlane obj) { int o = __offset(8); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public OgreCube Cube() { return Cube(new OgreCube()); }
  public OgreCube Cube(OgreCube obj) { int o = __offset(10); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }

  public static int CreateMesh(FlatBufferBuilder builder,
      int Name = 0,
      int RessGrp = 0,
      int plane = 0,
      int cube = 0) {
    builder.StartObject(4);
    Mesh.AddCube(builder, cube);
    Mesh.AddPlane(builder, plane);
    Mesh.AddRessGrp(builder, RessGrp);
    Mesh.AddName(builder, Name);
    return Mesh.EndMesh(builder);
  }

  public static void StartMesh(FlatBufferBuilder builder) { builder.StartObject(4); }
  public static void AddName(FlatBufferBuilder builder, int NameOffset) { builder.AddOffset(0, NameOffset, 0); }
  public static void AddRessGrp(FlatBufferBuilder builder, int RessGrpOffset) { builder.AddOffset(1, RessGrpOffset, 0); }
  public static void AddPlane(FlatBufferBuilder builder, int planeOffset) { builder.AddOffset(2, planeOffset, 0); }
  public static void AddCube(FlatBufferBuilder builder, int cubeOffset) { builder.AddOffset(3, cubeOffset, 0); }
  public static int EndMesh(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
