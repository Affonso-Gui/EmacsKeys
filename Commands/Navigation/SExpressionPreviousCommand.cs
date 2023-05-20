﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Text.Formatting;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;

namespace Microsoft.VisualStudio.Editor.EmacsEmulation.Commands
{
    /// <summary>
    /// This command moves backward across one balanced expression (s-expression).
    /// With a prefix arg, the command moves backward that many times, but if prefix arg is negative, it goes forward that many.
    ///
    /// Keys: Ctrl+Alt+Left Arrow | Ctrl+Alt+B
    /// </summary>
    [EmacsCommand(EmacsCommandID.SExpressionPrevious, CanBeRepeated = true, InverseCommand = EmacsCommandID.SExpressionNext)]
    internal class SExpressionPreviousCommand : EmacsCommand
    {
        internal override void Execute(EmacsCommandContext context)
        {
            // TODO: implement s-expression search
            var word = context.TextStructureNavigator.GetPreviousWord(context.TextView);

            if (word.HasValue)
            {
                context.EditorOperations.MoveCaret(word.Value.Start);
            }
        }
    }
}
