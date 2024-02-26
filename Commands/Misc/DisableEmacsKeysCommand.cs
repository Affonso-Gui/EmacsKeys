using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Text.Formatting;
using System.ComponentModel.Composition;

namespace Microsoft.VisualStudio.Editor.EmacsEmulation.Commands
{
    /// <summary>
    /// This command disables or enables the EmacsKeys extension.
    /// When the extension is disabled, no commands other than
    /// this one are recognized.
    /// 
    /// Keys:
    /// </summary>
    [EmacsCommand(EmacsCommandID.DisableEmacsKeys)]
    internal class DisableEmacsKeysCommand : EmacsCommand
    {
        internal override void Execute(EmacsCommandContext context)
        {
            context.Manager.Disable();
        }
    }
}
