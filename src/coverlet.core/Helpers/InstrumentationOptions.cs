// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Coverlet.Core.Helpers
{
  internal class InstrumentationOptions
  {
    /// <summary>
    /// A flag to indicate whether modules should be restored at the end of a run.
    /// This is controlled by either <see cref="InstrumentOnly"/> or <see cref="SkipInstrumentModules"/>.
    /// </summary>
    public bool SkipRestoreModules
    {
      get
      {
        if (InstrumentOnly)
        {
          return true;
        }

        if (SkipInstrumentModules)
        {
          return true;
        }

        return false;
      }
    }

    public bool SkipInstrumentModules { get; set; }
    public bool InstrumentOnly { get; set; }

    public static InstrumentationOptions Default => new()
    {
      SkipInstrumentModules = false, InstrumentOnly = false
    };
  }
}
