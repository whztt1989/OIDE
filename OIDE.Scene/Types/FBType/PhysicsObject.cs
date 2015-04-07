// automatically generated, do not modify

namespace XFBType
{

using FlatBuffers;

public class PhysicsObject : Table {
  public static PhysicsObject GetRootAsPhysicsObject(ByteBuffer _bb) { return GetRootAsPhysicsObject(_bb, new PhysicsObject()); }
  public static PhysicsObject GetRootAsPhysicsObject(ByteBuffer _bb, PhysicsObject obj) { return (obj.__init(_bb.GetInt(_bb.position()) + _bb.position(), _bb)); }
  public PhysicsObject __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public short ColMask() { int o = __offset(4); return o != 0 ? bb.GetShort(o + bb_pos) : (short)0; }
  public sbyte Type() { int o = __offset(6); return o != 0 ? bb.GetSbyte(o + bb_pos) : (sbyte)0; }
  public uint Mode() { int o = __offset(8); return o != 0 ? bb.GetUint(o + bb_pos) : (uint)0; }
  public sbyte Shape() { int o = __offset(10); return o != 0 ? bb.GetSbyte(o + bb_pos) : (sbyte)0; }
  public float Mass() { int o = __offset(12); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float Margin() { int o = __offset(14); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float Radius() { int o = __offset(16); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float AngularDamp() { int o = __offset(18); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float LinearDamp() { int o = __offset(20); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float FormFactor() { int o = __offset(22); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float MinVel() { int o = __offset(24); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float MaxVel() { int o = __offset(26); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float Restitution() { int o = __offset(28); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float Friction() { int o = __offset(30); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public short ColGroupMask() { int o = __offset(32); return o != 0 ? bb.GetShort(o + bb_pos) : (short)0; }
  public float CharStepHeight() { int o = __offset(34); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float CharJumpSpeed() { int o = __offset(36); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public float CharFallSpeed() { int o = __offset(38); return o != 0 ? bb.GetFloat(o + bb_pos) : (float)0; }
  public string Boneparent() { int o = __offset(40); return o != 0 ? __string(o + bb_pos) : null; }
  public string CollMeshName() { int o = __offset(42); return o != 0 ? __string(o + bb_pos) : null; }
  public Vec3f Size() { return Size(new Vec3f()); }
  public Vec3f Size(Vec3f obj) { int o = __offset(44); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public Vec3f Scale() { return Scale(new Vec3f()); }
  public Vec3f Scale(Vec3f obj) { int o = __offset(46); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public Vec3f Offset() { return Offset(new Vec3f()); }
  public Vec3f Offset(Vec3f obj) { int o = __offset(48); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public bool ParentIsNode() { int o = __offset(50); return o != 0 ? 0!=bb.Get(o + bb_pos) : (bool)false; }
  public PhysicsConstraint PhysicsConstraints(int j) { return PhysicsConstraints(new PhysicsConstraint(), j); }
  public PhysicsConstraint PhysicsConstraints(PhysicsConstraint obj, int j) { int o = __offset(52); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int PhysicsConstraintsLength() { int o = __offset(52); return o != 0 ? __vector_len(o) : 0; }

  public static void StartPhysicsObject(FlatBufferBuilder builder) { builder.StartObject(25); }
  public static void AddColMask(FlatBufferBuilder builder, short colMask) { builder.AddShort(0, colMask, 0); }
  public static void AddType(FlatBufferBuilder builder, sbyte type) { builder.AddSbyte(1, type, 0); }
  public static void AddMode(FlatBufferBuilder builder, uint mode) { builder.AddUint(2, mode, 0); }
  public static void AddShape(FlatBufferBuilder builder, sbyte shape) { builder.AddSbyte(3, shape, 0); }
  public static void AddMass(FlatBufferBuilder builder, float mass) { builder.AddFloat(4, mass, 0); }
  public static void AddMargin(FlatBufferBuilder builder, float margin) { builder.AddFloat(5, margin, 0); }
  public static void AddRadius(FlatBufferBuilder builder, float radius) { builder.AddFloat(6, radius, 0); }
  public static void AddAngularDamp(FlatBufferBuilder builder, float angularDamp) { builder.AddFloat(7, angularDamp, 0); }
  public static void AddLinearDamp(FlatBufferBuilder builder, float linearDamp) { builder.AddFloat(8, linearDamp, 0); }
  public static void AddFormFactor(FlatBufferBuilder builder, float formFactor) { builder.AddFloat(9, formFactor, 0); }
  public static void AddMinVel(FlatBufferBuilder builder, float minVel) { builder.AddFloat(10, minVel, 0); }
  public static void AddMaxVel(FlatBufferBuilder builder, float maxVel) { builder.AddFloat(11, maxVel, 0); }
  public static void AddRestitution(FlatBufferBuilder builder, float restitution) { builder.AddFloat(12, restitution, 0); }
  public static void AddFriction(FlatBufferBuilder builder, float friction) { builder.AddFloat(13, friction, 0); }
  public static void AddColGroupMask(FlatBufferBuilder builder, short colGroupMask) { builder.AddShort(14, colGroupMask, 0); }
  public static void AddCharStepHeight(FlatBufferBuilder builder, float charStepHeight) { builder.AddFloat(15, charStepHeight, 0); }
  public static void AddCharJumpSpeed(FlatBufferBuilder builder, float charJumpSpeed) { builder.AddFloat(16, charJumpSpeed, 0); }
  public static void AddCharFallSpeed(FlatBufferBuilder builder, float charFallSpeed) { builder.AddFloat(17, charFallSpeed, 0); }
  public static void AddBoneparent(FlatBufferBuilder builder, int boneparentOffset) { builder.AddOffset(18, boneparentOffset, 0); }
  public static void AddCollMeshName(FlatBufferBuilder builder, int collMeshNameOffset) { builder.AddOffset(19, collMeshNameOffset, 0); }
  public static void AddSize(FlatBufferBuilder builder, int sizeOffset) { builder.AddStruct(20, sizeOffset, 0); }
  public static void AddScale(FlatBufferBuilder builder, int scaleOffset) { builder.AddStruct(21, scaleOffset, 0); }
  public static void AddOffset(FlatBufferBuilder builder, int offsetOffset) { builder.AddStruct(22, offsetOffset, 0); }
  public static void AddParentIsNode(FlatBufferBuilder builder, bool parentIsNode) { builder.AddBool(23, parentIsNode, false); }
  public static void AddPhysicsConstraints(FlatBufferBuilder builder, int physicsConstraintsOffset) { builder.AddOffset(24, physicsConstraintsOffset, 0); }
  public static int CreatePhysicsConstraintsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static void StartPhysicsConstraintsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static int EndPhysicsObject(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return o;
  }
};


}
