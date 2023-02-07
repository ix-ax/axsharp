// Ix.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Connector.S71500.WebApi;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Ix.Connector.S71500.WebAPITests.Exploratory
{
    public class Exploratory
    {
        private readonly ITestOutputHelper output;

        private static string TargetIp { get; } = Environment.GetEnvironmentVariable("AXTARGET") ?? "10.10.101.1";

        public Exploratory(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void should_read_huge()
        {
#if RELEASE
    return;
#endif
            var connector = new WebApiConnector(TargetIp, "Everybody", "", true);

            await connector.WriteAsync<byte>("myBYTE", 42);
            await connector.WriteAsync<bool>("myBOOL", true);
            await connector.WriteAsync<short>("myINT", 43);

            var myBOOL = new WebApiBool(connector, "", "myBOOL");
            var myBYTE = new WebApiByte(connector, "", "myBYTE");
            var myWORD = new WebApiWord(connector, "", "myWORD");
            //    myDWORD	        :       DWORD	      ;
            var myLWORD = new WebApiLWord(connector, "", "myLWORD");
            var mySINT = new WebApiSInt(connector, "", "mySINT");
            var myINT = new WebApiInt(connector, "", "myINT");
            var myDINT = new WebApiDInt(connector, "", "myDINT");
            var myLINT = new WebApiLInt(connector, "", "myLINT");
            var myUSINT = new WebApiUSInt(connector, "", "myUSINT");
            var myUINT = new WebApiUInt(connector, "", "myUINT");
            var myUDINT = new WebApiUdInt(connector, "", "myUDINT");
            var myULINT = new WebApiULInt(connector, "", "myULINT");
            var myREAL = new WebApiReal(connector, "", "myREAL");
            var myLREAL = new WebApiLReal(connector, "", "myLREAL");
            var myTIME = new WebApiTime(connector, "", "myTIME");
            var myLTIME = new WebApiLTime(connector, "", "myLTIME");
            var myDATE = new WebApiDate(connector, "", "myDATE");
            //   myLDATE	        :       LDATE	      ;
            var myTIME_OF_DAY = new WebApiTimeOfDay(connector, "", "myTIME_OF_DAY");
            //   myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
            var myDATE_AND_TIME = new WebApiDateTime(connector, "", "myDATE_AND_TIME");
            //   myLDATE_AND_TIME :      LDATE_AND_TIME;
            //   myCHAR	        :       CHAR	      ;
            //   myWCHAR	        :       WCHAR	      ;
            var mySTRING = new WebApiString(connector, "", "mySTRING");
            //   myWSTRING	    :       WSTRING	      ;


            List<ITwinPrimitive> primitives = new List<ITwinPrimitive> 
            {
                myBOOL ,
                myBYTE ,
                myWORD ,
          //      DWORD  ,
                myLWORD,
                mySINT ,
                myINT,
                myDINT ,
                myLINT ,
                myUSINT,
                myUINT ,
                myUDINT,
                myULINT,
                myREAL ,
                myLREAL,
                myTIME ,
                myLTIME,
                myDATE ,
                //   myLDATE	        :
                myTIME_OF_DAY,
                //   myLTIME_OF_DAY	:
                myDATE_AND_TIME,
                //   myLDATE_AND_TIME
                //   myCHAR	        :
                //   myWCHAR	     
                mySTRING,
                //   myWSTRING	    :
            };
            
            List<ITwinPrimitive> huge = new List<ITwinPrimitive>();

            for (int i = 0; i < 29; i++)
            {
                huge.AddRange(primitives);
            }

            //for (int i = 0; i < 1; i++)
            {
                huge.AddRange(primitives.Take(11));
            }

           

            await connector.ReadBatchAsync(huge);
           
           

            Assert.Equal(42, myBYTE.Cyclic);
            Assert.True(myBOOL.Cyclic);
            Assert.Equal(43, myINT.Cyclic);
        }
    }
}