// ixconsole.app
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Runtime.InteropServices;
using System.Threading.Channels;
using ixconsole;

namespace ixconsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Kicks off cyclic operations with 100ms cycle.
            Entry.Plc.Connector.BuildAndStart().ReadWriteCycleDelay = 100;

            // Writes to single variable
            await Entry.Plc.Counter.SetAsync(0);

            // Reads single item from the PLC
            Console.WriteLine(Entry.Plc.Counter.Symbol + "[single]:" + await Entry.Plc.Counter.GetAsync());

            // Subscribe to the variable and performs an operation on change
            Entry.Plc.Counter.Subscribe((sender,primitive) 
                => Console.WriteLine(Entry.Plc.Counter.Symbol + "[subscription]:" +primitive.NewValue));

            // Reads from cyclic operation
            Console.WriteLine(Entry.Plc.Counter.Symbol + "[cyclic]:" + Entry.Plc.Counter.Cyclic);

            Console.WriteLine("Press any key to finish");
            Console.ReadLine();
        }
    }
}