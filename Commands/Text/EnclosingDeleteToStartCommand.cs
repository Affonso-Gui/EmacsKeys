﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Text.Formatting;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;

namespace Microsoft.VisualStudio.Editor.EmacsEmulation.Commands
{
    /// <summary>
    /// This command kills from point to location backward-sexp would place the caret, including the prefix arg handling of forward-word.
    ///
    /// Keys: Ctrl+Alt+Bkspace | Ctrl+Alt+Del
    /// </summary>
    [EmacsCommand(EmacsCommandID.EnclosingDeleteToStart, IsKillCommand = true, InverseCommand = EmacsCommandID.EnclosingDeleteToEnd, UndoName = "Cut")]
    internal class EnclosingDeleteToStartCommand : EmacsCommand
    {
        internal override void Execute(EmacsCommandContext context)
        {
            var caretPosition = context.TextView.GetCaretPosition();
            var position = caretPosition;

            for (var counter = context.Manager.GetUniversalArgumentOrDefault(1); counter > 0; counter--)
            {
                position = context.EditorOperations.GetPreviousEnclosing(position, context.TextStructureNavigator);
            }

            // Sometimes the start of the enclosing may be marked with spaces or newlines
            // Move it until the first non-whitespace character.
            position = context.EditorOperations.GetNextNonWhiteSpaceCharacter(position);

            if (position != caretPosition)
            {
                context.EditorOperations.Delete(position, caretPosition - position);
                context.TextView.Caret.EnsureVisible();
            }
        }
    }
}