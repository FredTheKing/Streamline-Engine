using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Node;

public class Folder : UuidIdentifier, IFolder<object>, IScript
{
  public string Name { get; set; }
  public bool Active { get; set; } = true;
  public FolderNodeType Type { get; set; }
  public List<dynamic>? Parent { get; set; } = [];
  public List<dynamic>? Children { get; set; }
  
  public Folder(string name, FolderNodeType type, params dynamic[] children) {
    Name = name;
    Type = type;
    Children = children.ToList();
  }

  public void Init(Context context)
  {
    foreach (dynamic child in Children ?? [])
    {
      child.Init(context);
      child.Parent.Add(this);
    }
  }
  public void Enter(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
      child.Enter(context);
  }
  public void Leave(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
      child.Leave(context);
  }
  public void EarlyUpdate(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
      child.EarlyUpdate(context);
  }
  public void Update(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
      child.Update(context);
  }
  public void LateUpdate(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
      child.LateUpdate(context);
  }
  public void PreDraw(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
      child.PreDraw(context);
  }
  public void Draw(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
      child.Draw(context);
  }
}