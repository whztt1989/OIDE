// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class EntityBase : Table {
  public static EntityBase GetRootAsEntityBase(ByteBuffer _bb) { return (new EntityBase()).__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb); }
  public EntityBase __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Mesh Meshes(int j) { return Meshes(new Mesh(), j); }
  public Mesh Meshes(Mesh obj, int j) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int MeshesLength() { int o = __offset(4); return o != 0 ? __vector_len(o) : 0; }
  public Sound Sounds(int j) { return Sounds(new Sound(), j); }
  public Sound Sounds(Sound obj, int j) { int o = __offset(6); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int SoundsLength() { int o = __offset(6); return o != 0 ? __vector_len(o) : 0; }
  public Material Materials(int j) { return Materials(new Material(), j); }
  public Material Materials(Material obj, int j) { int o = __offset(8); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int MaterialsLength() { int o = __offset(8); return o != 0 ? __vector_len(o) : 0; }
  public PhysicsObject Physics(int j) { return Physics(new PhysicsObject(), j); }
  public PhysicsObject Physics(PhysicsObject obj, int j) { int o = __offset(10); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int PhysicsLength() { int o = __offset(10); return o != 0 ? __vector_len(o) : 0; }
  public ushort Type() { int o = __offset(12); return o != 0 ? bb.GetUshort(o + bb_pos) : (ushort)0; }
  public string Boneparent() { int o = __offset(14); return o != 0 ? __string(o + bb_pos) : null; }
  public uint Mode() { int o = __offset(16); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }
  public byte CastShadows() { int o = __offset(18); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; }
  public Debug Debug() { return Debug(new Debug()); }
  public Debug Debug(Debug obj) { int o = __offset(20); return o != 0 ? obj.__init(__indirect(o + bb_pos), bb) : null; }
  public string AnimationTree() { int o = __offset(22); return o != 0 ? __string(o + bb_pos) : null; }
  public string AnimationInfo() { int o = __offset(24); return o != 0 ? __string(o + bb_pos) : null; }

  public static int CreateEntityBase(FlatBufferBuilder builder,
      int meshes = 0,
      int sounds = 0,
      int materials = 0,
      int physics = 0,
      ushort type = 0,
      int boneparent = 0,
      uint mode = 0,
      byte castShadows = 0,
      int debug = 0,
      int animationTree = 0,
      int animationInfo = 0) {
    builder.StartObject(11);
    EntityBase.AddAnimationInfo(builder, animationInfo);
    EntityBase.AddAnimationTree(builder, animationTree);
    EntityBase.AddDebug(builder, debug);
    EntityBase.AddMode(builder, mode);
    EntityBase.AddBoneparent(builder, boneparent);
    EntityBase.AddPhysics(builder, physics);
    EntityBase.AddMaterials(builder, materials);
    EntityBase.AddSounds(builder, sounds);
    EntityBase.AddMeshes(builder, meshes);
    EntityBase.AddType(builder, type);
    EntityBase.AddCastShadows(builder, castShadows);
    return EntityBase.EndEntityBase(builder);
  }

  public static void StartEntityBase(FlatBufferBuilder builder) { builder.StartObject(11); }
  public static void AddMeshes(FlatBufferBuilder builder, int meshesOffset) { builder.AddOffset(0, meshesOffset, 0); }
  public static int CreateMeshesVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static void StartMeshesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddSounds(FlatBufferBuilder builder, int soundsOffset) { builder.AddOffset(1, soundsOffset, 0); }
  public static int CreateSoundsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static void StartSoundsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddMaterials(FlatBufferBuilder builder, int materialsOffset) { builder.AddOffset(2, materialsOffset, 0); }
  public static int CreateMaterialsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static void StartMaterialsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddPhysics(FlatBufferBuilder builder, int physicsOffset) { builder.AddOffset(3, physicsOffset, 0); }
  public static int CreatePhysicsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static void StartPhysicsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddType(FlatBufferBuilder builder, ushort type) { builder.AddUshort(4, type, 0); }
  public static void AddBoneparent(FlatBufferBuilder builder, int boneparentOffset) { builder.AddOffset(5, boneparentOffset, 0); }
  public static void AddMode(FlatBufferBuilder builder, uint mode) { builder.AddUint(6, mode, 0); }
  public static void AddCastShadows(FlatBufferBuilder builder, byte castShadows) { builder.AddByte(7, castShadows, 0); }
  public static void AddDebug(FlatBufferBuilder builder, int debugOffset) { builder.AddOffset(8, debugOffset, 0); }
  public static void AddAnimationTree(FlatBufferBuilder builder, int animationTreeOffset) { builder.AddOffset(9, animationTreeOffset, 0); }
  public static void AddAnimationInfo(FlatBufferBuilder builder, int animationInfoOffset) { builder.AddOffset(10, animationInfoOffset, 0); }
  public static int EndEntityBase(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
