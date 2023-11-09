using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using AXSharp.TIA2AX.Transformer;
using CommandLine;
using TAXSharp.TIA2AX.Transformer;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(Main);

void Main(Options args)
{
    AXPseoudoProjectGenerator.Create(args.Output, "PseudoAX", args.DataBlocks);
}        











    


