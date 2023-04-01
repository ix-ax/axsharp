using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.Connector
{
    public static class PlainExtensions
    {
        public static void ToShadow<T>(this T plain, ITwinObject twin) where T : IPlain
        {
            twin.PlainToShadow(plain);
        }

        public static async Task<T> FromShadow<T>(this T plain, ITwinObject twin) where T : IPlain
        {
            return await twin.ShadowToPlain<T>();
        }

        public static async Task ToOnline<T>(this T plain, ITwinObject twin) where T : IPlain
        {
            await twin.PlainToOnline<T>(plain);
        }

        public static async Task<T> FromOnline<T>(this T plain, ITwinObject twin) where T : IPlain
        {
            return await twin.OnlineToPlain<T>();
        }
    }
}
