// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Coverlet.Core.Helpers
{
  internal class InstrumentationOptions
  {
    public bool RestoreModules { get; set; } = true;
    public bool SkipInstrumentModules { get; set; }
    public bool InstrumentOnly { get; set; }

    public static InstrumentationOptions Default => new()
    {
      RestoreModules = true, SkipInstrumentModules = false, InstrumentOnly = false
    };
  }
}
