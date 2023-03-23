// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AXSharp.Localizations;
using AXSharp.Localizations.Abstractions;

namespace AXSharp.Connector.Tests
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

        public Task<T> OnlineToPlain<T>()
        {
            throw new NotImplementedException();
        }

        public Task PlainToOnline<T>(T plain)
        {
            throw new NotImplementedException();
        }

        public Task<T> ShadowToPlain<T>()
        {
            throw new NotImplementedException();
        }

        public Task PlainToShadow<T>(T plain)
        {
            throw new NotImplementedException();
        }

        public object OnlineToPlain()
        {
            throw new NotImplementedException();
        }

        public void PlainToOnline(object plain)
        {
            throw new NotImplementedException();
        }

        public object ShadowToPlain()
        {
            throw new NotImplementedException();
        }

        public void PlainToShadow(object plain)
        {
            throw new NotImplementedException();
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