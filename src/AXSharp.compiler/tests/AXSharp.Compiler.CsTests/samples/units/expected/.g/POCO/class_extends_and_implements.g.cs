using System;

namespace Pocos
{
    public partial class ExtendsAndImplements : ExtendeeExtendsAndImplements, AXSharp.Connector.IPlain, IImplementation1, IImplementation2
    {
    }

    public partial class ExtendeeExtendsAndImplements : AXSharp.Connector.IPlain
    {
    }

    public partial interface IImplementation1
    {
    }

    public partial interface IImplementation2
    {
    }
}