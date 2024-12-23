using Exiled.API.Features;

namespace SCP1162;

public class Plugin : Plugin<Config> {
  public override string Name => "SCP1162";
  public override string Author => "xRoier, furry";
  private EventHandlers EventHandlers => new(this);
  public override Version Version => new(9, 0, 0);
  public override Version RequiredExiledVersion => new(9, 0, 0);

  public override void OnEnabled() {
    Exiled.Events.Handlers.Player.DroppingItem += EventHandlers.OnItemDropped;
    base.OnEnabled();
  }

  public override void OnDisabled() {
    Exiled.Events.Handlers.Player.DroppingItem -= EventHandlers.OnItemDropped;
    base.OnDisabled();
  }
}