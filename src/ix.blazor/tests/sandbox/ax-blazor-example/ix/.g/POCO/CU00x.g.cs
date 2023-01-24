using System;

namespace Pocos
{
    public partial class CU00x : CUBase
    {
        public string _cuName { get; set; } = string.Empty;
    }

    public partial class CUBase
    {
        public string _baseName { get; set; } = string.Empty;
    }
}