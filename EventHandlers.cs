using UnityEngine;
using Exiled.API.Extensions;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using SCP1162.API;
using SCP1162;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

namespace SCP1162;

public static class EventHandlers {
  public static void OnItemDropped(DroppingItemEventArgs ev) {
    if (Plugin.Instance == null) return;
    if (!ev.IsAllowed) return;
    if (!(Vector3.Distance(ev.Player.Position, RoleTypeId.Scp173.GetRandomSpawnLocation().Position) <= 8.2f)) return;
    if (Plugin.Instance.Config.UseHints)
     ev.Player.ShowHint(Plugin.Instance.Config.ItemDropMessage, Plugin.Instance.Config.ItemDropMessageDuration);
    else
      ev.Player.Broadcast(Plugin.Instance.Config.ItemDropMessageDuration, Plugin.Instance.Config.ItemDropMessage, Broadcast.BroadcastFlags.Normal, true);
    ev.IsAllowed = false;
    var oldItem = ev.Item.Base.ItemTypeId;
    ev.Player.RemoveItem(ev.Item);
    var newItemType = Plugin.Instance.Config.Chances[Random.Range(0, Plugin.Instance.Config.Chances.Count)];
    var eventArgs = new UsingScp1162EventArgs(ev.Player, newItemType, oldItem);
    Scp1162Event.OnUsingScp1162(eventArgs);
    var newItem = Item.Create(eventArgs.ItemAfter);
    ev.Player.AddItem(newItem);
    ev.Player.DropItem(newItem);
  }
}