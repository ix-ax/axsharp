﻿// Ix.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using Ix.Localizations;
using Ix.Localizations.Abstractions;

namespace Ix.Connector.Tests
{
    public class TestTwinObject : ITwinObject
    {
        public string Symbol => "testTwinObjectSymbol";

        public string AttributeName => "testTwinObjectAttributeName";

        public string HumanReadable => "testTwinObjectHumanReadable";

        public TranslatorBase TranslatorBase => throw new NotImplementedException();

        protected ITwinObject Parent { get; set; }

        private List<ITwinPrimitive> valueTags = new List<ITwinPrimitive>();
        private List<ITwinObject> children = new List<ITwinObject>();
        public void AddChild(ITwinObject twinObject)
        {
            children.Add(twinObject);
        }

        public void AddValueTag(ITwinPrimitive twinPrimitive)
        {
            valueTags.Add(twinPrimitive);
        }

        public IEnumerable<ITwinObject> GetChildren()
        {
            return children;
        }

        private static Connector dummyConnector = new DummyConnector();

        public Connector GetConnector()
        {
            return dummyConnector;
        }

        public ITwinObject GetParent()
        {
            return Parent;
        }

        public string GetSymbolTail()
        {
            return null;
        }

        public void Poll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITwinPrimitive> GetValueTags()
        {
            return valueTags;
        }

        public void AddKid(ITwinElement kid)
        {
            
        }

        public IEnumerable<ITwinElement> GetKids()
        {
            throw new NotImplementedException();
        }
    }
}