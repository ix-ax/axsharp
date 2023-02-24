﻿using Irony.Interpreter.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Pragmas;
using Irony;
using Irony.Parsing;
using Microsoft.VisualBasic.FileIO;

namespace Ix.Compiler.Cs.Pragmas.PragmaParser
{
    /// <summary>
    /// Provides entry for parsing and compiling pragma expression into ix.
    /// </summary>
    internal class PragmaCompiler
    {
        protected PragmaCompiler()
        { }

        public static string? Compile(IPragma pragma, IDeclaration declaration)
        {
            var parser = new Parser(new PragmaGrammar(declaration));

            return Compile(pragma, parser);
        }

        private static string? Compile(IPragma pragma, Parser parser)
        {
            try
            {
                var parseTree = parser.Parse(pragma.Content);

                if (parseTree.HasErrors())
                {
                    if (parseTree.ParserMessages.Any(p => p.Level >= ErrorLevel.Error))
                    {
                        var parserMessages = string.Join("\n",
                            parseTree.ParserMessages.Select(p => $"{p.Message}:{p.Location.ToUiString()}"));
                        throw new MalformedPragmaException(parserMessages);
                    }
                }

                var visitor = new PragmaVisitor();
                (parseTree.Root.AstNode as IVisitableNode)?.AcceptVisitor(visitor);
                return visitor?.Product;
            }
            catch (MalformedPragmaException malformedPragmaException)
            {
                var diagMessage =
                    $"[Error]: {pragma.Location.GetLineSpan().Filename}:{pragma.Location.GetLineSpan().StartLinePosition.Line}, {pragma.Location.GetLineSpan().StartLinePosition.Character} {malformedPragmaException.Message}";
                Log.Logger.Error(diagMessage);
                throw new MalformedPragmaException(diagMessage);
            }

            return string.Empty;
        }

        public static string? Compile(IPragma pragma)
        {
            var parser = new Parser(new PragmaGrammar());

            return Compile(pragma, parser);
        }
    }
}
