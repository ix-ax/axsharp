﻿// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

namespace AXSharp.Connector.Onliners.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using AXSharp.Connector.Tests;
    using AXSharp.Connector.ValueTypes;

    public class OnlinerUDIntTest : OnlinerBaseTests<uint>
    {
        protected override OnlinerBase<uint> Onliner { get; set; }


        public override void Init()
        {
            Onliner = new OnlinerUDInt(new TestTwinObject(), $"readableTail", "symbolTail");
        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Arrange

            var expected = uint.MaxValue;

            //-- Act
            Onliner.Edit = expected;

            //-- Assert
            Assert.AreEqual(expected, Onliner.GetAsync().Result);
            Assert.AreEqual($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};0;{expected}", logs);

        }

        [Test()]
        public void ChangeShadow()
        {
            //-- Arrange

            var expected = uint.MaxValue;

            //-- Act
            Onliner.Shadow = expected;

            //-- Assert
            Assert.AreEqual(expected, Onliner.Shadow);
            Assert.AreEqual($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};0;{expected}", logs);
        }

        [Test()]
        public void ValidateTightRangeTest()
        {
            //-- Arrange
            var min = OnlinerUDInt.MinValue;
            var max = OnlinerUDInt.MaxValue;
            var mid = (OnlinerUDInt.MaxValue / 2);

            //-- Act  
            Assert.True(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.True(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.True(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test()]
        public void ValidateOverShootRangePresetTest()
        {

            Onliner.AttributeMinimum = (OnlinerUDInt.MinValue + 1);
            Onliner.AttributeMaximum = (OnlinerUDInt.MaxValue - 1);
            //-- Arrange
            var min = OnlinerUDInt.MinValue;
            var max = OnlinerUDInt.MaxValue;


            //-- Act  
            Assert.IsFalse(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            var expected = OnlinerUDInt.MaxValue / 12;
            Onliner.SetAsync(expected).Wait();

            Assert.AreEqual(expected, Onliner.GetAsync().Result);
        }
    }
}