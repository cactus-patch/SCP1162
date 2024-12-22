﻿namespace SCP1162.API;

using Exiled.Events.Features;

public static class Scp1162Event {
  public static event CustomEventHandler<UsingScp1162EventArgs>? UsingScp1162;
  public static void OnUsingScp1162(UsingScp1162EventArgs ev) => UsingScp1162?.Invoke(ev);
}