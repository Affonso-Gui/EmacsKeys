﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Text.Formatting;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace Microsoft.VisualStudio.Editor.EmacsEmulation.Commands
{
    /// <summary>
    /// This command is the opposite of scroll-down-command.
    /// 
    /// Keys: PgUp | Alt+V
    /// </summary>
    [EmacsCommand(EmacsCommandID.ScrollPageUp, InverseCommand = EmacsCommandID.ScrollPageDown)]
    internal class ScrollPageUpCommand : EmacsCommand
    {
        internal override void Execute(EmacsCommandContext context)
        {
            var caret = context.TextView.Caret;
            var firstLine = context.TextView.GetFirstSufficientlyVisibleLine();

            // number of lines in the current view that should remain visible after scrolling
            int visibleLines = 2;

            if (context.TextBuffer.GetLineNumber(firstLine.Start) == 0 &&
                !context.Manager.UniversalArgument.HasValue)
            {
                // Beginning of buffer reached. Do nothing
                context.Manager.UpdateStatus("Beginning of buffer");
                return;
            }

            if (context.Manager.UniversalArgument.HasValue)
            {
                // Scroll up the designated number of lines
                context.TextView.DisplayTextLineContainingBufferPosition(
                    firstLine.Start,
                    context.TextView.LineHeight * context.Manager.GetUniversalArgumentOrDefault(0),
                    ViewRelativePosition.Top);
            }
            else
            {
                // Scroll up while keeping some overlapping lines
                context.TextView.DisplayTextLineContainingBufferPosition(
                    firstLine.Start,
                    context.TextView.LineHeight * Math.Max(0, visibleLines - 1),
                    ViewRelativePosition.Bottom);
            }

            var lastLine = context.TextView.GetLastSufficientlyVisibleLine();
            if (caret.ContainingTextViewLine.Start <= lastLine.Start)
            {
                // Caret is in the viewport. Leave it as is
                return;
            }

            // Move the caret to the bottom line
            context.EditorOperations.MoveCaret(lastLine.Start);
        }
    }
}
