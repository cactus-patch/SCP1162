namespace SCP1162;

using Exiled.API.Features;

public class Plugin : Plugin<Config> {
  public override string Name => "SCP1162";
  public override string Author => "xRoier, furry";
  private EventHandlers EventHandlers => new EventHandlers(this);
  public override Version Version => new Version(9, 0, 0);
  public override Version RequiredExiledVersion => new Version(9, 0, 0);

  public override void OnEnabled() {
    Exiled.Events.Handlers.Player.DroppingItem += EventHandlers.OnItemDropped;
    base.OnEnabled();
  }

  public override void OnDisabled() {
    Exiled.Events.Handlers.Player.DroppingItem -= EventHandlers.OnItemDropped;
    base.OnDisabled();
  }
}