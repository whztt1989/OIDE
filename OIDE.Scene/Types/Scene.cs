//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Scene.proto
namespace ProtoType
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Vec3f")]
  public partial class Vec3f : global::ProtoBuf.IExtensible
  {
    public Vec3f() {}
    
    private float _x = (float)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float x
    {
      get { return _x; }
      set { _x = value; }
    }
    private float _y = (float)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float y
    {
      get { return _y; }
      set { _y = value; }
    }
    private float _z = (float)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float z
    {
      get { return _z; }
      set { _z = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Quat4f")]
  public partial class Quat4f : global::ProtoBuf.IExtensible
  {
    public Quat4f() {}
    
    private float _w = (float)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"w", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float w
    {
      get { return _w; }
      set { _w = value; }
    }
    private float _x = (float)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float x
    {
      get { return _x; }
      set { _x = value; }
    }
    private float _y = (float)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float y
    {
      get { return _y; }
      set { _y = value; }
    }
    private float _z = (float)0;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float z
    {
      get { return _z; }
      set { _z = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TransformStateData")]
  public partial class TransformStateData : global::ProtoBuf.IExtensible
  {
    public TransformStateData() {}
    
    private ProtoType.Quat4f _rot = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"rot", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ProtoType.Quat4f rot
    {
      get { return _rot; }
      set { _rot = value; }
    }
    private ProtoType.Vec3f _loc = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"loc", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ProtoType.Vec3f loc
    {
      get { return _loc; }
      set { _loc = value; }
    }
    private ProtoType.Vec3f _scl = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"scl", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ProtoType.Vec3f scl
    {
      get { return _scl; }
      set { _scl = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Colour")]
  public partial class Colour : global::ProtoBuf.IExtensible
  {
    public Colour() {}
    
    private float _r = (float)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"r", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float r
    {
      get { return _r; }
      set { _r = value; }
    }
    private float _g = (float)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"g", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float g
    {
      get { return _g; }
      set { _g = value; }
    }
    private float _b = (float)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"b", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float b
    {
      get { return _b; }
      set { _b = value; }
    }
    private float _a = (float)0;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"a", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float a
    {
      get { return _a; }
      set { _a = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Clipping")]
  public partial class Clipping : global::ProtoBuf.IExtensible
  {
    public Clipping() {}
    
    private float _near = (float)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"near", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float near
    {
      get { return _near; }
      set { _near = value; }
    }
    private float _far = (float)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"far", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float far
    {
      get { return _far; }
      set { _far = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Camera")]
  public partial class Camera : global::ProtoBuf.IExtensible
  {
    public Camera() {}
    
    private ProtoType.Clipping _clipping;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"clipping", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ProtoType.Clipping clipping
    {
      get { return _clipping; }
      set { _clipping = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SpawnPoint")]
  public partial class SpawnPoint : global::ProtoBuf.IExtensible
  {
    public SpawnPoint() {}
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Trigger")]
  public partial class Trigger : global::ProtoBuf.IExtensible
  {
    public Trigger() {}
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Light")]
  public partial class Light : global::ProtoBuf.IExtensible
  {
    public Light() {}
    
    private ProtoType.Colour _colourDiffuse;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"colourDiffuse", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ProtoType.Colour colourDiffuse
    {
      get { return _colourDiffuse; }
      set { _colourDiffuse = value; }
    }
    private ProtoType.Colour _colourSpecular;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"colourSpecular", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ProtoType.Colour colourSpecular
    {
      get { return _colourSpecular; }
      set { _colourSpecular = value; }
    }
    private ProtoType.Vec3f _directionVector;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"directionVector", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ProtoType.Vec3f directionVector
    {
      get { return _directionVector; }
      set { _directionVector = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Node")]
  public partial class Node : global::ProtoBuf.IExtensible
  {
    public Node() {}
    
    private ProtoType.TransformStateData _transform;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"transform", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ProtoType.TransformStateData transform
    {
      get { return _transform; }
      set { _transform = value; }
    }
    private bool _visible;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"visible", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool visible
    {
      get { return _visible; }
      set { _visible = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Scene")]
  public partial class Scene : global::ProtoBuf.IExtensible
  {
    public Scene() {}
    
    private ProtoType.Colour _colourAmbient;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"colourAmbient", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ProtoType.Colour colourAmbient
    {
      get { return _colourAmbient; }
      set { _colourAmbient = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"AddressType")]
    public enum AddressType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"StreetAddress", Value=0)]
      StreetAddress = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ZipCode", Value=1)]
      ZipCode = 1
    }
  
}