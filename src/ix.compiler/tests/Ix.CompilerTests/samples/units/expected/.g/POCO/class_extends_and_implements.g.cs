using System;

namespace Pocos
{
    public partial class ExtendsAndImplements : ExtendeeExtendsAndImplements, IImplementation1, IImplementation2
    {
    }

    public partial class ExtendeeExtendsAndImplements
    {
    }

    public interface IImplementation1
    {
    }

    public interface IImplementation2
    {
    }
}